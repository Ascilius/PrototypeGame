using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // timing
    private float startTime;
    [SerializeField] private int intervalTime = 1;

    // movement
    [SerializeField] private float movementSpeed = 1f;
    private Rigidbody2D rb;
    private Vector2 nullDirection = new Vector2(0f, 0f);
    private Vector2 movementDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        startTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() { 
        // determining input direction
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector2 updatedDirection;
        if (Mathf.Abs(hor) > Mathf.Abs(ver))
            updatedDirection = new Vector2(hor, 0f);
        else
            updatedDirection = new Vector2(0f, ver);

        // rejecting no direction change
        if (!updatedDirection.Equals(nullDirection)) {
            movementDirection = updatedDirection;
            // print("Debug: Direction updated");
        }
    }

    private void FixedUpdate() {
        float totalTime = Time.time - startTime;
        // print("Debug: totalTime: " + totalTime);
        if (totalTime > intervalTime) {
            rb.position += movementDirection.normalized * movementSpeed;
            startTime = Time.time; // reset timer
        }
    }

}
