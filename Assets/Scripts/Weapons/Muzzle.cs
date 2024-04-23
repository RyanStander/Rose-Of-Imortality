using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Muzzle : MonoBehaviour
    {
        [SerializeField] private GameObject muzzleFlash;
        [SerializeField] private Transform muzzleTransform;
        [SerializeField] private float flashDuration = 0.1f;

        public void Flash()
        {
            var flash = Instantiate(muzzleFlash, muzzleTransform.position, muzzleTransform.rotation, muzzleTransform);
            Destroy(flash, flashDuration);
        }
    }
}
