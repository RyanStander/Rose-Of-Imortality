using System;
using UnityEngine;

namespace Audio
{
    public class DisableIfNotPlaying : MonoBehaviour
    {
        [SerializeField]private AudioSource audioSource;

        private void OnValidate()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (!audioSource.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
