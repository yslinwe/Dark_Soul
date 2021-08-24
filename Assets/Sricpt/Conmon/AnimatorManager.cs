using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace  SG
{
    public class AnimatorManager : MonoBehaviour
    {
        public Animator anim;
        public bool canRotate;

        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            if(targetAnim == null)
                Debug.LogError(targetAnim+" is null");
            if(targetAnim == "")
                Debug.LogWarning("PlayTargetAnimation of targetAnim is Empty");
            if(targetAnim !=""&&targetAnim!=null)
            {
                anim.applyRootMotion = isInteracting;
                anim.SetBool("isInteracting", isInteracting);
                anim.CrossFade(targetAnim, 0.2f);
            }
        }
        public virtual void TakeCriticalDamageAnimationEvent()
        {
            
        }
    }
}