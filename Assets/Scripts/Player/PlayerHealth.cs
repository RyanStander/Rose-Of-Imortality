using System;
using Characters;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : CharacterHealth
    {
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private PlayerManager playerManager;

        private void OnValidate()
        {
            if (playerManager == null)
                playerManager = FindObjectOfType<PlayerManager>();

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

        public override void Heal(int healAmount)
        {
            base.Heal(healAmount);
            healthBar.SetHealth(CurrentHealth);
        }

        protected override void Die()
        {
            if (!IsDead)
                playerManager.PlayerDeathHandler.HandleDeath();

            IsDead = true;
        }
    }
}
