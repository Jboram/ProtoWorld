using UnityEngine;

namespace ProtoWorld
{
    public interface IDetectionStrategy
    {
        bool Execute(Transform player, Transform detector);
    }
}