using UnityEngine;

public class LevelManager : MonoBehaviour
{
  public GameObject objectPrefab; // Assign your object prefab in the inspector

  public void RespawnObject()
  {
    if (objectPrefab != null)
    {
      Instantiate(objectPrefab, transform.position, transform.rotation);
    }
    else
    {
      Debug.LogError("LevelManager: Object prefab not assigned!");
    }
  }
}
