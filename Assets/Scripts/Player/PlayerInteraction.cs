using System;
using Interactables;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// When the player enters the trigger zone of an interactable object, the player can interact with it
    /// </summary>
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        private BaseInteractable currentInteractable;

        private void OnValidate()
        {
            if (playerManager == null)
                playerManager = GetComponent<PlayerManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                currentInteractable = other.GetComponent<BaseInteractable>();
                if (currentInteractable != null)
                {
                    currentInteractable.EnterRange();
                }
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Interactable"))
            {
                currentInteractable = null;
            }
        }
        
        public void HandleInteraction()
        {
            if (currentInteractable != null&& playerManager.Inputs.interact)
            {
                currentInteractable.Interact();
                playerManager.Inputs.interact = false;
            }
        }
    }
}
