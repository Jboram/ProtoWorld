using UnityEngine;

namespace ProtoWorld
{
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] float detectionAngle = 60f; //플레이어를 탐색하는 각도
        [SerializeField] float detectionRadius = 10f; //바깥 반경 범위
        [SerializeField] float attackRange = 2f;//플레이어가 들어오면 공격할 범위

        public Transform Player { get; private set; }

        IDetectionStrategy detectionStrategy;

        void Start()
        {
            detectionStrategy = new DefaultDetectionStrategy(detectionAngle, detectionRadius);
            Player = PlayerService.inst.Controller.transform; //플레이어의 위치
        }

        void Update()
        {
        }

        public bool CanDetectPlayer()
        {
            return detectionStrategy.Execute(Player, transform);
        }

        public bool CanAttackPlayer()
        {
            var directionToPlayer = Player.position - transform.position;
            return directionToPlayer.magnitude <= attackRange;//플레이어와 몬스터의 거리가 공격 범위보다 가까운지
        }

        void OnDrawGizmos() //공격 영역 표시 
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, detectionRadius);
            Gizmos.DrawWireSphere(transform.position, detectionRadius / 2.0f);

            Vector3 forwardDirection = Quaternion.Euler(0, detectionAngle / 2, 0) * transform.forward * detectionRadius;
            Vector3 backwardDirection = Quaternion.Euler(0, -detectionAngle / 2, 0) * transform.forward * detectionRadius;

            Gizmos.DrawLine(transform.position, transform.position + forwardDirection);
            Gizmos.DrawLine(transform.position, transform.position + backwardDirection);
        }
    }
}