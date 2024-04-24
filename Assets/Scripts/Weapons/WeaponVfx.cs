using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Weapons
{
    public class WeaponVfx : MonoBehaviour
    {
        [SerializeField] private GameObject bulletHolePrefab;

        public void SpawnBulletHole(RaycastHit hit)
        {
            if (bulletHolePrefab == null)
            {
                Debug.LogWarning("Bullet hole prefab is not assigned in the inspector");
                return;
            }
            
            //check if the object we hit is on the Environment layer
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Environment")) 
                return;

            Instantiate(bulletHolePrefab, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
