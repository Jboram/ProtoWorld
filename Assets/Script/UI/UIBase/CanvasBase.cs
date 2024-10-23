using UnityEngine;
using UnityEngine.UI;

namespace ProtoWorld 
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(CanvasScaler))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public abstract class CanvasBase : BaseUI
    {
        protected Canvas canvas;
        protected CanvasGroup canvasGroup;
        protected CanvasScaler canvasScaler;
        protected GraphicRaycaster graphicRaycaster;

        protected override void Awake()
        {
            base.Awake();
            this.canvas = GetComponent<Canvas>();
            this.canvasGroup = GetComponent<CanvasGroup>();
            this.canvasScaler = GetComponent<CanvasScaler>();
            this.graphicRaycaster = GetComponent<GraphicRaycaster>();

            this._isOpen = false;

            this.canvas.overrideSorting = true;
            this.canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            this.canvas.additionalShaderChannels = AdditionalCanvasShaderChannels.TexCoord1;

            this.canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            this.canvasScaler.referenceResolution = new Vector2(1920, 1080);
            this.canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        }
    }
}