using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class AmbushState : State
    {
        public bool isSleeping;
        public float detectionRadius = 2;
        public string sleepAnimation;
        public string wakeAnimation;
        public LayerMask detectionLayer;
        public PursueTargetState pursueTargetState;
        public override State Tick(EnemyManager enemyManager, EnemyStates enemyStats, EnemyAninmatorManager enemyAninmatorManager)
        {
            if(isSleeping && enemyManager.isInteracting ==false)
            {
                enemyAninmatorManager.PlayTargetAnimation(sleepAnimation, false);
            }
            #region Handle Target Detection
            Collider[] colliders = Physics.OverlapSphere(enemyManager.transform.position, detectionRadius, detectionLayer);
            for (int i = 0; i < colliders.Length; i++)
            {
                CharcterStats charcterStats = colliders[i].transform.GetComponent<CharcterStats>();
                if(charcterStats != null)
                {
                    Vector3 targetsDirection = charcterStats.transform.position - enemyManager.transform.position;
                    float viewableAngle = Vector3.Angle(targetsDirection, enemyManager.transform.forward);
                    if(viewableAngle > enemyManager.minimumDetectionAngle
                    && viewableAngle < enemyManager.maximumDetectionAngle)
                    {
                        enemyManager.currentTarget = charcterStats;
                        isSleeping = false;
                        enemyAninmatorManager.PlayTargetAnimation(wakeAnimation, true);
                    }

                }
            }
            #endregion
            #region Handle State Detection
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
