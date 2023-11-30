using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCharacterHandler : MonoBehaviour
{
    public Camera camera; 
    public float posX = 0.0f;
    public float posY = 0.0f; 
    public float posZ = 0.0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits; 
        hits = Physics.RaycastAll(camera.transform.position, Vector3.down, 100.0f);
        posX = camera.transform.position.x; 
        posZ = camera.transform.position.z; 
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Ground") {
                posY = hit.point.y + this.gameObject.GetComponent<CapsuleCollider>().height / 2;
                break;
            }
        }
        
        this.gameObject.transform.position = new Vector3(posX,posY,posZ);
    }
}
