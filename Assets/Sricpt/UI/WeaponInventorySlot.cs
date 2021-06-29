using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

namespace SG
{
    public class WeaponInventorySlot : MonoBehaviour
    {
        PlayerInventory playerInventory;
        WeaponSlotManager weaponSlotManager;
        UIManage uIManage;
        public Image icon;
        WeaponItem item;
        private void Awake() {
            playerInventory = FindObjectOfType<PlayerInventory>();
            uIManage = FindObjectOfType<UIManage>();
            weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
        }
        public void AddIterm(WeaponItem newItem) 
        {
            item = newItem;
            icon.sprite = item.itemIcon;
            icon.enabled = true;
            gameObject.SetActive(true);
        }
        public void ClearInventorySlot()
        {
            item = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }
        public void EquipThisItem()
        {
            if(uIManage.rightHandSlot01Selected)
            {
                print(playerInventory.weaponsInventory.Count);
                print(playerInventory.weaponsInRightHandSlots.Count);
                playerInventory.weaponsInventory.Add(playerInventory.weaponsInRightHandSlots[1]);
                playerInventory.weaponsInRightHandSlots[1] = item;
                playerInventory.weaponsInventory.Remove(item);
                playerInventory.currentRightWeaponIndex = 1;
                playerInventory.rightWeapon = playerInventory.weaponsInRightHandSlots[playerInventory.currentRightWeaponIndex];
            }
            else if(uIManage.rightHandSlot02Selected)
            {
                playerInventory.weaponsInventory.Add(playerInventory.weaponsInRightHandSlots[2]);
                playerInventory.weaponsInRightHandSlots[2] = item;
                playerInventory.weaponsInventory.Remove(item);
                playerInventory.currentRightWeaponIndex = 2;
                playerInventory.rightWeapon = playerInventory.weaponsInRightHandSlots[playerInventory.currentRightWeaponIndex];
            }
            else if(uIManage.leftHandSlot01Selected)
            {
                playerInventory.weaponsInventory.Add(playerInventory.weaponsInLeftHandSlots[1]);
                playerInventory.weaponsInLeftHandSlots[1] = item;
                playerInventory.weaponsInventory.Remove(item);
                playerInventory.currentLeftWeaponIndex = 1;
                playerInventory.leftWeapon = playerInventory.weaponsInLeftHandSlots[playerInventory.currentLeftWeaponIndex];
            }
            else if(uIManage.leftHandSlot02Selected)
            {
                playerInventory.weaponsInventory.Add(playerInventory.weaponsInLeftHandSlots[2]);
                playerInventory.weaponsInLeftHandSlots[2] = item;
                playerInventory.weaponsInventory.Remove(item);
                playerInventory.currentLeftWeaponIndex = 2;
                playerInventory.leftWeapon = playerInventory.weaponsInLeftHandSlots[playerInventory.currentLeftWeaponIndex];
            }
            else
            {
                return;
            }

            weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon,false);
            weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon,true);

            uIManage.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(uIManage.handEquipmentSlotUIs,playerInventory);
            uIManage.ResetAllSelectedSlots();
        }
    }
}