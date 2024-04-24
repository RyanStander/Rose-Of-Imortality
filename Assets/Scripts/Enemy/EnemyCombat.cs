using System;
using Characters;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyCombat : CharacterCombat
    {
        [SerializeField] private EnemyManager enemyManager;
        private Quaternion originalRaycastTransformRotation;
        [SerializeField] private float missedShotRotationRange = 10f;
        
        [SerializeField, Range(0f, 1f),
         Tooltip(
             "Percentage chance that the enemy will hit the player. A value of 0.2 means the enemy has a 20% chance to hit the player.")]
        private float chanceToHit = 0.2f;

        protected override void GetComponents()
        {
            base.GetComponents();

            if (enemyManager == null)
                enemyManager = GetComponentInParent<EnemyManager>();
        }

        private void Start()
        {
            originalRaycastTransformRotation = RaycastOriginTransform.localRotation;
        }

        public void DetermineIfHit(Vector3 playerPosition)
        {
            // Calculate a random value to determine the accuracy of the shot
            float accuracy = Random.Range(0f, 1f);

            // Check if the adjusted accuracy is enough to hit the player
            if (chanceToHit > accuracy)
            {
                // The enemy hits the player
                Vector3 directionToPlayer = (playerPosition - transform.position).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);
                RaycastOriginTransform.rotation = targetRotation;
            }
            else
            {
                // Attack missed
                float randomYRotation = Random.Range(-missedShotRotationRange, missedShotRotationRange);
                RaycastOriginTransform.Rotate(Vector3.up, randomYRotation);

                float randomXRotation = Random.Range(-missedShotRotationRange, missedShotRotationRange);
                RaycastOriginTransform.Rotate(Vector3.right, randomXRotation);
            }

            Fire();
        }
    }
}
