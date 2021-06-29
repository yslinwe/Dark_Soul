using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;
        [Header("Idle Animations")]
        public string right_hand_idle;
        public string left_hand_idle;
        public string two_hand_idle;
        [Header("One Handed Attack Animations")]
        public string OH_Light_Attack_1;
        public string OH_Light_Attack_2;
        public string OH_Heavy_Attack_1;
        [Header("Two Handed Attack Animations")]
        public string TH_Sword_Attack_01;
        public string TH_Sword_Attack_02;
        public string TH_Sword_Attack_03;
        [Header("Stamina Coats")]
        public int baseStamina;
        public float lightAttackMultiplier;
        public float heavyAttackMultiplier;
    }

}
