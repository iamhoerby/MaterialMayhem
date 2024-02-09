using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public customMaterial currentMaterial; 
    public Material defaultMaterial; 
    public Material honeyMaterial; 
    public Material rubberMaterial; 
    public Material iceMaterial; 
    public Material metalMaterial; 
    public Material cardbordMaterial; 
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMaterial();
    }
    void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }
    public customMaterial GetMaterial() {
        return currentMaterial; 
    }
    public void SetMaterial(customMaterial material) {
        currentMaterial = material;
    }
    void UpdateMaterial() {
        switch (currentMaterial)
        {
            case customMaterial.Default: 
                setDefault();
                break;
            case customMaterial.Honey: 
                setHoney(); 
                break;
            case customMaterial.Rubber: 
                setRubber();
                break;
            case customMaterial.Ice: 
                setIce();
                break;
            case customMaterial.Metal: 
                setMetal();
                break;
            case customMaterial.Cardbord: 
                setCardbord();
                break;
            default: 
                setDefault(); 
                break; 
        }
    }

    void setDefault() {
        gameObject.GetComponent<MeshRenderer>().material = defaultMaterial;
    }
    void setHoney() {
        gameObject.GetComponent<MeshRenderer>().material = honeyMaterial;
    }
    void setRubber() {
        gameObject.GetComponent<MeshRenderer>().material = rubberMaterial;
    }
    void setIce() {
        gameObject.GetComponent<MeshRenderer>().material = iceMaterial;
    }
    void setMetal() {
        gameObject.GetComponent<MeshRenderer>().material = metalMaterial;
    }
    void setCardbord() {
        gameObject.GetComponent<MeshRenderer>().material = cardbordMaterial;
    }
}
