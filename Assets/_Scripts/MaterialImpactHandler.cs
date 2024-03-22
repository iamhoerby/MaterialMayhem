using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum customMaterial 
{
    None,
    Default, 
    Honey, 
    Rubber, 
    Ice, 
    Metal, 
    Cardbord
}
public class MaterialImpactHandler : MonoBehaviour
{
    public float impactLimit = 3.0f; 

    public float defaultMass = 1.0f;
    public float metalMass = 10.0f; 
    public float cardbordMass = 0.1f;
    public Material defaultMaterial; 
    public Material honeyMaterial; 
    public Material rubberMaterial; 
    public Material iceMaterial; 
    public Material metalMaterial; 
    public Material cardbordMaterial; 
    public PhysicMaterial rubberPhysic;
    public PhysicMaterial icePhysic;
    public PhysicMaterial honeyPhysic; 
    public PhysicMaterial defaultPhysic;
    //public PhysicMaterial metalPhysic;
    //public PhysicMaterial cardbordPhysic;
    
    public customMaterial currentMaterial; 
    GameObject objectPrefab; 
    Vector3 originalPos; 
    bool isQuitting = false; 
    


    // Start is called before the first frame update
    void Start()
    {
        UpdateMaterial(); 
        objectPrefab = Resources.Load("Default") as GameObject; 
        originalPos = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x) + Mathf.Abs(transform.position.y) + Mathf.Abs(transform.position.z) > 1000.0f){
            Destroy(this); 
        }
    }
    public void SetMaterial(customMaterial material) {
        currentMaterial = material; 
    }
    public customMaterial GetMaterial() {
        return currentMaterial;
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Bullet")) {
            customMaterial newMaterial = other.gameObject.GetComponent<BulletHandler>().GetMaterial(); 
            if (newMaterial != currentMaterial) {
                if (currentMaterial == customMaterial.Honey) {
                    Debug.Log("Destroy Script");
                    Destroy(this.gameObject.GetComponent<HoneyFormable>());
                }
                currentMaterial = newMaterial;
                UpdateMaterial(); 
            }
        } else {
            GetImpactEffect(other.gameObject);
        }
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
        gameObject.GetComponent<Collider>().material = defaultPhysic; 
        gameObject.GetComponent<Rigidbody>().mass = defaultMass; 
    }
    void setHoney() {
        gameObject.GetComponent<MeshRenderer>().material = honeyMaterial;
        gameObject.GetComponent<Rigidbody>().mass = defaultMass;  
        gameObject.AddComponent<HoneyFormable>(); 
        gameObject.GetComponent<Collider>().material = honeyPhysic; 

    }
    void setRubber() {
        gameObject.GetComponent<MeshRenderer>().material = rubberMaterial;
        gameObject.GetComponent<Collider>().material = rubberPhysic; 
        gameObject.GetComponent<Rigidbody>().mass = defaultMass; 
    }
    void setIce() {
        gameObject.GetComponent<MeshRenderer>().material = iceMaterial;
        gameObject.GetComponent<Collider>().material = icePhysic; 
        gameObject.GetComponent<Rigidbody>().mass = defaultMass; 
    }
    void setMetal() {
        gameObject.GetComponent<MeshRenderer>().material = metalMaterial;
        gameObject.GetComponent<Collider>().material = defaultPhysic; 
        gameObject.GetComponent<Rigidbody>().mass = metalMass; 
    }
    void setCardbord() {
        gameObject.GetComponent<MeshRenderer>().material = cardbordMaterial;
        gameObject.GetComponent<Collider>().material = defaultPhysic; 
        gameObject.GetComponent<Rigidbody>().mass = cardbordMass; 
    }
    void GetImpactEffect(GameObject other) {
        switch (currentMaterial)
        {
            case customMaterial.Default: 
                //defaultEffect(other);
                break;
            case customMaterial.Honey: 
                //honeyEffect(other); 
                break;
            case customMaterial.Rubber: 
                //rubberEffect(other);
                break;
            case customMaterial.Ice: 
                //iceEffect(other);
                break;
            case customMaterial.Metal: 
                //metalEffect(other);
                break;
            case customMaterial.Cardbord: 
                cardbordEffect(other);
                break;
            default: 
                //defaultEffect(other); 
                break; 
        }
    }
    private void cardbordEffect(GameObject other) {
        Vector3 otherVelocity = other.GetComponent<Rigidbody>().velocity; 
        Vector3 velocityDifference = gameObject.GetComponent<Rigidbody>().velocity - otherVelocity;  
        Vector3 direction = gameObject.transform.position - other.transform.position; 
        if (direction.y < 0 && other.GetComponent<Rigidbody>().mass >= 1) {
            Destroy(gameObject);

        }
        if (Mathf.Abs(velocityDifference.magnitude) >= impactLimit) {
            Destroy(gameObject); 
        }
    }
    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy() {
            // Object is destroyed, respawn it
            if (!isQuitting) {
                Respawn();
            }
 
    }
    void Respawn() {
        if (Application.isPlaying) {
            Debug.Log("Respawn" + gameObject);
            GameObject newObject = Instantiate(objectPrefab, originalPos, Quaternion.identity);
            newObject.GetComponent<MaterialImpactHandler>().SetMaterial(currentMaterial);
        }
    }
}
