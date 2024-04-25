using System;
using Characters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerManager : CharacterManager
    {
        public PlayerInput PlayerInput;
        public Inputs Inputs;
        
        public PlayerAnimator PlayerAnimator;
        public FirstPersonController FirstPersonController;
        public PlayerCombat PlayerCombat;
        public PlayerHealth PlayerHealth;
        public PlayerLifetime PlayerLifetime;
        public PlayerInteraction PlayerInteraction;

        private void OnValidate()
        {
            if (PlayerInput == null)
                PlayerInput = GetComponent<PlayerInput>();
            
            if (Inputs == null)
                Inputs = GetComponent<Inputs>();
            
            if (PlayerAnimator == null)
                PlayerAnimator = GetComponentInChildren<PlayerAnimator>();

            if (FirstPersonController == null)
                FirstPersonController = GetComponent<FirstPersonController>();
            
            if (PlayerCombat == null)
                PlayerCombat = GetComponent<PlayerCombat>();
            
            if (PlayerHealth == null)
                PlayerHealth = GetComponentInChildren<PlayerHealth>();
            
            if (PlayerLifetime == null)
                PlayerLifetime = GetComponent<PlayerLifetime>();
            
            if (PlayerInteraction == null)
                PlayerInteraction = GetComponent<PlayerInteraction>();
        }

        private void Update()
        {
            PlayerCombat.HandleCombat();
            PlayerAnimator.HandleAnimator();
            PlayerInteraction.HandleInteraction();
        }

        private void LateUpdate()
        {
            Inputs.SetInputs();
        }
    }
}
