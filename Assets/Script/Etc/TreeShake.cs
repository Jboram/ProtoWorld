using ProtoWorld;
using UnityEngine;
namespace ProtoWorld
{
    public class TreeShake : MonoBehaviour
    {
        private Animator animator;
        private Health health;
        private int TreeShakeHash = Animator.StringToHash("TreeShake");
        [SerializeField] private GameObject fruit;

        private void Start()
        {
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
            health.OnDeadEvent += Shake;

            fruit.GetComponent<Collectible>().enabled = false;
        }

        private void Shake()
        {
            Debug.Log("shakeing");
            animator.SetTrigger(TreeShakeHash);
        }

        public void DropFruit()
        {
            Debug.Log("drop!");
            fruit.transform.SetParent(null);
            fruit.GetComponent<Rigidbody>().useGravity = true;
            fruit.GetComponent<Collectible>().enabled = true;
        }
    }
}
