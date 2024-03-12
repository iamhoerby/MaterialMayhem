using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class JetpackHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public int fuelMax; 
    public float jetpackPower; 
    public GameObject player; 
    float fuel = 0.0f; 
    public InputActionProperty jetpackAction;
    void Start()
    {
        fuel = fuelMax; 
    }

    // Update is called once per frame
    void Update()
    {
        GameObject output = GameObject.Find("Fuel/BarCover"); 
        if (jetpackAction.action.IsPressed()) {
            if (fuel > 0) {
                fuel -= Time.deltaTime; 
                thrust();
            }           
        } else {
            if (fuel < fuelMax) {
                fuel += Time.deltaTime; 
            }
        }
        float percentage = fuel/fuelMax; 
        output.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 18*(1-percentage));
        
    }
    void thrust() {
        player.GetComponent<CharacterController>().Move(Vector3.up * jetpackPower * Time.deltaTime); 
    }
}
