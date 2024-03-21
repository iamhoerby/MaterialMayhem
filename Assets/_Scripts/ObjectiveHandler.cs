using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var objective = GameObject.Find("Goal");
        var lookPos = objective.transform.position - transform.position;
        var rotation = Quaternion.LookRotation(lookPos);
        rotation *= Quaternion.Euler(-90, 0, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1); 
    }
}
