using System;
using Characters;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : CharacterAnimator
    {
        [SerializeField] private PlayerManager playerManager;
        private bool isPlayingLookAtArm;

        #region Cached Properties
        
        private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
        private static readonly int IsViewingLifeTime = Animator.StringToHash("isViewingLifeTime");

        #endregion

        public override void HandleAnimator()
        {
            base.HandleAnimator();
            
            Animator.SetBool(IsViewingLifeTime,playerManager.Inputs.checkArm);

            if (isPlayingLookAtArm)
                 isPlayingLookAtArm=Animator.GetBool(IsViewingLifeTime);
        }

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
        
        public void CheckArm()
        {
            if (isPlayingLookAtArm)
                return;
            
            Animator.CrossFade("Look at Arm",0.2f,-1,0);
            isPlayingLookAtArm = true;
        }
    }
}
