using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;
        public bool b_Input;
        public bool a_Input;
        public bool y_Input;
        public bool rb_Input;
        public bool rt_Input;
        public bool lb_Input;
        public bool lt_Input;
        public bool jump_Input;
        public bool inventory_Input;
        public bool lockOnInput;
        public bool right_Stick_Right_Input;
        public bool right_Stick_Left_Input;

        public bool d_Pad_Up;
        public bool d_Pad_Down;
        public bool d_Pad_Left;
        public bool d_Pad_Right;
        public bool rollFlag;
        public bool sprintFlag;
        public bool comboFlag;
        public bool lockOnFlag;
        public bool two_handFlag;

        public bool inventoryFlag;
        public float rollInputTimer;
        
        PlayerControls inputActions;
        AnimatorHandler animatorHandler; 
        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;
        PlayerManager playerManager;
        PlayerStates playerStates;
        WeaponSlotManager weaponSlotManager;
        CameraHandler cameraHandler;
        UIManage uiManage;
        Vector2 movementInput;
        Vector2 cameraInput;
        private void Awake() {
            playerAttacker = GetComponentInChildren<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
            playerManager = GetComponent<PlayerManager>();
            playerStates = GetComponent<PlayerStates>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
            uiManage = FindObjectOfType<UIManage>();
            cameraHandler = FindObjectOfType<CameraHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }
        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();

                inputActions.PlayerActions.RB.performed += i => rb_Input = true;
                inputActions.PlayerActions.RT.performed += i => rt_Input = true;
                inputActions.PlayerActions.LB.performed += i => lb_Input = true;
                inputActions.PlayerActions.LT.performed += i => lt_Input = true;

                inputActions.PlayerQuickSlots.DPadRight.performed += i => d_Pad_Right = true;
                inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_Pad_Left = true; 
                HandleInteractingButtonInput();
                HandleJumpInput();
                inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true;
                inputActions.PlayerActions.LockOn.performed += i => lockOnInput = true;
                inputActions.PlayerActions.Y.performed += i => y_Input = true;
                inputActions.PlayerMovement.LockOnTargetLeft.performed +=i => right_Stick_Left_Input = true;
                inputActions.PlayerMovement.LockOnTargetRight.performed += i => right_Stick_Right_Input = true;

            }
            inputActions.Enable();
        }
        private void OnDisable()
        {
            inputActions.Disable();
        }
        public void TickInput(float delta)
        {
            HandleMoveInput(delta);
            HandleRollInput(delta);
            HandleAttackInput(delta);
            HandleQuickSlotsInput();
            HandleInventoryInput();
            HandleLockOnInput();
            HandleTwoHandInput();
        }
        private void HandleMoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }
        private void HandleRollInput(float delta)
        {
            b_Input = inputActions.PlayerActions.Roll.phase == UnityEngine.InputSystem.InputActionPhase.Started;
            sprintFlag = false;
            if (b_Input)
            {
                rollInputTimer += delta;
                if(moveAmount>0.55f)
                    sprintFlag = true;
            }
            else
            {
                if(rollInputTimer > 0&& rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }
                rollInputTimer = 0;
            }
        }
        private void HandleAttackInput(float delta)
        {
            if(rt_Input)
            {
                animatorHandler.anim.SetBool("isRight",true);
                playerAttacker.HandleHeavyAttackAction(playerInventory.rightWeapon);
            }
            else if(lt_Input)
            {
                animatorHandler.anim.SetBool("isLeft",true);
                playerAttacker.HandleHeavyAttackAction(playerInventory.leftWeapon);
            }
            if(rb_Input)
            {   
                animatorHandler.anim.SetBool("isRight",true);
                playerAttacker.HandleRBAction(playerInventory.rightWeapon);
            }
            else if(lb_Input)
            {
                animatorHandler.anim.SetBool("isLeft",true);
                playerAttacker.HandleRBAction(playerInventory.leftWeapon);
            }
        }
        private void HandleQuickSlotsInput()
        {
            if(d_Pad_Right)
            {
                playerInventory.ChangeRightWeapon();
            }
            else if(d_Pad_Left)
            {
                playerInventory.ChangeLeftWeapon();
            }
        }
        private void HandleInteractingButtonInput()
        {
            inputActions.PlayerActions.A.performed += i => a_Input = true;
        }
        private void HandleJumpInput()
        {
            inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
        }
        private void HandleInventoryInput()
        {
            if(inventory_Input)
            {
                inventoryFlag = !inventoryFlag;
                if(inventoryFlag)
                {
                    uiManage.OpenSelectWindow();
                    uiManage.UpdateUI();
                    uiManage.HUDWindow.SetActive(false);                }
                else
                {
                    uiManage.CloseSelectWindow();
                    uiManage.HUDWindow.SetActive(true);
                    uiManage.CloseAllInventoryWindow();
                }
            }
        }

        private void HandleLockOnInput()
        {
            if(lockOnInput && !lockOnFlag)
            {
                lockOnInput = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.nearestLockOnTarget != null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                    lockOnFlag = true;
                }
            }
            else if(lockOnInput && lockOnFlag)
            {
                lockOnInput = false;
                lockOnFlag = false;
                cameraHandler.ClearLockOnTargets();
            }

            if(lockOnFlag && right_Stick_Left_Input)
            {
                right_Stick_Left_Input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.leftLockTarget!=null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
                }
            }
            if(lockOnFlag && right_Stick_Right_Input)
            {
                right_Stick_Right_Input = false;
                cameraHandler.HandleLockOn();
                if(cameraHandler.rightLockTarget!=null)
                {
                    cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
                }
            }
            cameraHandler.SetCameraHeight();
        }
        private void HandleTwoHandInput()
        {
            if(y_Input)
            {
                y_Input = false;
                two_handFlag = !two_handFlag;
                if(two_handFlag)
                {
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon,false);
                }
                else
                {
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon,false);
                    weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon,true);
                }
            }
        }
    }
}
