using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class UIManage : MonoBehaviour
    {
        public PlayerInventory playerInventory;
        public EquipmentWindowUI equipmentWindowUI;
        [Header("UI Window")]
       public GameObject selectWindow;
       public GameObject HUDWindow;
       public GameObject weaponInventoryWindow;
       public GameObject equipmentScreenWindow;
       [Header("Equipment Window Slot Selected")]
       public bool rightHandSlot01Selected;
       public bool rightHandSlot02Selected;
       public bool leftHandSlot01Selected;
       public bool leftHandSlot02Selected;
       
       [Header("Weapon Inventory")]
       public GameObject weaponInventorySlotPrefab;
       public Transform weaponInventorySlotParent;
        WeaponInventorySlot[] weaponInventorySlots;
        public HandEquipmentSlotUI[] handEquipmentSlotUIs;

       private void Awake() {
       }
       private void Start() {
            weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
            handEquipmentSlotUIs = equipmentWindowUI.GetComponentsInChildren<HandEquipmentSlotUI>();
       }
       public void UpdateUI()
       {
            equipmentWindowUI.LoadWeaponsOnEquipmentScreen(handEquipmentSlotUIs,playerInventory);
           #region Weapon inventory Slots
            for (int i = 0; i < weaponInventorySlots.Length; i++)
            {
                if(i<playerInventory.weaponsInventory.Count)
                {
                    if(weaponInventorySlots.Length<playerInventory.weaponsInventory.Count)
                    {
                        Instantiate(weaponInventorySlotPrefab,weaponInventorySlotParent);
                        weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
                    }
                    weaponInventorySlots[i].AddIterm(playerInventory.weaponsInventory[i]);
                }
                else
                {
                    weaponInventorySlots[i].ClearInventorySlot();
                }
            }
           #endregion
       }
        public void OpenSelectWindow()
        {
            selectWindow.SetActive(true);
        }
        public void CloseSelectWindow()
        {
            selectWindow.SetActive(false);
        }
        public void CloseAllInventoryWindow()
        {
            ResetAllSelectedSlots();
            weaponInventoryWindow.SetActive(false);
            equipmentScreenWindow.SetActive(false);
        }
        public void ResetAllSelectedSlots()
        {
            rightHandSlot01Selected = false;
            rightHandSlot02Selected = false;
            leftHandSlot01Selected = false;
            leftHandSlot02Selected = false;
        }
    }
}

