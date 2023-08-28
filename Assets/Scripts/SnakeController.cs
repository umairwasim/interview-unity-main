using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float moveSpeed = 1f; // Snake movement speed
    public float moveInterval = 0.5f; // Time interval between movements
    public Transform bodyPrefab; // Prefab for the snake body segment

    private Vector2Int direction = Vector2Int.right; // Initial movement direction
    private List<Transform> bodySegments = new List<Transform>();
    private float lastMoveTime; // Time of the last movement
    [SerializeField] private AppleSpawner appleSpawner;

    private void Start()
    {
        // Spawn initial segments
        SpawnBodySegment();
        SpawnBodySegment();
        lastMoveTime = Time.time;
    }

    private void Update()
    {
        // Handle player input to change direction
        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2Int.down)
            direction = Vector2Int.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2Int.up)
            direction = Vector2Int.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2Int.right)
            direction = Vector2Int.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2Int.left)
            direction = Vector2Int.right;
    }

    private void FixedUpdate()
    {
        // Check if enough time has passed since the last move
        if (Time.time - lastMoveTime >= moveInterval)
        {
            // Move the snake
            Vector3 nextPosition = transform.position + new Vector3(direction.x, direction.y, 0);
            transform.position = nextPosition;

            // Update body segments
            for (int i = bodySegments.Count - 1; i > 0; i--)
            {
                bodySegments[i].position = bodySegments[i - 1].position;
            }
            bodySegments[0].position = transform.position;

            #region Apple Collision

            Collider2D coll = Physics2D.OverlapCircle(transform.position, 0.5f); // Adjust the radius to match the snake's size

            if (coll != null && coll.CompareTag("Apple"))
            {
                // Consume the apple
                Destroy(coll.gameObject);
                SpawnBodySegment(); // Extend the snake's body
                appleSpawner.SpawnApple(); // Spawn a new apple
                UIManager.Instance.UpdateScore(); //Increment score
            }

            #endregion

            #region self collisio 

            // Check if the snake's head collides with its body segments
            for (int i = 1; i < bodySegments.Count; i++)
            {
                if (bodySegments[i].position == (Vector3Int)direction + transform.position)
                {
                    GameManager.Instance.SwitchGameState(GameState.Lost);
                    break; // No need to check further
                }
            }

            #endregion

            lastMoveTime = Time.time; // Update the last move time
        }
    }

    private void SpawnBodySegment()
    {
        // Spawn a new body segment
        Transform newSegment = Instantiate(bodyPrefab, bodySegments.Count > 0 ? bodySegments[bodySegments.Count - 1].position : transform.position, Quaternion.identity);
        bodySegments.Add(newSegment);
    }
}





