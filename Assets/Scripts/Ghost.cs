using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float seekDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private LevelRooms[] levelRooms;

    private Rigidbody2D rb;
    private bool canMove;

    public bool CanMove
    {
        get { return canMove; }
        set
        {
            if (!value)
            {
                rb.velocity = Vector3.zero;
            }

            canMove = value;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= seekDistance)
        {
            SeekPlayer();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void SeekPlayer()
    {
        Vector2 direction = (transform.position - playerTransform.position).normalized;

        rb.velocity = new Vector2(-direction.x * moveSpeed, rb.velocity.y);
    }

    public void ChangeRoom(int level, bool enterFromRight)
    {
        LevelRooms levelRoom = levelRooms[level];
        int chanceToTeleportToPlayerRoom = Random.Range(0, 4);
        int roomIndex;

        if (chanceToTeleportToPlayerRoom >= 2)
        {
            roomIndex = Random.Range(0, levelRoom.bounds.Length);
        }
        else
        {
            roomIndex = GameManager.Instance.GetPlayerNextRoom(levelRooms[level].bounds);
        }

        transform.position = levelRoom.bounds[roomIndex].GetGhostPosition(enterFromRight);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.OpenLoseCanvas();
        }
    }
}
