using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private Animator animator;
        
        public bool isReloading  { get; private set; }

        #region Cached Properties

        private static readonly int Forward = Animator.StringToHash("forward");
        private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
        private static readonly int IsReloading = Animator.StringToHash("isReloading");

        #endregion
        

        private void OnValidate()
        {
            if (playerManager == null)
                playerManager = GetComponentInParent<PlayerManager>();
            
            if (animator == null)
                animator = GetComponent<Animator>();
        }
        
        public void HandleAnimator()
        {
            isReloading=animator.GetBool(IsReloading);
        }
        
        public void SetAnimatorForwardSpeed(float speed)
        {
            animator.SetFloat(Forward, speed);
        }
        
        public void SetGrounded(bool isGrounded)
        {
            animator.SetBool(IsGrounded, isGrounded);
        }
        
        public void Fire()
        {
            var randomFire = UnityEngine.Random.Range(1,4).ToString();
            
            animator.Play("Fire"+randomFire);
        }
        
        public void EmptyReload()
        {
            animator.Play("ReloadEmpty");
        }
        
        public void TacticalReload()
        {
            animator.Play("ReloadTactical");
        }
    }
}
