using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public PlayerInput PlayerInput;
        public Inputs Inputs;
        
        public PlayerAnimator PlayerAnimator;
        public FirstPersonController FirstPersonController;
        public PlayerCombat PlayerCombat;

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
        }

        private void Update()
        {
            PlayerCombat.HandleCombat();
            PlayerAnimator.HandleAnimator();
        }

        private void LateUpdate()
        {
            Inputs.SetInputs();
        }
    }
}
