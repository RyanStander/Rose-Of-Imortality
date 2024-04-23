using UnityEngine;
using UnityEngine.Serialization;

namespace Characters
{
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] protected Animator Animator;
        
        private void OnValidate()
        {
            GetComponents();
        }
        
        protected virtual void GetComponents()
        {
            if (Animator == null)
                Animator = GetComponent<Animator>();
        }
    }
}
