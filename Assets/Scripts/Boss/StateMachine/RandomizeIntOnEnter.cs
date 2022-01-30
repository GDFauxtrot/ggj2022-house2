using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeIntOnEnter : StateMachineBehaviour
{
    [SerializeField] private int intMin = 0;
    [SerializeField] private int intMax = 1;
    [SerializeField] private string intName;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(intName, Random.Range(intMin, intMax + 1));
    }
}
