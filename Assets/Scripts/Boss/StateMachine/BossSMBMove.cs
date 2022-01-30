using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSMBMove : SceneLinkedSMB<Boss>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.MoveTowardsRandomDirection();
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
