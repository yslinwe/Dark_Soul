using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class PlayerManager : CharacterManager
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerStates playerStates;
        PlayerLocomotion playerLocomotion;
        InteractableUI interactableUI;
        public GameObject interactableUIGameObject;
        public GameObject itemInteractableUIGameObject;
        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool isInteracting;
        public bool canDoCombo;
        public bool isInvulerable;
        private MyTools tools;
        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            tools = new MyTools();
            
        }
        void Start()
        {
            cameraHandler = FindObjectOfType<CameraHandler>();
            interactableUI = FindObjectOfType<InteractableUI>();
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            playerStates = GetComponent<PlayerStates>();
        }

        // Update is called once per frame
        void Update()
        {
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            isInvulerable = anim.GetBool("isInvulerable");
            anim.SetBool("isInAir",isInAir);

            float delta = Time.deltaTime;

            inputHandler.TickInput(delta);

            playerLocomotion.HandleJumping();
            playerLocomotion.HandleRollingAndSprinting(delta);
            playerStates.RegenerateStamina();
            CheckForInteractableObject();
         
        }
        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
            playerLocomotion.HandleMovement(delta);
            playerLocomotion.HandleFalling(delta,playerLocomotion.moveDirection);
        }
        private void LateUpdate() {
            inputHandler.rollFlag = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.lb_Input = false;
            inputHandler.lt_Input = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.a_Input = false;
            inputHandler.jump_Input = false;
            inputHandler.inventory_Input = false;
            
            float delta = Time.deltaTime;
            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
            if(isInAir)
            {
                playerLocomotion.inAirTimer += Time.deltaTime;
            }
        }
        public void CheckForInteractableObject()
        {
            RaycastHit hit;
            if(Physics.SphereCast(transform.position,0.3f,transform.forward,out hit,1f,cameraHandler.ignoreLayers))
            {
                if(hit.collider.tag == "Interactable")
                {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();
                    if(interactableObject != null)
                    {
                        string interactableText = interactableObject.interactableText;
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);
                        if(inputHandler.a_Input)
                        {
                            //hit.collider.GetComponent<Interactable>().Interact(this);
                            interactableObject.Interact(this);
                        }
                    }
                }
            }
            else
            {

                if(interactableUIGameObject != null)
                {
                    interactableUIGameObject.SetActive(false);
                }
                if(itemInteractableUIGameObject != null)
                {
                    if(tools.upSide(itemInteractableUIGameObject.activeSelf))
                    {
                        StopCoroutine(itemInteractableUIGameObjectDisable());
                        StartCoroutine(itemInteractableUIGameObjectDisable());
                    }
                }  
            }
        }
        IEnumerator itemInteractableUIGameObjectDisable()
        {
            yield return new WaitForSeconds(3);
            itemInteractableUIGameObject.SetActive(false);
            yield break;
        }
        
    }
}


