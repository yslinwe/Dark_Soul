using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class PlayerAnimatorManager : AnimatorManager
    {
        InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
        PlayerStates playerStates;
        PlayerManager playerManager;
        int vertical;
        int horizontal;
        public void Initialize()
        {
            anim = GetComponent<Animator>();
            inputHandler = GetComponentInParent<InputHandler>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            playerStates = GetComponentInParent<PlayerStates>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
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
 
        public void CanRotate()
        {
            anim.SetBool("canRotate",true);
        }
        public void StopRotation()
        {
            anim.SetBool("canRotate",false);
        }
        public void EnaleCombo()
        {
            anim.SetBool("canDoCombo",true);
        }
        public void DisableCombo()
        {
            anim.SetBool("canDoCombo",false);
        }
        public void EnableIsInvulerable()
        {
            anim.SetBool("isInvulerable",true);
        }
        public void DisableIsInvulerable()
        {
            anim.SetBool("isInvulerable",false);
        }
        public override void TakeCriticalDamageAnimationEvent()
        {
            playerStates.TakeDamageNoAnimation(playerManager.pendingCriticalDamage);
            playerManager.pendingCriticalDamage = 0;
        }
        private void OnAnimatorMove()
        {
            if (playerManager.isInteracting == false)
                return;
            float delta = Time.deltaTime;
            playerLocomotion.rigidbody.drag = 0;
            Vector3 deltaPostion = anim.deltaPosition;
            deltaPostion.y = 0;
            Vector3 velocity = deltaPostion / delta;
            playerLocomotion.rigidbody.velocity = velocity;
        }
    }

}
