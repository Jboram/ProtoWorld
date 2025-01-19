using UnityEngine;

namespace ProtoWorld
{
    public class DefaultDetectionStrategy : IDetectionStrategy
    {
        readonly float detectionAngle;
        readonly float detectionRadius;
        readonly float halfRadius;
        readonly Transform baseTransform;
        readonly float baseRadius;

        public DefaultDetectionStrategy(float detectionAngle, float detectionRadius, Transform baseTransform, float baseRadius)
        {
            this.detectionAngle = detectionAngle;
            this.detectionRadius = detectionRadius;
            halfRadius = detectionRadius / 2.0f;
            this.baseTransform = baseTransform;
            this.baseRadius = baseRadius;
        }

        public bool Execute(Transform target, Transform detector) //detector=몬스터, target= player
        {
            if (baseTransform != null)
            {
                float targetDistanceFromBase = (baseTransform.position - target.position).magnitude;
                if (targetDistanceFromBase < baseRadius) // 기준 위치 범위 안에 들어온 경우
                {
                    return true;
                }

                float detectorDistanceFromBase = (baseTransform.position - detector.position).magnitude;
                if (detectionRadius < detectorDistanceFromBase)
                {
                    return false;
                }
            }

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