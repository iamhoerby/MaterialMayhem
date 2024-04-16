using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class HoneyFormable : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject handlePrefab;
    public Vector3[] vertices; 
    public List<GameObject> handles = new List<GameObject>();
    List<GameObject> verticesHandles = new List<GameObject>();  
    List<GameObject> edgeHandles = new List<GameObject>();
    public MeshCollider collider; 
    int[] triangles; 
    void Start()
    {
        transform.rotation = Quaternion.Euler(0,0,0);
        transform.GetComponent<Rigidbody>().isKinematic = true; 
        transform.GetComponent<Rigidbody>().useGravity = false; 
        transform.gameObject.tag="Untagged";
        handlePrefab = Resources.Load("Handle") as GameObject; 

        vertices = GetComponent<MeshFilter>().mesh.vertices;
        triangles = GetComponent<MeshFilter>().mesh.triangles; 

        // Initiate handles at every vertice 
        for(int i = 0; i < vertices.Length; i++)
        {
            GameObject newHandle = Instantiate(handlePrefab, this.transform.TransformPoint(vertices[i]), Quaternion.identity, this.transform);
            verticesHandles.Add(newHandle); 
            int[] tmp = {i};
            newHandle.GetComponent<HoneyHandle>().AddVertices(tmp);
            newHandle.GetComponent<HoneyHandle>().SetParent(this.gameObject);
        }
        verticesHandles = ReduceHandles(verticesHandles); 

        // Initiates handles at every edge 
        for(int i = 0; i < verticesHandles.Count; i++)
        {
            for(int j = i; j < verticesHandles.Count; j++) 
            {
                if (verticesHandles[i] != verticesHandles[j]) {
                    Vector3 newPosition =  (verticesHandles[i].transform.position + verticesHandles[j].transform.position) / 2; 
                    GameObject newHandle = Instantiate(handlePrefab, newPosition, Quaternion.identity, this.transform);
                    edgeHandles.Add(newHandle); 
                    GameObject[] tmp = {verticesHandles[i],verticesHandles[j]};
                    newHandle.GetComponent<HoneyHandle>().AddHandles(tmp);
                    newHandle.GetComponent<HoneyHandle>().SetParent(this.gameObject);
                }
            }
        } 
        edgeHandles = ReduceHandles(edgeHandles); 
        handles = verticesHandles.Union<GameObject>(edgeHandles).ToList<GameObject>();
        
        //DestroyImmediate(GetComponent<Collider>());
        Debug.Log("Add Collider");
        collider = this.transform.gameObject.AddComponent<MeshCollider>();
        collider.sharedMesh = this.GetComponent<MeshFilter>().mesh;
        collider.convex = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHandlesList(); 
        UpdateCollider(); 
        //UpdateHandlesPosition(); 
    }
    // Takes a list of handles 
    // Reduces the Amount of Handles, Merges handles which are close to each other into one handle 
    // Returns updated List 
    List<GameObject> ReduceHandles(List<GameObject> inputList) {
        List<GameObject> result = inputList; 
        GameObject[] tmp = inputList.ToArray(); 
        for(int i = 0; i < tmp.Length; i++)
        {
            for(int j = i; j < tmp.Length; j++) 
            {
                float distance = (tmp[i].transform.position - tmp[j].transform.position).magnitude;
                if(tmp[i] != tmp[j] && distance < 0.01) {
                    MergeHandles(tmp[i],tmp[j],inputList); 
                    result.Remove(tmp[j]);
                }
            }
        }
        return result;
        
    }
    // Takes two handles and a list of handles 
    // merges the handles and their attributes 
    // Returns a updated List 
    List<GameObject> MergeHandles(GameObject handle1, GameObject handle2, List<GameObject> inputList) {
        List<GameObject> tmp = inputList; 
        handle1.GetComponent<HoneyHandle>().AddVertices(handle2.GetComponent<HoneyHandle>().GetVertices());
        handle1.GetComponent<HoneyHandle>().AddHandles(handle2.GetComponent<HoneyHandle>().GetHandles());
        tmp.Remove(handle2); 
        Destroy(handle2);
        return tmp; 
    }
    List<GameObject> UpdateHandlesList() {
        List<GameObject> result = new List<GameObject>(); 
        List<GameObject> tmp = handles; 
        foreach (GameObject handle in tmp){
            if (handle != null){
                result.Add(handle);
            } 
        }
        return result; 
    }
    void UpdateCollider() {
        collider.sharedMesh = this.GetComponent<MeshFilter>().mesh;
    }
    public void UpdateHandlesPosition() {
        /* foreach (GameObject handle in verticesHandles)
        {
            Vector3 newPosition = vertices[handle.GetComponent<HoneyHandle>().GetVertices()[0]]; 
            handle.GetComponent<HoneyHandle>().SetPosition(newPosition);
        } */
        foreach (GameObject handle in edgeHandles)
        {
            GameObject[] tmp = handle.GetComponent<HoneyHandle>().GetHandles(); 
            Vector3 newPosition = new Vector3(0,0,0); 
            for (int i = 0; i < tmp.Length; i++)
            {
                newPosition += tmp[i].transform.position;
            }
            newPosition = newPosition/tmp.Length;
            handle.GetComponent<HoneyHandle>().SetPosition(newPosition);
        }
    }
    public void UpdateVertices(int[] connectedVertices, Vector3 movement) {
        movement.x = movement.x / transform.localScale.x; 
        movement.y = movement.y / transform.localScale.y; 
        movement.z = movement.z / transform.localScale.z; 


        for (int i = 0; i < connectedVertices.Length; i++)
        {
            vertices[connectedVertices[i]] += movement; 
        }
        GetComponent<MeshFilter>().mesh.vertices = vertices;
        GetComponent<MeshFilter>().mesh.RecalculateNormals();
    }
    public void OnDestroy() {
        foreach (GameObject handle in handles)
        {
            Debug.Log("handle destroy");
            Destroy(handle); 
        }
        transform.gameObject.tag="Morphable";
        transform.GetComponent<Rigidbody>().isKinematic = false; 
        transform.GetComponent<Rigidbody>().useGravity = true; 
    }
}
