
using UnityEngine;

namespace ProtoWorld
{
    public class DialogueInteraction : IInteractable
    {
        [SerializeField] private Sprite npcSprite;
        [SerializeField] private DialogData[] dialogs;
        public override string GetInteractionText()
        {
            return "대화 하기";
        }

        public override void Interact(PlayerController playerController)
        {
            var dialogueWindow = UIManager.inst.GetWindow<DialogueWindow>();
            dialogueWindow.SetDialogue(npcSprite, dialogs);
            dialogueWindow.Show();
        }
    }
}