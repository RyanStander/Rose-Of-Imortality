using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        public NavMeshAgent agent;

        public Transform player;

        public LayerMask whatIsGround, whatIsPlayer;

        #region Patrolling

        public Vector3 walkPoint;
        private bool walkPointSet;
        public float walkPointRange;

        #endregion

        #region Attacking

        public float timeBetweenAttacks;
        private bool alreadyAttacked;

        #endregion

        #region States

        public float sightRange, attackRange;
        public bool playerInSightRange, playerInAttackRange;

        #endregion

        private void OnValidate()
        {
            if (agent == null)
                agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            if (player == null)
                player = GameObject.Find("PlayerCapsule").transform;
        }

        private void Update()
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange)
                Patrolling();
            if (playerInSightRange && !playerInAttackRange)
                ChasePlayer();
            if (playerInSightRange && playerInAttackRange)
                AttackPlayer();
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
            float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
            float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,
                transform.position.z + randomZ);

            if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
                walkPointSet = true;
        }

        private void ChasePlayer()
        {
            agent.SetDestination(player.position);
        }

        private void AttackPlayer()
        {
            agent.SetDestination(transform.position);

            //prevent look at from changing the x and z rotation
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

            if (!alreadyAttacked)
            {
                // Attack code here

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
