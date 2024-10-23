using UnityEngine.EventSystems;


namespace ProtoWorld 
{
    public class BaseUI : UIBehaviour
    {
        // UI의 공통 동작
        protected bool _isOpen;
        public bool IsOpen => _isOpen;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start(); 
        }

        // UI 활성화 메서드
        public virtual void Show()
        {
            _isOpen = true;
            gameObject.SetActive(true);
            OnShow();
        }

        public virtual void Hide()
        {
            _isOpen = false;
            gameObject.SetActive(false);
            OnHide();
        }

        public virtual void Initialize()
        {
        }

        protected virtual void OnShow() { }
        protected virtual void OnHide() { }

        // UI의 상태를 토글
        public void Toggle()
        {
            if (_isOpen)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
}