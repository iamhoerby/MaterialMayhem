using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    // Start is called before the first frame update
    float direction = 1; 
    bool isGrounded; 
    public Rigidbody rb; 
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); 
        InvokeRepeating("Move",0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
       
        /* transform.position += new Vector3(1,0,0) * direction * 0.1f;
        int random = Random.Range(0,10); 
        if (random < 3) {
            direction = direction * (-1); 
        } */
    }
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Bullet") {
            Destroy(other.gameObject);
            Destroy(gameObject);
        } else {
            isGrounded = true; 
        }
    }
    void OnCollisionExit(Collision other) {
        isGrounded = false; 
    }
    void Move() {
        if (isGrounded) {
            rb.AddForce(new Vector3(Random.Range(-5.0f,5.0f),Random.Range(0.0f,5.0f),Random.Range(-5.0f,5.0f)),ForceMode.VelocityChange); 
        }
    }
}
