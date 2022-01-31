using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSMBSideSlam : SceneLinkedSMB<Boss>
{
    [SerializeField] private float hurtBoxAppearTimeInPercent = 0.5f;
    [SerializeField] private float hurtBoxRemovalTimeInPercent = 0.6f;
    private bool hasActivatedHurtBox;
    private bool hasRemovedHurtBox;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        hasActivatedHurtBox = false;
        hasRemovedHurtBox = false;
    }

    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!hasActivatedHurtBox && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > hurtBoxAppearTimeInPercent){
            m_MonoBehaviour.SetSideSlamHurtBoxActive(true);
            hasActivatedHurtBox = true;
        }

        if(!hasRemovedHurtBox && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > hurtBoxRemovalTimeInPercent){
            m_MonoBehaviour.SetSideSlamHurtBoxActive(false);
            hasRemovedHurtBox = true;
        }
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.SetSideSlamHurtBoxActive(false);
    }
}
