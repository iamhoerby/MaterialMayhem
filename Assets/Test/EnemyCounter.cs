using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCounter : MonoBehaviour
{
    GameObject[] gos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject.Find("EnemyCounter").GetComponent<TextMeshPro>().SetText(gos.Length.ToString());
    }
}
