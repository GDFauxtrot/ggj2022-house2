using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum MovementType{
        Idle,
        Approach
    }
    
    [SerializeField] private MovementType movementType;
    [SerializeField] private EnemyData data;
    [SerializeField] private Rigidbody rigid;
    private GameObject player;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        switch(movementType){
            case(MovementType.Approach):{
                ApproachPlayer();
                break;
            }
        }
    }

    private void ApproachPlayer(){
        Vector3 diff = player.transform.position - gameObject.transform.position;
        diff.y = 0;
        Vector3 velocity = diff.normalized * data.movingSpeed;
        velocity.y = rigid.velocity.y;
        rigid.velocity = velocity;
        // look at player
        Vector3 lookAtPos = player.transform.position;
        lookAtPos.y = transform.position.y;
        transform.LookAt(lookAtPos);
        animator.SetFloat("MoveSpeed", velocity.magnitude);
        animator.SetBool("IsMoving", true);
    }

    
}
