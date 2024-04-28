using UnityEngine;
using UnityEngine.Serialization;

namespace Characters
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] protected Animator Animator;

        #region Cached Properties

        protected static readonly int Forward = Animator.StringToHash("forward");
        protected static readonly int IsReloading = Animator.StringToHash("isReloading");

        #endregion
        
        public bool isReloading  { get; private set; }
        
        private void OnValidate()
        {
            GetComponents();
        }
        
        protected virtual void GetComponents()
        {
            if (Animator == null)
                Animator = GetComponent<Animator>();
        }
        
        public virtual void HandleAnimator()
        {
            isReloading=Animator.GetBool(IsReloading);
        }
        
        public void SetAnimatorForwardSpeed(float speed)
        {
            Animator.SetFloat(Forward, speed);
        }
        
        public void Fire(int fireVariationsCount)
        {
            var randomFire = Random.Range(1,fireVariationsCount).ToString();
            
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
