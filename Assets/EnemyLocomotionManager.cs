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

        private void Awake() {
            enemyManager = GetComponent<EnemyManager>();
            enemyAninmatorManager = GetComponentInChildren<EnemyAninmatorManager>();
        }
    }
}
