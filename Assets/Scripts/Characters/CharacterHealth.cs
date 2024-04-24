using UnityEngine;
using UnityEngine.Serialization;

namespace Characters
{
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] protected int MaxHealth = 100;
        protected int CurrentHealth;
        public bool IsDead { get; protected set; }

        private void Start()
        {
            Setup();
        }

        protected virtual void Setup()
        {
            CurrentHealth = MaxHealth;
        }

        public virtual void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            IsDead = true;
        }
    }
}
