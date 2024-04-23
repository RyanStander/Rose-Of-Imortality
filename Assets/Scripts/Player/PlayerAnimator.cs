using System;
using Characters;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : CharacterAnimator
    {
        [SerializeField] private PlayerManager playerManager;
        
        public bool isReloading  { get; private set; }

        #region Cached Properties

        private static readonly int Forward = Animator.StringToHash("forward");
        private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
        private static readonly int IsReloading = Animator.StringToHash("isReloading");

        #endregion

        protected override void GetComponents()
        {
            base.GetComponents();
            
            if (playerManager == null)
                playerManager = GetComponentInParent<PlayerManager>();
        }

        public void HandleAnimator()
        {
            isReloading=Animator.GetBool(IsReloading);
        }
        
        public void SetAnimatorForwardSpeed(float speed)
        {
            Animator.SetFloat(Forward, speed);
        }
        
        public void SetGrounded(bool isGrounded)
        {
            Animator.SetBool(IsGrounded, isGrounded);
        }
        
        public void Fire()
        {
            var randomFire = UnityEngine.Random.Range(1,4).ToString();
            
            //animator.Play("Fire"+randomFire);
            Animator.CrossFade("Fire"+randomFire,0.1f,-1,0);
        }
        
        public void EmptyReload()
        {
            Animator.Play("ReloadEmpty");
        }
        
        public void TacticalReload()
        {
            Animator.Play("ReloadTactical");
        }
    }
}
