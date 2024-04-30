using System;
using UnityEngine;

namespace Characters
{
    public class EnableCharacterRagdoll : MonoBehaviour
    {
        [SerializeField] private Rigidbody mainRigidbody;
        [SerializeField] private CapsuleCollider mainCollider;
        private void OnValidate()
        {
            DisableRagdoll();
            
            //enable all ragdoll components
            var colliders = GetComponentsInChildren<Collider>();
            foreach (var collider in colliders)
            {
                collider.enabled = true;
            }
        }
        
        public void DisableRagdoll()
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();

            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = true;
            }
        }

        public void EnableRagdoll()
        {
            var rigidbodies = GetComponentsInChildren<Rigidbody>();
            GetComponent<Animator>().enabled = false;

            foreach (var rigidbody in rigidbodies)
            {
                rigidbody.isKinematic = false;
            }
            
            mainCollider.enabled = false;
        }
        
        public void Push(Vector3 direction, float force)
        {
            mainRigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }
}
