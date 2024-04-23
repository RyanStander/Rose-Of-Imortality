using System;
using UnityEngine;

namespace Weapons
{
    public class WeaponSfx : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip fireSound;
        [SerializeField] private AudioClip reloadSound;
        [SerializeField] private AudioClip emptySound;

        private void OnValidate()
        {
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
        }

        public void PlayFireSound()
        {
            audioSource.PlayOneShot(fireSound);
        }

        public void PlayReloadSound()
        {
            audioSource.PlayOneShot(reloadSound);
        }

        public void PlayEmptySound()
        {
            audioSource.PlayOneShot(emptySound);
        }
    }
}
