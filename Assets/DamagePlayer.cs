using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace SG
{
    public class DamagePlayer : MonoBehaviour
    {
        private int damage = 25;
        private void OnTriggerEnter(Collider other)
        {
            PlayerStates playerStates = other.GetComponent<PlayerStates>();
            if (playerStates != null)
            {
                playerStates.TakeDamge(damage);
            }
        }
    }
}
