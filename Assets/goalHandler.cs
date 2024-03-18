using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class goalHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject particleSystem; 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other) {
        Scene[] allScenes = SceneManager.GetAllScenes();
        if (other.CompareTag("Player"))
        {
            particleSystem.GetComponent<ParticleSystem>().Play(); 
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            if (currentScene < allScenes.Length) 
            {
                SceneManager.LoadScene(allScenes[currentScene + 1].buildIndex);
            }
            else {
                print("Its last scene");
            }
        }
    }
}
