using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProtoWorld
{
    public class DialogueWindow : WindowBase
    {
        // Component
        [SerializeField] private Image playerImage;
        [SerializeField] private Image npcImage;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Image imgArrow;

        // Data
        private DialogData[] dialogs;

        private int currentDialogIndex = -1;        // ���� ��� ����
        private int currentSpeakerIndex = 0;	    // ���� ���� �ϴ� ȭ��(Speaker)�� speakers �迭 ���� (0���� �÷��̾�, 1�� NPC)
        private float typingSpeed = 0.1f;           // �ؽ�Ʈ Ÿ���� ȿ���� ��� �ӵ�
        private bool isTypingEffect;        // �ؽ�Ʈ Ÿ���� ȿ���� ���������

        Coroutine updateDialogueCoroutin = null;
        Coroutine typeTextCoroutin = null;

        protected override void OnShow()
        {
            base.OnShow();
            if (dialogs == null)
            {
                return;
            }
            currentDialogIndex = -1;
            updateDialogueCoroutin = StartCoroutine(UpdateDialog());
        }

        protected override void OnHide()
        {
            base.OnHide();
            if (updateDialogueCoroutin != null)
            {
                StopCoroutine(updateDialogueCoroutin);
            }

            if (typeTextCoroutin != null)
            {
                StopCoroutine(typeTextCoroutin);
            }

            updateDialogueCoroutin = null;
            typeTextCoroutin = null;
            dialogs = null;
        }

        public void SetDialogue(Sprite npcSprite, DialogData[] dialogs)
        {
            npcImage.sprite = npcSprite;
            this.dialogs = dialogs;
        }

        public IEnumerator UpdateDialog()
        {
            SetNextDialog();
            while (currentDialogIndex < dialogs.Length)
            {
                if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư
                {
                    // �ؽ�Ʈ Ÿ���� ȿ���� ������϶� ���콺 ���� Ŭ���ϸ� Ÿ���� ȿ�� ����
                    if (isTypingEffect == true)
                    {
                        isTypingEffect = false;

                        // Ÿ���� ȿ���� �����ϰ�, ���� ��� ��ü�� ����Ѵ�
                        StopCoroutine(typeTextCoroutin);
                        typeTextCoroutin = null;

                        dialogueText.text = dialogs[currentDialogIndex].dialogue;
                        // ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
                        imgArrow.enabled = true;
                    }
                    else // ȭ��ǥ�� �ִ� ���¶��
                    {
                        // ������ �����ٰ� �ִٸ�,
                        if (currentDialogIndex + 1 < dialogs.Length)
                        {
                            SetNextDialog();
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                yield return null;
            }
            Hide();
        }

        private void SetNextDialog()
        {
            // ���� ȭ���� ��ȭ ���� ������Ʈ ��Ȱ��ȭ
            SetActiveObjects(currentSpeakerIndex, false);

            // ���� ��縦 �����ϵ��� 
            currentDialogIndex++;

            // ���� ȭ�� ���� ����
            currentSpeakerIndex = dialogs[currentDialogIndex].speakerIndex;

            // ���� ȭ���� ��ȭ ���� ������Ʈ Ȱ��ȭ
            SetActiveObjects(currentSpeakerIndex, true);

            imgArrow.enabled = false;

            // ���� ȭ���� ��� �ؽ�Ʈ ����
            //dialogueText.text = dialogs[currentDialogIndex].dialogue;
            typeTextCoroutin = StartCoroutine(TypingText());
        }

        private void SetActiveObjects(int speakerIndex, bool visible)
        {
            if (speakerIndex == 0)
            {
                playerImage.color = visible ? Color.white : Color.gray;
            }
            else
            {
                npcImage.color = visible ? Color.white : Color.gray;
            }
        }


        private IEnumerator TypingText()
        {
            int length = 0;

            isTypingEffect = true;

            // �ؽ�Ʈ�� �ѱ��ھ� Ÿ����ġ�� ���
            while (length < dialogs[currentDialogIndex].dialogue.Length)
            {
                length++;
                dialogueText.text = dialogs[currentDialogIndex].dialogue.Substring(0, length);
                yield return new WaitForSeconds(typingSpeed);
            }

            isTypingEffect = false;

            // ��簡 �Ϸ�Ǿ��� �� ��µǴ� Ŀ�� Ȱ��ȭ
            imgArrow.enabled = true;
        }
    }
}