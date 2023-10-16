using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothThirdPersonCamera : MonoBehaviour
{   
    private Vector3 offset = new Vector3(0,3,-7);
    public GameObject player; 
    public float damping = 1;
    // Start is called before the first frame update
    void Start()
    {
        offset = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        float currentAngle = transform.eulerAngles.y;
        float desiredAngle = player.transform.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);
        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        transform.position = player.transform.position - (rotation * offset);
        //transform.position = player.transform.position + offset; 
        //transform.rotation = player.transform.rotation; 
        transform.LookAt(player.transform);
    }
}

