using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private EnemyManager enemyManager;

        [SerializeField] private LayerMask whatIsGround, whatIsPlayer;

        [SerializeField]private float playerPositionCheckIntervals=0.5f;
        private Vector3 lastPlayerPosition;

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

        [SerializeField] private float sightRange, attackRange;
        [SerializeField] private bool playerInSightRange, playerInAttackRange;

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
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange)
                Patrolling();
            if (playerInSightRange && !playerInAttackRange)
                ChasePlayer();
            if (playerInSightRange && playerInAttackRange)
                AttackPlayer();
            
            enemyManager.EnemyAnimator.SetAnimatorForwardSpeed(agent.velocity.magnitude/agent.speed);
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
            transform.LookAt(new Vector3(enemyManager.PlayerTransform.position.x, transform.position.y, enemyManager.PlayerTransform.position.z));

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

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, sightRange);
        }
    }
}
