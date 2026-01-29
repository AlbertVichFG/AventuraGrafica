using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 4f;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal"); // A, D
        float z = Input.GetAxis("Vertical");   // W, S

        Vector3 movement = transform.forward * z + transform.right * x;
        rb.linearVelocity = new Vector3(movement.x * speed, rb.linearVelocity.y, movement.z * speed);

    }
}
