using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowThreshold : MonoBehaviour
{
    public GameObject player;
    public Vector2 followOffset;
    public float cameraSpeed;

    private Vector2 threshold;
    private Rigidbody2D rb; // Referencia ao rigidbody do Player
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        threshold = calculateThreshold();
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 follow = player.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if(Mathf.Abs(xDifference) >= threshold.x)
        {
            newPosition.x = follow.x;
        }
        if(Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }

        // Decidir a velocidade da camera
        if(rb.velocity.magnitude > cameraSpeed)
        {
            moveSpeed = rb.velocity.magnitude;
        }
        else
        {
            moveSpeed = cameraSpeed;
        }

        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }

    private Vector3 calculateThreshold()
    {
        // Get camera's aspect ratio
        Rect aspect = Camera.main.pixelRect;

        // Get camera's dimensions
        Vector2 camDimensions = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);

        // Calculate threshold
        camDimensions.x -= followOffset.x;
        camDimensions.y -= followOffset.y;

        return camDimensions;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = calculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
