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

    void Start()
    {
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
        rigid.velocity = diff.normalized * data.movingSpeed;
        // look at player
        Vector3 lookAtPos = player.transform.position;
        lookAtPos.y = transform.position.y;
        transform.LookAt(lookAtPos);
    }

    
}
