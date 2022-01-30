using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody lootRB;
    [SerializeField] private Collider lootCollider;
    [SerializeField] private float pickUpDistance = 10;
    [SerializeField] private float attractMovingSpeed = 40;

    private List<GameObject> players;

    public void Initialize(Vector3 position)
    {
        if (!lootRB)
            lootRB = GetComponent<Rigidbody>();
        if (!lootCollider)
            lootCollider = GetComponent<Collider>();
        if (players == null)
            players = new List<GameObject>();

        // lmao this almost looks like netcode
        players.Clear();
        foreach (PlayerController player in GameObject.FindObjectsOfType<PlayerController>())
        {
            players.Add(player.gameObject);
        }

        transform.position = position;
        lootRB.velocity = Vector3.zero;

        SpawnJump();
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            Vector3 closestDir = Vector3.zero;
            float closestDist = Mathf.Infinity;
            bool closestInRange = false;

            foreach (GameObject player in players)
            {
                Vector3 dir = player.transform.position - transform.position;
                if (dir.magnitude < closestDist)
                {
                    closestDir = player.transform.position - transform.position;
                    closestDist = closestDir.magnitude;
                }
                closestInRange = closestDist <= pickUpDistance;
            }

            if (!closestInRange)
            {
                lootCollider.isTrigger = false;
                lootRB.useGravity = true;
            }
            else
            {
                lootCollider.isTrigger = true;
                lootRB.useGravity = false;

                lootRB.AddForce(attractMovingSpeed * closestDir.normalized * Time.deltaTime * 60f, ForceMode.Acceleration);
                // Make sure velocity always points to the player, otherwise pickup might orbit around player
                lootRB.velocity = closestDir.normalized * lootRB.velocity.magnitude;
            }
        }
    }

    // Create a little exploding effect with loot when dropped from enemies, adding some random vertical movement
    private void SpawnJump()
    {
        Vector3 movementDir = new Vector3(
            Random.Range(-1f, 1f),
            1f,
            Random.Range(-1f, 1f)).normalized;
        float movementForce = Random.Range(4f, 8f);
        lootRB.AddForce(movementDir * movementForce, ForceMode.Impulse);
    }
}
