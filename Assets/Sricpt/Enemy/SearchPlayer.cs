using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public enum FSMState
    {
        Wander,     //随机游荡状态
        Seek,       //搜索状态
        Chase,      //追踪状态
        Attack,     //攻击状态
        Dead,       //死亡状态
    }
    public class SearchPlayer : MonoBehaviour
    {

        // Start is called before the first frame update
        public Transform playTransform;
        EnemyAnimatorHandler animatorHandler;
        EnemyAttack enemyAttack;
        EnemyStates enemyStates;
        EnemyWeaponSlotManager enemyWeaponSlotManager;
        PlayerStates playerStates;
        public WeaponItem currentWeaponItem;
        public FSMState currentState;
        public FSMState lastState;
        public int SensorInterval = 2;
        float senserTimer = 0;
        float step = 0;
        [Header("Ground & Air Detection Stats")]
        [SerializeField]
        float groundDetectionRayStartPoint = 0.5f;
        [SerializeField]
        float minimunDistanceNeededToBeginFall = 0.5f;
        [SerializeField]
        float groundDirectionRayDistance = 0.2f;
        LayerMask ignoreForGroundCheck;
        Vector3 targetPositon;
        Transform myTransform;
        Vector3 myTransformPos;
        void Start()
        {
            animatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
            enemyAttack = GetComponent<EnemyAttack>();
            enemyStates = GetComponent<EnemyStates>();
            enemyWeaponSlotManager = GetComponentInChildren<EnemyWeaponSlotManager>();
            enemyWeaponSlotManager.LoadWeaponOnSlot(false);
            ignoreForGroundCheck = ~(1<<8 | 1<<11);
            myTransform = transform;
            myTransformPos = transform.position;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 1);
        }
        // Update is called once per frame
        void Update()
        {
            HandleLand();
            if(enemyStates.currentHealth==0)
            {
                currentState = FSMState.Dead; 
                return;
            }
            if (playTransform == null)
            {
                currentState = FSMState.Seek;
            }
            else
            { 
                senserTimer += Time.deltaTime;
                Vector3 playerPosition = new Vector3(playTransform.position.x, 0, playTransform.position.z);
                Vector3 position = new Vector3(transform.position.x, 0, transform.position.z);
                if (Vector3.Distance(playerPosition, position) < 1.5f)
                    if (lastState != FSMState.Attack)
                    {
                        currentState = FSMState.Attack;
                    }
                    else
                        currentState = FSMState.Wander;
                else
                    currentState = FSMState.Chase;
                if (playerStates != null)
                    if (playerStates.currentHealth <= 0)
                    {
                        currentState = FSMState.Wander;
                    }
            }
            lastState = currentState;
            if (currentState != FSMState.Chase)
                step = 0;
        }
        void FixedUpdate()
        {
            FSMUpdate();
        }

        void FSMUpdate()
        {
            float delta = Time.fixedDeltaTime;
            switch (currentState)
            {
                case FSMState.Wander:
                    HandleIdle();
                    break;
                case FSMState.Seek:
                    HandleSearchPlayer();
                    break;
                case FSMState.Chase:
                    HandleMovement(delta);
                    break;
                case FSMState.Attack:
                    HandleAttack();
                    break;
            }
            
        }
        private void HandleSearchPlayer()
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 2f))
            {
                if (hit.collider.tag == "Player")
                {
                    playTransform = hit.collider.gameObject.transform;
                    playerStates = playTransform.GetComponent<PlayerStates>();
                }
            }
            HandleIdle();
        }
        private void HandleRotation()
        {
            Quaternion PlayerRotation = Quaternion.LookRotation(playTransform.position - transform.position);
            transform.rotation = PlayerRotation;
        }
        public void HandleMovement(float delta)
        {
            if (animatorHandler.isInteracting)
                return;
            //Vector3 projectedVelocity = Vector3.ProjectOnPlane(transform.forward*5, normalVector);
            Mathf.SmoothDamp(0.0f, 1.0f, ref step, 1);
            GetComponent<Rigidbody>().velocity = transform.forward * 3 * step;
            animatorHandler.UpdateAnimatorValues(step, 0, false);
            HandleRotation();
        }
        public void HandleAttack()
        {
            if (animatorHandler.isInteracting)
                return;
            enemyAttack.HandleLightAttack(currentWeaponItem);
        }
        public void HandleIdle()
        {
            if (animatorHandler.isInteracting)
                return;
            animatorHandler.UpdateAnimatorValues(0, 0, false);
        }
        public void HandleLand()
        {         
            RaycastHit hit;
            Vector3 origin = transform.position;
            origin.y += groundDetectionRayStartPoint;

            targetPositon = transform.position;
            Debug.DrawRay(origin,-Vector3.up * minimunDistanceNeededToBeginFall,Color.red,0.1f,false);
            if(Physics.Raycast(origin,-Vector3.up,out hit,minimunDistanceNeededToBeginFall))
            {
                Vector3 tp = hit.point;
                targetPositon.y = tp.y;
            }
            transform.position = targetPositon;
            print(targetPositon);
        }
    }
}
