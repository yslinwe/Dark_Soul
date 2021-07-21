using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SG
{
    public class WeaponPickUp : Interactable
    {
        public WeaponItem weapon;
        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);
            PickUpItem(playerManager);
        }
        private void PickUpItem(PlayerManager playerManager)
        {
            PlayerInventory playerInventory;
            PlayerLocomotion playerLocomotion;
            AnimatorHandler animatorHandler;
            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
            animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();

            playerLocomotion.rigidbody.velocity = Vector3.zero; // Stops the Player from moving whilst pick up item
            animatorHandler.PlayTargetAnimation("Pick Up Item",true);//Plays the animation of looting the item
            playerInventory.weaponsInventory.Add(weapon);
            
            if(weapon.weaponHand == WeaponHand.isRightHand)
                playerInventory.weaponsInRightHandSlots.Add(weapon);
            if(weapon.weaponHand == WeaponHand.isLeftHand)
                playerInventory.weaponsInLeftHandSlots.Add(weapon);

            Text itemName =  playerManager.itemInteractableUIGameObject.GetComponentInChildren<Text>();
            itemName.text = weapon.itemName;
            playerManager.itemInteractableUIGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;
            playerManager.itemInteractableUIGameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
} 