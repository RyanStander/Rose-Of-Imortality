using System;
using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class BulletTrail : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private float speed = 2000f;
        [SerializeField] private float lifeTime = 0.5f;

        private void OnValidate()
        {
            if (rigidbody == null)
                rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            StartCoroutine(DestroyBulletTrail());
            MoveBulletTrail();
        }

        private void MoveBulletTrail()
        {
            rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
        }

        private IEnumerator DestroyBulletTrail()
        {
            yield return new WaitForSeconds(lifeTime);
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision other)
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
}
