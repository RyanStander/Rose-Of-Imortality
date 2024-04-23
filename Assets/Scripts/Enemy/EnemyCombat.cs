using Characters;
using UnityEngine;

namespace Enemy
{
    public class EnemyCombat : CharacterCombat
    {
        [SerializeField] private EnemyManager enemyManager;
        
        protected override void GetComponents()
        {
            base.GetComponents();
            
            if (enemyManager == null)
                enemyManager = GetComponentInParent<EnemyManager>();
        }
    }
}
