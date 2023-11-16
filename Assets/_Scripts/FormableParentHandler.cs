using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormableParentHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private string[] faces = {"top","bottom","north","south","east","west"};
    Dictionary<string, GameObject> handles = new Dictionary<string, GameObject>() {};
    public Vector3 newPos = new Vector3(0,0,0);
    public Vector3 newScale = new Vector3(1,1,1);
    public GameObject formableObject; 
    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;
    void Start()
    {
        var counter = 0; 
        foreach (Transform child in transform){
            if (child.gameObject.name == "Handle"){
                handles.Add(faces[counter], child.gameObject);
                counter++;
            }
            
        }
        //calculateFaces(); 
        Mesh mesh = new Mesh();
        formableObject.GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = handlesToArray();
        mesh.uv = newUV;
        mesh.triangles = newTriangles;

    }

    // Update is called once per frame
    void Update()
    {
        //calculateFaces(); 
        //calculateScale();
        //calculatePosition();
        //updateObject(); 
        Debug.Log("Position: " + newPos);
        Debug.Log("Scale: " + newScale); 
        updateVertices(); 
    }
    void updateVertices() {
        Mesh mesh = formableObject.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = handlesToArray();


    }
    void calculateFaces() {
        Dictionary<string, GameObject> newHandles = handles; 

        foreach (string key in faces)
        {
            GameObject handle = handles[key];
            if (handle.transform.position.y >= handles["top"].transform.position.y) {
                newHandles["top"] = handle; 
            }
            if (handle.transform.position.y <= handles["bottom"].transform.position.y) {
                newHandles["bottom"] = handle; 
            }
            if (handle.transform.position.x <= handles["north"].transform.position.x) {
                newHandles["north"] = handle; 
            }
            if (handle.transform.position.x >= handles["south"].transform.position.x) {
                newHandles["south"] = handle; 
            }
            if (handle.transform.position.z >= handles["east"].transform.position.z) {
                newHandles["east"] = handle; 
            }
            if (handle.transform.position.z <= handles["west"].transform.position.z) {
                newHandles["west"] = handle; 
            }
        }
        handles = newHandles; 
    }
    void calculatePosition() {
        newPos.x = handles["north"].transform.position.x + (handles["south"].transform.position.x - handles["north"].transform.position.x)/2;
        newPos.y = handles["bottom"].transform.position.y + (handles["top"].transform.position.y - handles["bottom"].transform.position.y) / 2;
        newPos.z = handles["west"].transform.position.z + (handles["east"].transform.position.z - handles["west"].transform.position.z) / 2;
    }
    void calculateScale() {
        newScale.x = handles["south"].transform.position.x - handles["north"].transform.position.x;
        newScale.y = handles["top"].transform.position.y - handles["bottom"].transform.position.y;
        newScale.z = handles["east"].transform.position.z - handles["west"].transform.position.z;
    }
    void updateObject() {
        formableObject.transform.position = newPos; 
        formableObject.transform.localScale = newScale;
    }
    Vector3[] handlesToArray() {
        Vector3[] tmp = new Vector3[handles.Count];
        int i = 0; 
        foreach (var obj in handles)
        {
            tmp[i] = obj.Value.transform.position;
            i++;
        }
        
        return tmp;
    }
}
