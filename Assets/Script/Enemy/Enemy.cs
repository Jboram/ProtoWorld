using UnityEngine;
using UnityEngine.AI;

namespace ProtoWorld
{
    public class Enemy : MonoBehaviour
    {
        // Component
        private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        private PlayerDetector playerDetector;

        // Data
        private Vector3 startPoint; //처음 시작 위치 
        private float wanderRadius = 10.0f; //배회할 영역의 범위
        private float timeBetweenAttacks = 2f; //공격 사이의 딜레이 시간 

        private bool isMove;
        private bool isInBattle;
        private bool isDetectPlayer;
        private bool canAttackPlayer;

        private static readonly int IsMoveHash = Animator.StringToHash("IsMove");
        private static readonly int IsInBattleHash = Animator.StringToHash("IsInBattle");

        //private static readonly int IdleHash = Animator.StringToHash("Idle");
        //private static readonly int WalkHash = Animator.StringToHash("WalkFWD");
        //private static readonly int IdleBattleHash = Animator.StringToHash("IdleBattle");
        //private static readonly int RunHash = Animator.StringToHash("RunFWD");
        private static readonly int AttackHash = Animator.StringToHash("Attack");
        //private static readonly int DieHash = Animator.StringToHash("Die");

        private CountdownTimer idleTimer;
        private CountdownTimer attackTimer;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            playerDetector = GetComponent<PlayerDetector>();
            startPoint = transform.position;

            idleTimer = new CountdownTimer(5.0f);
            idleTimer.OnTimerStop += OnStopIdle;
            attackTimer = new CountdownTimer(timeBetweenAttacks);

            idleTimer.Start();
        }

        private void OnStopIdle()
        {
            if (isInBattle == false)
            {
                StartWander();
            }
        }

        // Update is called once per frame
        void Update()
        {
            CheckState();
            idleTimer.Tick(Time.deltaTime);
            attackTimer.Tick(Time.deltaTime);
            UpdateAnimState();
        }

        private void UpdateAnimState() //설정한 parameter에 따라 애니메이션 상태 업데이트
        {
            animator.SetBool(IsMoveHash, isMove);
            animator.SetBool(IsInBattleHash, isInBattle);
        }

        private void CheckState()
        {
            isDetectPlayer = playerDetector.CanDetectPlayer(); //플레이어를 찾았는 지
            canAttackPlayer = playerDetector.CanAttackPlayer(); //공격 가능한지

            isInBattle = isDetectPlayer;

            if (canAttackPlayer) //공격 가능한 경우
            {
                agent.ResetPath();
                isMove = false; //이동을 멈춤
                Attack(); //공격 태세
            }
            else if (isDetectPlayer) //플레이어를 찾았는 데 공격 불가능한(공격거리밖) 경우
            {
                if (attackTimer.IsRunning == false) //공격중이 아니면
                {
                    ChasePlayer();//플레이어를 따라감
                }
            }
            else if (IsReachDestination()) //배회중에 도착지에 도착시 
            {
                isMove = false;  //이동을 멈춤
                agent.ResetPath(); //이동할 경로 초지화
                if (idleTimer.IsRunning == false && isInBattle == false)
                { //이동중이 아니고 공격중이 아니면
                    idleTimer.Start(); //대기 타이머 시작
                }
            }
        }

        private void StartWander()
        {
            var randomDirection = UnityEngine.Random.insideUnitSphere * wanderRadius; //이동 가능한 범위 내에 어디로 이동할지 랜덤한 크기 값 지정
            randomDirection += startPoint; //시작 위치 + 랜덤 거리 값 = 이동할 랜덤한 위치 
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1); //목적지에서 가장 가까운 이동 가능한 위치
            var finalPosition = hit.position; //최종 이동할 위치

            agent.SetDestination(finalPosition);
            isMove = true;
        }

        private bool IsReachDestination()
        {
            return !agent.pathPending &&
                    agent.remainingDistance <= agent.stoppingDistance &&
                    (!agent.hasPath || agent.velocity.sqrMagnitude == 0); //목적지에 도착했는지 판단
        }

        private void Attack()
        {
            if (attackTimer.IsFinished) //공격 딜레이가 끝났으면 
            {
                var targetPosition = playerDetector.Player.position; //플레이어의 위치
                targetPosition.y = transform.position.y;
                transform.LookAt(targetPosition); //플레이어를 바라보게 함
                attackTimer.Start(); //공격 타이머 시작
                animator.SetTrigger(AttackHash); //공격 애니메이션 시작 
            }
        }

        private void ChasePlayer()
        {
            var playerController = PlayerService.inst.Controller;
            agent.SetDestination(playerController.transform.position);
            isMove = true;
        }
    }

}
