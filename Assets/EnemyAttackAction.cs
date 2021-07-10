using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  SG
{
    [CreateAssetMenu(menuName = "A.I/Enemy Actions/Attack Action")]
    public class EnemyAttackAction : EnemyAction
    {
       public int attackScore = 3;
       public float recoveryTime = 2;

       public float maximumAttackAngle = 35;
       public float minimunAttackAngle = -35;

        public float minmumDistanceNeededToAttack = 0;
        public float maxmumDistanceNeededToAttack = 3;
    }
}