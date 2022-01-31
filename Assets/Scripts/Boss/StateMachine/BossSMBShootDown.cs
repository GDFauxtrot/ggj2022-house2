using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSMBShootDown : SceneLinkedSMB<Boss>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.ShootBulletsDown();
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.ShootBulletsDownFinished();
    }
}
