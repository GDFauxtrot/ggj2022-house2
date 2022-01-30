using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSMBLookAtPlayer : SceneLinkedSMB<Boss>
{
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.LookAtPlayer();
    }

}
