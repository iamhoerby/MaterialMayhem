using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormableHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other) {
    }
    public void GetHit(RaycastHit hit) {
        //Grab Mechanics here 
        Debug.Log("I got hit"); 
    }
}
