using System;
using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class BulletTrail : MonoBehaviour
    {
        [SerializeField] private float speed = 20f;
        [SerializeField] private float lifeTime = 0.5f;

        private void Start()
        {
            StartCoroutine(DestroyBulletTrail());
        }
        
        private void FixedUpdate()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        
        private IEnumerator DestroyBulletTrail()
        {
            yield return new WaitForSeconds(lifeTime);
          
            Destroy(gameObject);
        }
    }
}
