using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace SG
{
    public class EnemyLocomotionManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAninmatorManager enemyAninmatorManager;
        public CapsuleCollider CharacterCollider;
        public CapsuleCollider CharacterColliderBlocker;
        private void Awake() {
            enemyManager = GetComponent<EnemyManager>();
            enemyAninmatorManager = GetComponentInChildren<EnemyAninmatorManager>();
            Physics.IgnoreCollision(CharacterCollider,CharacterColliderBlocker,true);
        }
    }
}
