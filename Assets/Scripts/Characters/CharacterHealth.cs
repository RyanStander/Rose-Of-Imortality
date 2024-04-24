using UnityEngine;

namespace Characters
{
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] private int maxHealth = 100;
        private int currentHealth;
        public bool IsDead { get; private set; }

        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            IsDead = true;
            Destroy(gameObject);
        }
    }
}
