using System;
using Characters;
using UI;
using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerCombat : CharacterCombat
    {
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private float reloadTimeCost = 1800f; // 30 minutes
        [SerializeField] private AmmoDisplay ammoDisplay;

        protected override void GetComponents()
        {
            base.GetComponents();
            
            if (playerManager == null)
                playerManager = GetComponentInParent<PlayerManager>();
            
            if (RaycastOriginTransform == null)
                RaycastOriginTransform= Camera.main.transform;
            
            if (ammoDisplay == null)
                ammoDisplay = FindObjectOfType<AmmoDisplay>();
        }

        private void Start()
        {
            ammoDisplay.SetValues(MaxAmmo, CurrentAmmo,(int)reloadTimeCost);
        }

        public void HandleCombat()
        {
            if (playerManager.PlayerAnimator.isReloading)
            {
                return;
            }
            
            if(playerManager.Inputs.reload && CurrentAmmo<MaxAmmo)
            {
                Reload();
                playerManager.Inputs.reload = false;
            }
            
            if (playerManager.Inputs.fire && Time.time >= TimeStamp)
            {
                TimeStamp = Time.time + FireRate;
                Fire();
            }
            else if (playerManager.Inputs.fire)
            {
                playerManager.Inputs.fire = false;
            }
        }
        
        protected override void Fire()
        {
            if(CurrentAmmo>0)
                playerManager.PlayerAnimator.Fire();
            
            base.Fire();
            
            ammoDisplay.SetAmmo(CurrentAmmo);
        }

        

        protected override void Reload()
        {
            if (CurrentAmmo>0)
            {
                playerManager.PlayerAnimator.TacticalReload();
            }
            else
            {
                playerManager.PlayerAnimator.EmptyReload();
            }
            
            playerManager.PlayerLifetime.SpendTime(reloadTimeCost);
            
            base.Reload();
            
            ammoDisplay.SetAmmo(CurrentAmmo);
        }
    }
}
