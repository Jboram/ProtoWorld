using UnityEngine;

namespace ProtoWorld
{
    public class DefaultDetectionStrategy : IDetectionStrategy
    {
        readonly float detectionAngle;
        readonly float detectionRadius;
        readonly float halfRadius;

        public DefaultDetectionStrategy(float detectionAngle, float detectionRadius)
        {
            this.detectionAngle = detectionAngle;
            this.detectionRadius = detectionRadius;
            halfRadius = detectionRadius / 2.0f;
        }

        public bool Execute(Transform target, Transform detector) //detector=몬스터, target= player
        {
            var directionToPlayer = target.position - detector.position; //플레이어에 대한 방향, 거리 
            var angleToPlayer = Vector3.Angle(directionToPlayer, detector.forward);//두 벡터의 각도

            if (directionToPlayer.magnitude < halfRadius) //플레이어가 작은 반경에 들어와있는지
            {
                return true; //detect 함
            }

            if ((angleToPlayer < detectionAngle / 2f) && (directionToPlayer.magnitude < detectionRadius))
            {//몬스터의 시야각 안쪽에 있고 바깥 반경 안에 있는 지
                return true;
            }
            return false; //detect 못함
        }
    }
}