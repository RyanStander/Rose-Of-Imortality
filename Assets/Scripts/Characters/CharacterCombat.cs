using System;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

namespace Characters
{
    public class CharacterCombat : MonoBehaviour
    {
        [SerializeField] protected Muzzle Muzzle;
        [SerializeField] protected WeaponSfx WeaponSfx;
        [SerializeField] protected Transform RaycastOriginTransform;

        [SerializeField] protected float FireRate = 0.5f;

        [SerializeField] protected int MaxAmmo = 10;
        [SerializeField] protected int Damage = 10;
        protected int CurrentAmmo = 10;
        protected float TimeStamp;

        private void OnValidate()
        {
            GetComponents();
        }

        protected virtual void GetComponents()
        {
            if (Muzzle == null)
                Muzzle = GetComponentInChildren<Muzzle>();

            if (WeaponSfx == null)
                WeaponSfx = GetComponentInChildren<WeaponSfx>(); 
        }
        
        protected virtual void Fire()
        {
            if (CurrentAmmo <= 0)
            {
                Reload();
                return;
            }
            
            Muzzle.Flash();
            WeaponSfx.PlayFireSound();
            
            if (CheckIfHit() is CharacterHealth characterHealth)
            {
                characterHealth.TakeDamage(Damage);
            }
            
            CurrentAmmo--;
        }
        
        protected virtual void Reload()
        {
            CurrentAmmo = MaxAmmo;
            WeaponSfx.PlayReloadSound();
        }

        private CharacterHealth CheckIfHit()
        {
            if (Physics.Raycast(RaycastOriginTransform.position, RaycastOriginTransform.forward, out var hit, 100))
            {
                if (hit.transform.TryGetComponent(out CharacterHealth characterHealth))
                {
                    return characterHealth;
                }
            }

            return null;
        }
    }
}
