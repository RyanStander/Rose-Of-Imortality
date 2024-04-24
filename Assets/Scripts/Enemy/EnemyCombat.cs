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

        [FormerlySerializedAs("accuracy")]
        [FormerlySerializedAs("accuracyDeduction")]
        [SerializeField, Range(-1f, 1f),
         Tooltip(
             "The higher the value, the more likely they are to miss, 1 means they will always miss, 0 means they have an equal chance of hitting or missing, -1 means they will always hit.")]
        private float baseAccuracy = 0.2f;

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

            // Modify this value based on the enemy's accuracy
            float adjustedAccuracy = Mathf.Clamp(accuracy - baseAccuracy, 0f, 1f);

            // Check if the adjusted accuracy is enough to hit the player
            if (adjustedAccuracy > 0.5f)
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
