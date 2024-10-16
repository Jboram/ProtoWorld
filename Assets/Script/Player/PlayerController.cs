using System.Collections; // NameSpace = package
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProtoWorld 
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterController controller;
        [SerializeField] private Animator animator;

        float moveSpeed = 6f;    // 이동 속도
        float gravity = -9.81f;  // 중력 값
        float smoothTime = 0.2f; //값이 클 수록 목표 속도에 늦게 도달함.

        float currentSpeed;
        float velocity;
        const float ZeroF = 0f;

        static readonly int Speed = Animator.StringToHash("Speed");



        void Awake() {
            controller = GetComponent<CharacterController>();
            // GetComponents<CharacterController>();
            //GetComponentInChildren<>(); //하위 Object의 해당 컴포넌트가 있는 자식 하나만 반환
            //GetComponentsInChildren<CharacterController>().ToList();//하위 Object의 해당 컴포넌트가 있는 자식 모두 반환 
        }


        void Update()
        {
            CheckInteract();
            HandleMovement(); //이동, 방향전환 처리
            UpdateAnimator(); //애니메이션 처리
        }

        void CheckInteract()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1.5f); //반경 2만큼의 충돌을 찾을 영역의 구체를 만듬.

            // 각 콜라이더를 검사하여 상호작용 가능한 오브젝트와 상호작용
            foreach (Collider collider in hitColliders)
            {
                IInteractable interactable = collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    Debug.Log(collider.gameObject.name + " 상호작용");
                    interactable.Interact();
                }
            }
        }

        void HandleMovement()
        {
            // 플레이어 입력 처리
            float moveX = Input.GetAxisRaw("Horizontal"); //-1, 0 , 1 중에 들어옴 
            float moveZ = Input.GetAxisRaw("Vertical");

            var movementDirection = new Vector3(moveX, 0, moveZ).normalized; //높이는 변경 없어서 0으로 지정. // 1 : 1 : 루트2-> Normalized로 1로 조정 
            var adjustedDirection = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * movementDirection;
            if (ZeroF < adjustedDirection.magnitude) //magnitude는 절대값을 가져옴 
            { //값 입력이 있을때
                HandleRotation(adjustedDirection); //방향 전환 
                SmoothSpeed(moveSpeed); //이동 속도를 부드럽게 변경 
                MoveCharacter(adjustedDirection); //캐릭터 이동 
            }
            else if(ZeroF < currentSpeed) //입력이 없을 때 이동 속도가 천천히 바뀌도록 (가속도)
            {
                SmoothSpeed(ZeroF); //속도를 줄임.
                MoveCharacter(transform.forward); //이동 
            }
        }
        void HandleRotation(Vector3 adjustedDirection)
        {
            transform.LookAt(transform.position + adjustedDirection); //현재 위치와 봐야하는 위치로 바라봐야 하는 방향 계산함. 해당 방향으로 변경함.
        }

        void MoveCharacter(Vector3 adjustedDirection)
        {
            var adjustedMovement = adjustedDirection * currentSpeed * Time.deltaTime; //이동할 방향 * 현재 속도 * 업데이트가 될때 (프레임 차이 만큼)의 시간값
            controller.Move(adjustedMovement);
            controller.Move(Vector3.down); //아래로 1만큼 이동//중력 작용 //y축 
        }

        void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);  //현재 속도, 목표 속도, 가속도 계산용 참조 변수, 변경할 크기값
        }

        void UpdateAnimator()
        {
            animator.SetFloat(Speed, currentSpeed / moveSpeed); //현재 속도/최대속도 비율 
        }
    }
}
