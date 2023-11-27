using UnityEngine;

public class ShootingUnlockButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Unlock the shooting ability
            other.GetComponent<FPSController>().UnlockShooting();
            
            // Optionally, you can add visual feedback or deactivate the button
            gameObject.SetActive(false);
        }
    }
}
