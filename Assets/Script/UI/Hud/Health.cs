using System;
using UnityEngine;

namespace ProtoWorld
{
    public class Health : MonoBehaviour
    {
        private int currentHealth;
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private HealthBar healthBar;

        public Action OnDeadEvent;

        private void Start()
        {
            currentHealth = maxHealth;
            healthBar?.SetMaxHealth(maxHealth);
        }

        public void TakeDamage(int amount)
        {

            if (currentHealth == 0)
            {
                return;
            }

            currentHealth = Mathf.Clamp(currentHealth - amount, 0, maxHealth);
            healthBar?.SetHealth(currentHealth);

            if (currentHealth == 0)
            {
                OnDeadEvent?.Invoke();
            }
        }
    }
}