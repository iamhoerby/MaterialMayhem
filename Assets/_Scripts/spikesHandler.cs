using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 


public class spikesHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        if (other.CompareTag("Player")) {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        if (other.CompareTag("Morphable") || other.CompareTag("Bullet")) {
            Destroy(other.gameObject);
        }
    }
}
