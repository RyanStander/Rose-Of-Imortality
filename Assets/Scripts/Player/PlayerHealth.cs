using System;
using Characters;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : CharacterHealth
    {
        [SerializeField] private HealthBar healthBar;

        private void OnValidate()
        {
            if (healthBar == null)
                healthBar = FindObjectOfType<HealthBar>();
        }

        protected override void Setup()
        {
            base.Setup();
            
            healthBar.SetMaxHealth(MaxHealth);
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Debug.Log("Player took damage: " + damage);
            healthBar.SetHealth(CurrentHealth);
        }

        protected override void Die()
        {
            IsDead = true;
            Debug.Log("Player is dead!");
        }
    }
}
