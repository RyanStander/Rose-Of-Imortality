using System;
using Characters;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public class EnemyManager : CharacterManager
    {
        public EnemyAnimator EnemyAnimator;
        public EnemyCombat EnemyCombat;
        public EnemyController EnemyController;
        public CharacterHealth CharacterHealth;
        public Transform PlayerTransform;

        public float SightRange = 30, SightAngle = 35;
        public float AudibleRange = 20;
        public float AttackRange =12;

        private void OnValidate()
        {
            if (EnemyAnimator == null)
                EnemyAnimator = GetComponentInChildren<EnemyAnimator>();

            if (EnemyCombat == null)
                EnemyCombat = GetComponentInChildren<EnemyCombat>();
            
            if (EnemyController == null)
                EnemyController = GetComponentInChildren<EnemyController>();
            
            if (CharacterHealth == null)
                CharacterHealth = GetComponentInChildren<CharacterHealth>();
        }

        private void Awake()
        {
            if (PlayerTransform == null)
                PlayerTransform = GameObject.Find("PlayerCapsule").transform;
        }

        private void Update()
        {
            if (CharacterHealth.IsDead)
            {
                EnemyController.StopMoving();
                return;
            }

            EnemyController.HandleStates();
            EnemyAnimator.HandleAnimator();
        }
    }
}
