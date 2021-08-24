using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  SG
{
    public class EnemyAninmatorManager : AnimatorManager
    {
        EnemyManager enemyManager;
        EnemyStates enemyStates;
        private void Awake() {
            anim = GetComponent<Animator>();
            enemyManager = GetComponentInParent<EnemyManager>();
            enemyStates = GetComponentInParent<EnemyStates>();
        }
        public void EnaleCombo()
        {
            anim.SetBool("canDoCombo",true);
        }
        public void DisableCombo()
        {
            anim.SetBool("canDoCombo",false);
        }
        public override void TakeCriticalDamageAnimationEvent()
        {
            enemyStates.TakeDamageNoAnimation(enemyManager.pendingCriticalDamage);
            enemyManager.pendingCriticalDamage = 0;
        }
        private void OnAnimatorMove() {
            float delta = Time.deltaTime;
            enemyManager.enemyRigidBody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyManager.enemyRigidBody.velocity = velocity;
        }
    }
}
