using UnityEngine;

namespace ProtoWorld
{
    public class HitCollider : MonoBehaviour
    {
        //공격하는 영역의 collider
        // public enum Type
        // {
        //     None,
        //     Player,
        //     Enemy,
        // }
        private Collider collider;
        [SerializeField] private int damage;
        // [SerializeField] private Type type;

        private void Start()
        {
            collider = GetComponent<Collider>();
        }

        public void OnTriggerEnter(Collider other)
        {
            var health = other.GetComponent<Health>();
            if (health != null && other.tag != tag)
            {
                //other(= 공격 받는 녀석)이 체력이 있고
                 //공격 하는 object랑 공격하는 object가 다를 때 데미지 줌
                health.TakeDamage(damage);
            }
        }
        // public void OnTriggerExit(Collider other)
        // {
        //     var health = other.GetComponent<Health>();
        //     if (health != null)
        //     {
        //         Debug.Log(other.name + " 충돌 나감");
        //     }
        // }

        public void EnableCollider(bool enable)
        {
            collider.enabled = enable;
        }

        public void SetHitDamage(int damage)
        {
            this.damage = damage;
        }

        public int GetDamage()
        {
            return damage;
        }
        
        // public Type GetHitColliderType()
        // {
        //     return type;
        // }
    }
}
