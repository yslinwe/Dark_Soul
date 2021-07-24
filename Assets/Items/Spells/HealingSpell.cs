using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SG
{    
    [CreateAssetMenu(menuName = "Spells/Healing shelll")]
    public class HealingSpell : SpellItem
    {
        public int healAmount;
        public override void AttemptToCastSpell(AnimatorHandler animatorHandler, PlayerStates playerStates)
        {
            base.AttemptToCastSpell(animatorHandler,playerStates);
            GameObject instantiateWarmUpSpellFX = Instantiate(spellWarmUpFX, animatorHandler.transform);
            animatorHandler.PlayTargetAnimation(spellAnimation,true);
            Destroy(instantiateWarmUpSpellFX,1);
            Debug.Log("Attempt to cast spell...");
        }
        public override void SuccessfullyCastSpell(AnimatorHandler animatorHandler, PlayerStates playerStates)
        {
            base.SuccessfullyCastSpell(animatorHandler,playerStates);
            GameObject instantiatedSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
            playerStates.HealPlayer(healAmount);
            Destroy(instantiatedSpellFX,1);
            Debug.Log("Spell cast successful");
        }
    }
}
