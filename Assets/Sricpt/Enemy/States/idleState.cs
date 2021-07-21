using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  SG
{
    public class idleState : State
    {
        public PursueTargetState pursueTargetState;
        public LayerMask detectionLayer;
        public override State Tick(EnemyManager enemyManager, EnemyStates enemyStats, EnemyAninmatorManager enemyAninmatorManager)
        {
            //look for a potential target
            //Switch to the pursue target state if target is found
            //if not return this state
            if(enemyStats.isDead)
                return null; 
            return HandleDectection(enemyManager);
        }
        public State HandleDectection(EnemyManager enemyManager)
        {
            #region Handle Enemy Target Detection
            Collider[] colloders = Physics.OverlapSphere(transform.position,enemyManager.detectionRadius, detectionLayer);
            for (int i = 0; i < colloders.Length; i++)
            {
                CharcterStats charcterStats = colloders[i].transform.GetComponent<CharcterStats>();
                if(charcterStats!=null)
                {
                    //check for Team ID
                    Vector3 targetDirection = charcterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection,transform.forward);
                    if(viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        enemyManager.currentTarget = charcterStats;
                    }
                }
            }
            #endregion
            #region Handle Switching To Next State
            if(enemyManager.currentTarget != null)
            {
                return pursueTargetState;
            }
            else
            {
                return this;
            }
            #endregion
        }
    }
}
