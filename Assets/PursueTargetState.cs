using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace  SG
{
    public class PursueTargetState : State
    {
        public combaStanceState combaStanceState;
        public override State Tick(EnemyManager enemyManager, EnemyStates enemyStats, EnemyAninmatorManager enemyAninmatorManager)
        {
            //Chase the target 
            //If within attack range , return combat stance state
            //if target is out of range, return this state and continue to chase target
            return HandleMoveToTarget(enemyManager, enemyAninmatorManager);
        }
        public State HandleMoveToTarget(EnemyManager enemyManager,EnemyAninmatorManager enemyAninmatorManager)
        {
            if(enemyManager.isPerformingaction)
                return this;
            // Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position,transform.position);
            // if we are performing an action stop our movememnt
             if(enemyManager.distanceFromTarget > enemyManager.maxmunAttackingRange)
            {
                enemyAninmatorManager.anim.SetFloat("Vertical",1,0.1f,Time.deltaTime);
            }
        
            HandleRotateTowardsTarget(enemyManager);
            enemyManager.navMeshAgent.transform.localPosition = Vector3.zero;
            enemyManager.navMeshAgent.transform.localRotation = Quaternion.identity;

            if(enemyManager.distanceFromTarget <= enemyManager.maxmunAttackingRange)
            {
                return combaStanceState;
            }
            else
            {
                return this;
            }
        }
        private void HandleRotateTowardsTarget(EnemyManager enemyManager)
        {
           
            // Rotate maually
            if(enemyManager.isPerformingaction)
            {
                Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
                direction.y =0;
                direction.Normalize();
                if(direction == Vector3.zero)
                {
                    direction = transform.forward;
                }
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,enemyManager.rotateSpeed/Time.deltaTime);

            }
            //Rotate with pathfinding(navmesh)
            else
            {
                Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyManager.enemyRigidBody.velocity;
                enemyManager.navMeshAgent.enabled =true;
                enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRigidBody.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation,enemyManager.navMeshAgent.transform.rotation,enemyManager.rotateSpeed/Time.deltaTime);
            }
        }
    }
}