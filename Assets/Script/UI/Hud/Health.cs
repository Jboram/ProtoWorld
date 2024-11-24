using UnityEngine;

namespace ProtoWorld
{
    public class Health : MonoBehaviour
    {
        private int currentHealth;
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private HealthBar healthBar;

        private void Start()
        {
            currentHealth = maxHealth;
            healthBar?.SetMaxHealth(maxHealth);
        }

        public void TakeDamage(int amount)
        {
            currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
            healthBar?.SetHealth(currentHealth);
        }
    }
}