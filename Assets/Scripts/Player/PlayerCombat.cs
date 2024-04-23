using System;
using Characters;
using UnityEngine;
using Weapons;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Muzzle muzzle;
        [SerializeField] private WeaponSfx weaponSfx;
        [SerializeField] private float fireRate = 0.5f;
        
        [SerializeField] private int maxAmmo = 10;
        [SerializeField] private int damage = 10;
        private int currentAmmo = 10;
        
        private float timeStamp;

        private void OnValidate()
        {
            if (playerManager == null)
                playerManager = GetComponentInParent<PlayerManager>();
            
            if (playerCamera == null)
                 playerCamera= Camera.main;

            if (muzzle==null)
                muzzle = GetComponentInChildren<Muzzle>();
            
            if (weaponSfx==null)
                weaponSfx = GetComponentInChildren<WeaponSfx>();
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
            
            muzzle.Flash();
            weaponSfx.PlayFireSound();
            
            if (CheckIfHit() is CharacterHealth characterHealth)
            {
                characterHealth.TakeDamage(damage);
            }

            currentAmmo--;
            
            playerManager.PlayerAnimator.Fire();
        }

        private CharacterHealth CheckIfHit()
        {
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out var hit, 100))
            {
                if (hit.transform.TryGetComponent(out CharacterHealth characterHealth))
                {
                    return characterHealth;
                }
            }

            return null;
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
            
            weaponSfx.PlayReloadSound();
            currentAmmo = maxAmmo;
        }
    }
}
