using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class UIManage : MonoBehaviour
    {
        public PlayerInventory playerInventory;
        EquipmentWindowUI equipmentWindowUI;
        [Header("UI Window")]
       public GameObject selectWindow;
       public GameObject HUDWindow;
       public GameObject weaponInventoryWindow;
       [Header("Weapon Inventory")]
       public GameObject weaponInventorySlotPrefab;
       public Transform weaponInventorySlotParent;
       WeaponInventorySlot[] weaponInventorySlots;
       private void Awake() {
           equipmentWindowUI = FindObjectOfType<EquipmentWindowUI>();
       }
       private void Start() {
            weaponInventorySlots = weaponInventorySlotParent.GetComponentsInChildren<WeaponInventorySlot>();
       }
       public void UpdateUI()
       {
           //todo 更改位置
            equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
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
            weaponInventoryWindow.SetActive(false);
        }
    }
}

