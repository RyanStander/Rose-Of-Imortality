using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private EnemyManager enemyManager;

        [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

        [SerializeField] private float playerPositionCheckIntervals = 0.5f;
        private Vector3 lastPlayerPosition;
        private bool stoppedMoving;

        #region Patrolling

        [SerializeField] private Vector3 walkPoint;
        private bool walkPointSet;
        [SerializeField] private float walkPointRange;

        #endregion

        #region Attacking

        [SerializeField] private float timeBetweenAttacks;
        private bool alreadyAttacked;

        #endregion

        #region States

        [FormerlySerializedAs("playerInSightRange")] [SerializeField]
        private bool playerInAudibleRange;

        [SerializeField] private bool playerInAttackRange;

        #endregion

        private void OnValidate()
        {
            if (agent == null)
                agent = GetComponent<NavMeshAgent>();

            if (enemyManager == null)
                enemyManager = GetComponentInParent<EnemyManager>();
        }

        public void HandleStates()
        {
            playerInAudibleRange = Physics.CheckSphere(transform.position, enemyManager.AudibleRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, enemyManager.AttackRange, whatIsPlayer);

            //If player is in neither audible range, attack range or sight cone, then patrol
            if (!playerInAudibleRange && !playerInAttackRange && !PlayerInSightCone())
                Patrolling();
            //if the player is in audible range or in sight cone but not in attack range, chase the player
            else if ((playerInAudibleRange || (PlayerInSightCone() && enemyManager.EnemyCombat.CanSeePlayer())) &&
                     !playerInAttackRange)
                ChasePlayer();
            //if the player is in audible range and in attack range, attack the player
            else if (playerInAudibleRange && playerInAttackRange) AttackPlayer();

            enemyManager.EnemyAnimator.SetAnimatorForwardSpeed(agent.velocity.magnitude / agent.speed);
        }

        private void Start()
        {
            lastPlayerPosition = enemyManager.PlayerTransform.position;
            StartCoroutine(UpdateLastPlayerPosition());
        }

        private IEnumerator UpdateLastPlayerPosition()
        {
            while (!enemyManager.CharacterHealth.IsDead)
            {
                lastPlayerPosition = enemyManager.PlayerTransform.position;
                yield return new WaitForSeconds(playerPositionCheckIntervals);
            }
        }

        public void StopMoving()
        {
            if (stoppedMoving)
                return;

            agent.SetDestination(transform.position);
            agent.velocity = Vector3.zero;
            agent.enabled = false;
            stoppedMoving = true;
        }

        private void Patrolling()
        {
            if (!walkPointSet)
                SearchWalkPoint();
            else
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }

        private void SearchWalkPoint()
        {
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,
                transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
                walkPointSet = true;
        }

        private void ChasePlayer()
        {
            agent.SetDestination(lastPlayerPosition);
        }

        private void AttackPlayer()
        {
            agent.SetDestination(transform.position);

            //prevent look at from changing the x and z rotation
            transform.LookAt(new Vector3(enemyManager.PlayerTransform.position.x, transform.position.y,
                enemyManager.PlayerTransform.position.z));

            if (!alreadyAttacked && !enemyManager.EnemyAnimator.isReloading)
            {
                enemyManager.EnemyCombat.DetermineIfHit(lastPlayerPosition);

                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
        }

        private void ResetAttack()
        {
            alreadyAttacked = false;
        }

        private bool PlayerInSightCone()
        {
            // Calculate the direction to the player
            Vector3 directionToPlayer = enemyManager.PlayerTransform.position - transform.position;

            // Check if the player is within the sight range
            if (directionToPlayer.magnitude <= enemyManager.SightRange)
            {
                // Calculate the angle between the enemy's forward direction and the direction to the player
                float angle = Vector3.Angle(transform.forward, directionToPlayer);

                // Check if the angle is within the cone of vision
                if (angle <= enemyManager.SightAngle / 2f)
                {
                    // Player is within the cone of vision
                    return true;
                }
            }

            // Player is not within the cone of vision
            return false;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, enemyManager.AttackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, enemyManager.AudibleRange);
        }
    }
}
