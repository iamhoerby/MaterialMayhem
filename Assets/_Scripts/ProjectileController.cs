using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float initialSpeed = 50f; // Initial speed of the projectile
    public float gravity = 9.8f; // Gravity affecting the projectile

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Set the initial velocity to make the projectile move forward
        rb.velocity = transform.forward * initialSpeed;
    }

    void Update()
    {
        // Apply gravity manually
        rb.velocity += Vector3.down * gravity * Time.deltaTime;
    }
}
