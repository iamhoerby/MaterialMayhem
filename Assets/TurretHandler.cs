using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHandler : MonoBehaviour
{
    public customMaterial material;
    public float projectileSpeed; 
    GameObject projectilePrefab;
    Vector3 shootPoint; 
    // Start is called before the first frame update
    void Start()
    {
        projectilePrefab = Resources.Load("Bullet") as GameObject;
        shootPoint = gameObject.transform.Find("SpawnPoint").transform.position; 
        InvokeRepeating("LaunchProjectile", 1.0f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LaunchProjectile() {
        GameObject newProjectile = Instantiate(projectilePrefab, shootPoint, gameObject.transform.rotation);
        newProjectile.GetComponent<BulletHandler>().SetMaterial(material);
        newProjectile.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * projectileSpeed,ForceMode.Force);
    }
}
