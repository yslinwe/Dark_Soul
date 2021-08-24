using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{
    public class DamageCollider : MonoBehaviour
    {
        Collider damageCollider;
        public int currentWeaponDamage = 25;
        private void Awake() {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
        }
        public void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }
        public void DisableDamageCollider()
        {
            damageCollider.enabled = false;
        }
        private void OnTriggerEnter(Collider collision) {
            
            if(collision.tag == "Hittable")
            {
                PlayerStates playerStates = collision.GetComponent<PlayerStates>();
                if(playerStates != null)
                {
                    playerStates.TakeDamage(currentWeaponDamage);
                }
            }
            if(collision.tag == "Player")
            {
                PlayerStates playerStates = collision.GetComponent<PlayerStates>();
                if(playerStates != null)
                {
                    playerStates.TakeDamage(currentWeaponDamage);
                }
            }
            if(collision.tag == "Enemy")
            {
                EnemyStates enemyStates = collision.GetComponent<EnemyStates>();
                if(enemyStates != null)
                {
                    enemyStates.TakeDamage(currentWeaponDamage);
                }
            }
        }
    }

}
