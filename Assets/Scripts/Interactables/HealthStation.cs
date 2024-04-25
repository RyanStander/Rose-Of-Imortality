using System;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    /// <summary>
    /// Player can interact with the station to heal at the cost of life time
    /// </summary>
    public class HealthStation : BaseInteractable
    {
        [SerializeField] private int healAmount = 50;
        [SerializeField] private float lifeTime = 3600f;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<AudioClip> enterRangeSound;
        [SerializeField] private List<AudioClip> interactSound;

        private void OnValidate()
        {
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
        }

        public override void Interact()
        {
            Heal();
            PlayRandomSound(interactSound);
        }

        public override void EnterRange()
        {
            PlayRandomSound(enterRangeSound);
        }
        
        private void PlayRandomSound(List<AudioClip> clips)
        {
            if (clips.Count > 0)
            {
                var randomIndex = UnityEngine.Random.Range(0, clips.Count);
                audioSource.PlayOneShot(clips[randomIndex]);
            }
        }

        private void Heal()
        {
            var playerManager = FindObjectOfType<Player.PlayerManager>();
            
            if (playerManager != null)
            {
                playerManager.PlayerHealth.Heal(healAmount);
                playerManager.PlayerLifetime.SpendTime(lifeTime);
            }
        }
    }
}
