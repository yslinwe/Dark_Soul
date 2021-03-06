using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  SG
{
    public class EnemyAninmatorManager : AnimatorManager
    {
        EnemyManager enemyManager;
        private void Awake() {
            anim = GetComponent<Animator>();
            enemyManager = GetComponentInParent<EnemyManager>();
        }
        public void EnaleCombo()
        {
            anim.SetBool("canDoCombo",true);
        }
        public void DisableCombo()
        {
            anim.SetBool("canDoCombo",false);
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
