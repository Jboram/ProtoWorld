using System.Collections; // NameSpace = package
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace ProtoWorld 
{
    public class PlayerController : MonoBehaviour
    {
        private CharacterController controller;
        [SerializeField] private Animator animator;
        [SerializeField] private Collider hitCollider;
        [SerializeField] private AnimationEventHandler animEventHandler;
        public LayerMask groundLayer;

        float moveSpeed = 6f;    // 이동 속도
        float gravity = -9.81f;  // 중력 값
        float smoothTime = 0.2f; //값이 클 수록 목표 속도에 늦게 도달함.

        float currentSpeed;
        float velocity;
        const float ZeroF = 0f;

        static readonly int SpeedHash = Animator.StringToHash("Speed");
        static readonly int AttackHash = Animator.StringToHash("Attack");

        private int currentCornerIndex = 0;
        private NavMeshPath path;

        List<IInteractable> interactableObjects = new List<IInteractable>();
        IInteractable currentInteractObject;

        private Inventory inventory;
        public Inventory Inventory => inventory;

        void Awake() {
            PlayerService.inst.AddPlayerController(this);
            controller = GetComponent<CharacterController>();
            path = new NavMeshPath();
            inventory = new Inventory();
            // GetComponents<CharacterController>();
            //GetComponentInChildren<>(); //하위 Object의 해당 컴포넌트가 있는 자식 하나만 반환
            //GetComponentsInChildren<CharacterController>().ToList();//하위 Object의 해당 컴포넌트가 있는 자식 모두 반환 
       
            animEventHandler.RegisterEvent("AttackStart", AttackStart);
            animEventHandler.RegisterEvent("AttackEnd", AttackEnd);
        }


        void Update()
        {
            if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == AttackHash)
            {
                //이동중 공격 불가 처리
                return;
            }
            UIInteractionCheck();
            CheckInteract();
            HandleMovement(); //이동, 방향전환 처리
            UpdateAnimator(); //애니메이션 처리
        }


        void UIInteractionCheck()
        {
             if (EventSystem.current.IsPointerOverGameObject() == false) //UI를 클릭중이지 않으면 
            {
                if (Input.GetMouseButtonDown(1)) // 우클릭 감지
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//Ray :현재 위치로 부터의 방향
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer)) // "Ground" 레이어만 감지
                    {//Raycast :layer mask를 감지하여 얼만큼 ray를 쏴서 맞춘 위치 (hit)
                        if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, 5.0f, NavMesh.AllAreas))
                        {//samplePosition 이동하려는 위치에서 일정 반경 내에 이동가능 위치를 찾음
                            path.ClearCorners();
                            currentCornerIndex = 0;
                            if (NavMesh.CalculatePath(transform.position, navHit.position, NavMesh.AllAreas, path))
                            {//이동할 경로 계산                        
                                
                            }
                        }
                    }
                }
                else if (Input.GetMouseButtonDown(0)) //0: 좌클릭 
                {
                    if (EventSystem.current.IsPointerOverGameObject() == false) //UI를 클릭중이지 않으면 
                    {
                        Attack();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                var inventoryWindow = UIManager.inst.GetWindow<InventoryWindow>();
                inventoryWindow.Toggle();
                if(inventoryWindow.IsOpen)
                {
                    inventoryWindow.SetInventory(inventory);
                }
                else
                {
                    var craftWindow = UIManager.inst.GetWindow<CraftWindow>();
                    if (craftWindow.IsOpen)
                    {
                        craftWindow.Hide();
                    }
                }
            }
        }

        //상호 작용 가능한 오브젝트 탐색
        //상호 작용할 예정인 오브젝트가 있고, 그녀석과 상호 작용 가능한 거리에 있을 경우, interact ghcnf
        void CheckInteract()
        {
            if (currentInteractObject != null)
            {
                if (currentInteractObject.CanInteract(transform.position))
                {
                    currentInteractObject.Interact(this);
                    currentInteractObject = null;
                    return;
                }
            }

            interactableObjects.Clear();
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4.0f); //반경 2만큼의 충돌을 찾을 영역의 구체를 만듬.

            foreach (Collider collider in hitColliders) //충돌된 오브젝트중에 탐색
            {
                IInteractable interactable = collider.GetComponent<IInteractable>(); 
                if (interactable != null && interactable.enabled)
                {
                    interactableObjects.Add(interactable); //IInteractable이 있는 오브젝트만 리스트에 추가
                }
            }

            var interactionHud = UIManager.inst.GetHud<InteractionHud>();
            if (0 < interactableObjects.Count)
            {
                var position = transform.position;
                interactableObjects.Sort((x1, x2) => x1.Distance(position).CompareTo(x2.Distance(position))); //거리를 비교해서 가까운 순으로 정렬
                var interactObject = interactableObjects[0];
                UIManager.inst.ShowHud<InteractionHud>();
                interactionHud.ShowText(interactObject.GetInteractionText()); //가장 가까운 object의 텍스트를 가져와 보여줌

                if(Input.GetKeyDown(KeyCode.F)) //F 키가 눌렸을때
                {
                    currentInteractObject = interactObject;
                    if (interactObject.CanInteract(position)) //상호작용 가능한 거리면 바로 상호작용
                    {
                        interactObject.Interact(this);
                        currentInteractObject = null;
                    }
                    else //상호작용 가능한 거리가 아니면 이동 경로 계산함 
                    {
                        if (NavMesh.CalculatePath(transform.position, interactObject.transform.position, NavMesh.AllAreas, path))
                        {
                            
                        }
                    }
                }
            }
            else //상호 작용 가능한 오브젝트가 없으면 
            {
                currentInteractObject = null;
                if (interactionHud.IsOpen)
                {
                    interactionHud.Hide(); // hub (상호작용 ui)를 숨김
                }
            }
        }

        void HandleMovement()
        {
            // 플레이어 입력 처리
            float moveX = Input.GetAxisRaw("Horizontal"); //-1, 0 , 1 중에 들어옴 
            float moveZ = Input.GetAxisRaw("Vertical");
            Vector3 movementDirection = new Vector3(moveX, 0, moveZ).normalized; //높이는 변경 없어서 0으로 지정. // 1 : 1 : 루트2-> Normalized로 1로 조정 
            Vector3 adjustedDirection = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * movementDirection;

            if (movementDirection.magnitude != ZeroF) //입력 값이 있으면 
            {
                path.ClearCorners(); //경로 초기화 
            }
            
            if (0 < path.corners.Length && currentInteractObject != null) //(입력이 없음 == ) 경로가 있으면 이동할 방향 계산
            {
                Vector3 targetPosition = path.corners[currentCornerIndex];
                adjustedDirection = (targetPosition - transform.position);
                adjustedDirection.y = 0.0f;
                adjustedDirection.Normalize();
            }

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

            if (0 < path.corners.Length && currentInteractObject != null) //경로가 있을때
            {
                Vector3 targetPosition = path.corners[currentCornerIndex];

                Vector3 currentPosition = transform.position;
                Vector3 nextPosition = currentPosition + adjustedMovement;
                //다음 이동할 위치가 내 이동 거리보다 작을때 필요만큼만 이동 
                if (Vector3.Distance(nextPosition, targetPosition) < Vector3.Distance(currentPosition, targetPosition))
                {
                    controller.Move(adjustedMovement);
                }
                else
                {
                    // 지나쳤다면 정확히 해당 코너 위치로 이동
                    controller.Move(targetPosition - controller.transform.position);
                    currentCornerIndex++;
                    if (path.corners.Length <= currentCornerIndex)
                    {
                        currentCornerIndex = 0;
                        path.ClearCorners();
                    }
                }
            }
            else //경로가 없을때 이동 
            {
                controller.Move(adjustedMovement);
            }

            controller.Move(Vector3.down); //아래로 1만큼 이동//중력 작용 //y축 
        }

        void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);  //현재 속도, 목표 속도, 가속도 계산용 참조 변수, 변경할 크기값
        }

        void UpdateAnimator()
        {
            animator.SetFloat(SpeedHash, currentSpeed / moveSpeed); //현재 속도/최대속도 비율 
        }

        void Attack()
        {
            animator.SetTrigger(AttackHash);
        }

        void AttackStart()
        {
            hitCollider.enabled = true;
        }

        void AttackEnd()
        {
            hitCollider.enabled = false;
        }
        private void OnDrawGizmos()
        {
            if (path != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    Gizmos.DrawLine(path.corners[i], path.corners[i + 1]);
                }
            }
        }
    }
}
