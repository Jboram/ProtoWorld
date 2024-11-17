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

        public bool Execute(Transform target, Transform detector)
        {
            var directionToPlayer = target.position - detector.position;
            var angleToPlayer = Vector3.Angle(directionToPlayer, detector.forward);

            if (directionToPlayer.magnitude < halfRadius)
            {
                return true;
            }

            if ((angleToPlayer < detectionAngle / 2f) && (directionToPlayer.magnitude < detectionRadius))
            {
                return true;
            }
            return false;
        }
    }
}