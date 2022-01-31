using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSMBShootAround : SceneLinkedSMB<Boss>
{
    [SerializeField] private int bulletNums = 9;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MonoBehaviour.ShootAround(bulletNums);
    }
}
