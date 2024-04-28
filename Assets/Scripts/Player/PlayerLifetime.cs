using System;
using UI;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// Player lives for 24 hours. He can spend his lifetime to perform certain actions. Once it reaches 0, he dies
    /// </summary>
    public class PlayerLifetime : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private float startingLifetime = 86400f;
        [SerializeField] private LifeTimer lifeTime;
        private float currentLifetime;
        private bool isDead;

        private void OnValidate()
        {
            if (lifeTime == null)
                lifeTime = GetComponentInChildren<LifeTimer>();
            
            if (playerManager == null)
                playerManager = GetComponent<PlayerManager>();
        }

        private void Start()
        {
            currentLifetime = startingLifetime;
            lifeTime.Setup(currentLifetime);
            InvokeRepeating(nameof(TickLifetime), 1f, 1f);
        }
        
        public void SpendTime(float seconds)
        {
            currentLifetime -= seconds;
            
            if(currentLifetime < 0f)
                currentLifetime = 0f;
            
            lifeTime.UpdateLifetimeUI(currentLifetime);
        }
        
        private void TickLifetime()
        {
            SpendTime(1f);

            if (currentLifetime <= 0f)
            {
                Debug.Log("Out of lifetime!");
                if (!isDead)
                {
                    isDead = true;
                    playerManager.PlayerDeathHandler.HandleDeath();
                }
            }
        }
    }
}
