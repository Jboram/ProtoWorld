using UnityEngine;
namespace ProtoWorld
{
    public static class AnimatorExtensions
    {
        public static bool HasParameter(this Animator animator, string paramName)
        {
            foreach (var param in animator.parameters)
            {
                if (param.name == paramName)
                    return true;
            }
            return false;
        }

        public static bool HasParameter(this Animator animator, int nameHash)
        {
            foreach (var param in animator.parameters)
            {
                if (param.nameHash == nameHash)
                    return true;
            }
            return false;
        }
    }
}