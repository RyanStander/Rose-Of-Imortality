using System;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private float fireRate = 0.5f;
        
        [SerializeField] private int maxAmmo = 10;
        private int currentAmmo = 10;
        
        private float timeStamp;

        private void OnValidate()
        {
            if (playerManager == null)
                playerManager = GetComponentInParent<PlayerManager>();
        }

        public void HandleCombat()
        {
            if (playerManager.PlayerAnimator.isReloading)
            {
                return;
            }
            
            if(playerManager.Inputs.reload && currentAmmo<maxAmmo)
            {
                Reload();
                playerManager.Inputs.reload = false;
            }
            
            if (playerManager.Inputs.fire && Time.time >= timeStamp)
            {
                timeStamp = Time.time + fireRate;
                Fire();
            }
            else if (playerManager.Inputs.fire)
            {
                playerManager.Inputs.fire = false;
            }
        }
        
        private void Fire()
        {
            if (currentAmmo <= 0)
            {
                Reload();
                return;
            }

            currentAmmo--;
            
            playerManager.PlayerAnimator.Fire();
        }

        private void Reload()
        {
            if (currentAmmo>0)
            {
                playerManager.PlayerAnimator.TacticalReload();
            }
            else
            {
                playerManager.PlayerAnimator.EmptyReload();
            }
            
            currentAmmo = maxAmmo;
        }
    }
}
