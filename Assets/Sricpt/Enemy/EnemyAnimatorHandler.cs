using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class EnemyAnimatorHandler : MonoBehaviour
    {
        public Animator anim;
        InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
        PlayerManager playerManager;
        private Rigidbody rigid;
        EnemyStates enemyStates;
        public int vertical = 0 ;
        int horizontal;
        public bool canRotate;
        public bool isInteracting;
        void Start()
        {
            anim = GetComponent<Animator>();
            rigid = GetComponentInParent<Rigidbody>();
            //inputHandler = GetComponentInParent<InputHandler>();
            //playerManager = GetComponentInParent<PlayerManager>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            enemyStates = GetComponentInParent<EnemyStates>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }
        void Update()
        {
            isInteracting = anim.GetBool("isInteracting");
        }
        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            #region Vertical
            float v = 0;
            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion
            #region Horizontal
            float h = 0;
            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion
            if (isSprinting)
            {
                v = 2;
                h = horizontalMovement;
            }
            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }
        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            if (targetAnim == null)
                Debug.LogError("targetAnim is null");
            if (targetAnim == "")
                Debug.LogWarning("targetAnim is Empty");
            if (targetAnim != "" && targetAnim != null)
            {
                anim.applyRootMotion = isInteracting;
                anim.SetBool("isInteracting", isInteracting);
                anim.CrossFade(targetAnim, 0.2f);
            }
        }
        public void CanRotate()
        {
            canRotate = true;
        }
        public void StopRotation()
        {
            canRotate = false;
        }
        public void EnaleCombo()
        {
            anim.SetBool("canDoCombo", true);
        }
        public void DisableCombo()
        {
            anim.SetBool("canDoCombo", false);
        }
        private void OnAnimatorMove()
        {
            if (isInteracting == false||rigid == null)
                return;
            float delta = Time.deltaTime;
            rigid.drag = 0;
            Vector3 deltaPostion = anim.deltaPosition;
            deltaPostion.y = 0;
            Vector3 velocity = deltaPostion / delta;
            rigid.velocity = velocity;
        }
    }

}
