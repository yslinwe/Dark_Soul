using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class combaStanceState : State
    {
        public attackState attackState;
        public PursueTargetState pursueTargetState;
        public override State Tick(EnemyManager enemyManager, EnemyStates enemyStats, EnemyAninmatorManager enemyAninmatorManager)
        {
            //check for attack range
            //potentially circle player or walk around them
            //if in attack range return attack State
            //if we are in a cool down after attacking, return this state and continue circling player
            //if the player runs out of range return the pursuetarget state
            if(enemyManager.isPerformingaction)
            {
                enemyAninmatorManager.anim.SetFloat("Vertical",0,0.1f,Time.deltaTime);
            }
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            
            if(enemyManager.currentRecoveryTime <=0 && distanceFromTarget <= enemyManager.maxmunAttackingRange)
            {
                return attackState;
            }
            else if(distanceFromTarget > enemyManager.maxmunAttackingRange)
            {
               return pursueTargetState;
            }
            else
            {
                return this;
            }
        }
    }
}