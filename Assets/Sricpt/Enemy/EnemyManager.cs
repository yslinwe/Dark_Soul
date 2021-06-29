using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace SG
{
    public class EnemyManager : CharacterManager
    {
        EnemyLocomotionManager enemyLocomotionManager;
        EnemyAninmatorManager enemyAninmatorManager;
        EnemyStates enemyStates;
        
        public NavMeshAgent navMeshAgent;
        public State currentState;
        public CharcterStats currentTarget;
        public Rigidbody enemyRigidBody;

        public bool isPerformingaction;
        public float rotateSpeed = 15f;
        public float distanceFromTarget;
        public float maxmunAttackingRange = 1.5f;
        [Header("A.I Settings")]
        public float detectionRadius = 20;
        // The higher and lower respectively angles are,the greater detection FIELD OF VIEW(basically like eye sight)
        public float maximumDetectionAngle = 50;
        public float minimumDetectionAngle = -50;
        public float viewableAngle;
        public float currentRecoveryTime = 0;
        private void Awake() {
            enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
            enemyAninmatorManager = GetComponentInChildren<EnemyAninmatorManager>();
            enemyStates = GetComponent<EnemyStates>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyRigidBody = GetComponent<Rigidbody>();
        }
        private void Start() {
            navMeshAgent.enabled = false;
            enemyRigidBody.isKinematic = false;
        }
        private void Update() {
            HandleRecoveryTimer();
        }
        private void FixedUpdate() {
            HandleStateMachine();
        }
        private void HandleStateMachine()
        {
            if(currentState != null)
            {
                State nextState = currentState.Tick(this, enemyStates, enemyAninmatorManager);
                if(nextState!=null)
                {
                    SwitchToNextState(nextState);
                }
            }
        }
        private void SwitchToNextState(State state)
        {
            currentState = state; 
        }
        private void HandleRecoveryTimer()
        {
            if(currentRecoveryTime > 0)
            {
                currentRecoveryTime -= Time.deltaTime;
            }
            if(isPerformingaction)
            {
                if(currentRecoveryTime <= 0)
                {
                    isPerformingaction = false;
                }
            }
        }
    }

}
