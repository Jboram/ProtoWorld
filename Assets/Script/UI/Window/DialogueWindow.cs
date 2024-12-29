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

        private int currentDialogIndex = -1;        // 현재 대사 순번
        private int currentSpeakerIndex = 0;	    // 현재 말을 하는 화자(Speaker)의 speakers 배열 순번 (0번은 플레이어, 1은 NPC)
        private float typingSpeed = 0.1f;           // 텍스트 타이핑 효과의 재생 속도
        private bool isTypingEffect;        // 텍스트 타이핑 효과를 재생중인지

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
                if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼
                {
                    // 텍스트 타이핑 효과를 재생중일때 마우스 왼쪽 클릭하면 타이핑 효과 종료
                    if (isTypingEffect == true)
                    {
                        isTypingEffect = false;

                        // 타이핑 효과를 중지하고, 현재 대사 전체를 출력한다
                        StopCoroutine(typeTextCoroutin);
                        typeTextCoroutin = null;

                        dialogueText.text = dialogs[currentDialogIndex].dialogue;
                        // 대사가 완료되었을 때 출력되는 커서 활성화
                        imgArrow.enabled = true;
                    }
                    else // 화살표가 있는 상태라면
                    {
                        // 다음에 보여줄게 있다면,
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
            // 이전 화자의 대화 관련 오브젝트 비활성화
            SetActiveObjects(currentSpeakerIndex, false);

            // 다음 대사를 진행하도록 
            currentDialogIndex++;

            // 현재 화자 순번 설정
            currentSpeakerIndex = dialogs[currentDialogIndex].speakerIndex;

            // 현재 화자의 대화 관련 오브젝트 활성화
            SetActiveObjects(currentSpeakerIndex, true);

            imgArrow.enabled = false;

            // 현재 화자의 대사 텍스트 설정
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

            // 텍스트를 한글자씩 타이핑치듯 재생
            while (length < dialogs[currentDialogIndex].dialogue.Length)
            {
                length++;
                dialogueText.text = dialogs[currentDialogIndex].dialogue.Substring(0, length);
                yield return new WaitForSeconds(typingSpeed);
            }

            isTypingEffect = false;

            // 대사가 완료되었을 때 출력되는 커서 활성화
            imgArrow.enabled = true;
        }
    }
}