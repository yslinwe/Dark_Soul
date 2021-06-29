using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class attackState : State
    {
        public combaStanceState combaStanceState;

        public EnemyAttackAction []enemyAttackActions;
        public EnemyAttackAction currentAttack;
        public override State Tick(EnemyManager enemyManager, EnemyStates enemyStats, EnemyAninmatorManager enemyAninmatorManager)
        {
            //Select one of our many attack based on attack scores
            //if the selected attack is not able to be used because of bad angle or distance , select a new attack
            //if the attack is viable, stop our movement and attack our target 
            //set our recovery timer to the attacks reovery time
            //return the comba stance state
           return AttackTarget(enemyManager,enemyAninmatorManager);
        }
        #region Attacks
        private State AttackTarget(EnemyManager enemyManager,EnemyAninmatorManager enemyAninmatorManager)
        {
            // Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
            // float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
            if(enemyManager.isPerformingaction)
                return combaStanceState;
            if(currentAttack != null)
            {
                if(enemyManager.distanceFromTarget < currentAttack.minmumDistanceNeededToAttack)
                {
                    return this;
                }
                else if(enemyManager.distanceFromTarget < currentAttack.maxmumDistanceNeededToAttack)
                {
                    if(enemyManager.viewableAngle <= currentAttack.maximumAttackAngle 
                    && enemyManager.viewableAngle >= currentAttack.minimunAttackAngle)
                    {
                        if(enemyManager.currentRecoveryTime <= 0 && enemyManager.isPerformingaction == false)
                        {
                            enemyAninmatorManager.anim.SetFloat("Vertical",0,0.1f,Time.deltaTime);
                            enemyAninmatorManager.anim.SetFloat("Horizontal",0,0.1f,Time.deltaTime);
                            enemyAninmatorManager.PlayTargetAnimation(currentAttack.actionAnimation,true);
                            enemyManager.isPerformingaction = true;
                            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                            currentAttack = null;
                            return combaStanceState;
                        }
                    }
                }
            }
            else
            {
                GetNewAttack(enemyManager);
            }
            return combaStanceState;
        }
        private void GetNewAttack(EnemyManager enemyManager)
        {
            Vector3 targetsDirection = enemyManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetsDirection,transform.forward);
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position,transform.position);

            int maxScore = 0;
            for (int i = 0; i < enemyAttackActions.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttackActions[i];
                if(enemyManager.distanceFromTarget <= enemyAttackAction.maxmumDistanceNeededToAttack
                && enemyManager.distanceFromTarget > enemyAttackAction.minmumDistanceNeededToAttack)
                {
                    if(viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && viewableAngle >= enemyAttackAction.minimunAttackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;
                    }
                }
            }
            int randomValue = Random.Range(0,maxScore);     
            int temporaryScore = 0;
            for (int i = 0; i < enemyAttackActions.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttackActions[i];
                if(enemyManager.distanceFromTarget <= enemyAttackAction.maxmumDistanceNeededToAttack
                && enemyManager.distanceFromTarget > enemyAttackAction.minmumDistanceNeededToAttack)
                {
                    if(viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && viewableAngle >= enemyAttackAction.minimunAttackAngle)
                    {
                        if(currentAttack!=null)
                            return;
                        temporaryScore += enemyAttackAction.attackScore;
                        if(temporaryScore > randomValue)
                        {
                            currentAttack = enemyAttackAction;
                        }
                    }
                }
            }   
        }
        #endregion
    }
}