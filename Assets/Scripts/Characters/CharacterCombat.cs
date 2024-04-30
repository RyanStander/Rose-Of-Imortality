using System;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons;

namespace Characters
{
    public class CharacterCombat : MonoBehaviour
    {
        [SerializeField] protected Muzzle Muzzle;
        [SerializeField] protected WeaponVfx WeaponVfx;
        [SerializeField] protected WeaponSfx WeaponSfx;
        [SerializeField] protected Transform RaycastOriginTransform;
        [SerializeField] protected LayerMask LayersToIgnore;

        [SerializeField] protected float FireRate = 0.5f;
        [SerializeField] protected float WeaponForce = 25f;

        [SerializeField] protected int MaxAmmo = 10;
        [SerializeField] protected int Damage = 10;
        [SerializeField] protected float HeadShotMultiplier = 2;
        [SerializeField] protected float LimbDamageMultiplier = 0.5f;
        public int FireVariationsCount = 4;
        protected int CurrentAmmo;
        protected float TimeStamp;


        private BodyPart shotBodyPart;

        private enum BodyPart
        {
            None,
            Head,
            Limb
        }

        private void OnValidate()
        {
            GetComponents();
        }

        protected virtual void GetComponents()
        {
            if (Muzzle == null)
                Muzzle = GetComponentInChildren<Muzzle>();

            if (WeaponVfx == null)
                WeaponVfx = GetComponentInChildren<WeaponVfx>();

            if (WeaponSfx == null)
                WeaponSfx = GetComponentInChildren<WeaponSfx>();

            CurrentAmmo = MaxAmmo;
        }


        private void Start()
        {
            StartSetup();
        }

        protected virtual void StartSetup()
        {
            CurrentAmmo = MaxAmmo;
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
                var multiplier = shotBodyPart switch
                {
                    BodyPart.Head => HeadShotMultiplier,
                    BodyPart.Limb => LimbDamageMultiplier,
                    _ => 1f
                };

                characterHealth.TakeDamage((int)(Damage * multiplier));
                if (characterHealth.IsDead)
                {
                    var characterRagdoll = characterHealth.GetComponentInChildren<EnableCharacterRagdoll>();
                    if (characterRagdoll != null)
                    {
                        characterRagdoll.EnableRagdoll();
                        characterRagdoll.Push(characterHealth.transform.position - transform.position, WeaponForce);
                    }
                }
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
            if (Physics.Raycast(RaycastOriginTransform.position, RaycastOriginTransform.forward, out var hit, 100,
                    ~LayersToIgnore))
            {
                //get character health in parent
                var characterHealth = hit.transform.GetComponentInParent<CharacterHealth>();
                if (characterHealth != null)
                {
                    if (hit.transform.CompareTag(BodyPart.Head.ToString()))
                        shotBodyPart = BodyPart.Head;
                    else if (hit.transform.CompareTag(BodyPart.Limb.ToString()))
                        shotBodyPart = BodyPart.Limb;
                    else
                        shotBodyPart = BodyPart.None;

                    return characterHealth;
                }

                WeaponVfx.SpawnBulletHole(hit);
                WeaponVfx.SpawnBulletTrail(RaycastOriginTransform);
            }

            return null;
        }
    }
}
