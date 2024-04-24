using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class WeaponSfx : MonoBehaviour
    {
        [SerializeField] private Transform audioSourceTransform;
        [SerializeField] private GameObject audioSourcePrefab;
        [SerializeField] private AudioClip fireSound;
        [SerializeField] private AudioClip reloadSound;
        [SerializeField] private AudioClip emptySound;

        private List<AudioSource> audioSourcePool = new List<AudioSource>();
        
        private void PlayAudioSourcePool(AudioClip audioClip)
        {
            foreach (var source in audioSourcePool)
            {
                if (!source.isPlaying)
                {
                    source.gameObject.SetActive(true);
                    source.clip = audioClip;
                    source.Play();
                    return;
                }
            }

            var newAudioSource = Instantiate(audioSourcePrefab, audioSourceTransform).GetComponent<AudioSource>();
            newAudioSource.clip = audioClip;
            newAudioSource.Play();
            audioSourcePool.Add(newAudioSource);
        }

        public void PlayFireSound()
        {
            PlayAudioSourcePool(fireSound);
        }

        public void PlayReloadSound()
        {
            PlayAudioSourcePool(reloadSound);
        }

        public void PlayEmptySound()
        {
            PlayAudioSourcePool(emptySound);
        }
    }
}
