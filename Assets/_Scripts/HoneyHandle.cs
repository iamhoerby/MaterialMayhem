using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HoneyHandle : MonoBehaviour
{
    // Start is called before the first frame update
    public int[] connectedVertices; 
    public GameObject[] connectedHandles; 
    public Vector3 oldPosition; 
    public GameObject parent; 
    void Start()
    {
        oldPosition = this.transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }
    public void SetParent(GameObject parent) {
        this.parent = parent; 
    }
    public void AddVertices(int[] newVertices) {
        int[] result = connectedVertices.Union(newVertices).ToArray(); 
        connectedVertices = result; 
    }
    public void AddHandles(GameObject[] newHandles) {
        GameObject[] result = connectedHandles.Union(newHandles).ToArray(); 
        connectedHandles = result;
    }
    public int[] GetVertices() {
        return connectedVertices;
    }
    public GameObject[] GetHandles() {
        return connectedHandles; 
    }
    public void SetPosition(Vector3 position) {
        this.transform.position = position; 
        oldPosition = position; 
    }
    void UpdatePosition() {
        Vector3 movement = this.transform.position - oldPosition; 
        if(connectedHandles != null && connectedHandles.Length > 0) {
            for (int i = 0; i < connectedHandles.Length; i++)
            {
                connectedHandles[i].GetComponent<HoneyHandle>().UpdatePosition(movement);
            }
        }
        parent.GetComponent<HoneyFormable>().UpdateVertices(connectedVertices, movement);
        oldPosition = this.transform.position; 
    }
    public void UpdatePosition(Vector3 movement) {
        this.transform.position += movement; 
    }
    public void UpdateOtherHandles() {
        parent.GetComponent<HoneyFormable>().UpdateHandlesPosition();
    }
}
