using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SG
{
    public class HandEquipmentSlotUI : MonoBehaviour
    {
        public Image icon;
        WeaponItem weapon;
        
        public bool rightHandSlot01;
        public bool rightHandSlot02;
        public bool leftHandSlot01;
        public bool leftHandSlot02;
        public void AddItem(WeaponItem newWeapon)
        {
            weapon = newWeapon;
            icon.sprite = weapon.itemIcon;
            icon.color = new Color(255,255,255,255);
            icon.enabled = true;
            gameObject.SetActive(true);
        }
        public void ClearItem()
        {
            weapon = null;
            icon.sprite = null;
            icon.enabled = false;
            gameObject.SetActive(false);
        }

    }
}
