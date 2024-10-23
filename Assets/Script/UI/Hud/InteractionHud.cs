using TMPro;
using UnityEngine;

namespace ProtoWorld 
{
   public class InteractionHud : HudBase
    {
        [SerializeField] private TextMeshProUGUI interactionText;
        public void ShowText(string text)
        {
            interactionText.text = text;
        }
    }
}