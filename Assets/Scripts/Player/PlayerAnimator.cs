using System;
using Characters;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : CharacterAnimator
    {
        [SerializeField] private PlayerManager playerManager;

        #region Cached Properties
        
        private static readonly int IsGrounded = Animator.StringToHash("isGrounded");

        #endregion

        protected override void GetComponents()
        {
            base.GetComponents();
            
            if (playerManager == null)
                playerManager = GetComponentInParent<PlayerManager>();
        }
        
        public void SetGrounded(bool isGrounded)
        {
            Animator.SetBool(IsGrounded, isGrounded);
        }
    }
}
