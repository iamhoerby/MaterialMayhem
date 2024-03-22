using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class JetpackHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public float fuelMax; 
    public float jetpackPower; 
    public GameObject player; 
    float velocityNow;
    public float velocityMax;  
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
        //if (jetpackAction.action.IsPressed()) {
        if (Input.GetButton("Jump")) {
            if (fuel > 0) {
                fuel -= Time.deltaTime; 
                thrust();
            } else if (fuel < 0.25) {
                if (velocityNow > 0) {
                    velocityNow -= jetpackPower * Time.deltaTime * 0.1f;
                    thrust(); 
                }
            }          
        } else {
            if (velocityNow > 0) 
            {
                velocityNow -= jetpackPower * Time.deltaTime * 0.15f;
                thrust();
            } else {
                velocityNow = 0.0f;
            }
            if (fuel < fuelMax) {
                fuel += Time.deltaTime; 
            } 
        }
        float percentage = fuel/fuelMax; 
        output.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 18*(1-percentage));
        
    }
    void thrust() {
        velocityNow += jetpackPower * Time.deltaTime * 0.05f; 
        if (velocityNow > velocityMax) {
            velocityNow = velocityMax; 
        } 
        Debug.Log(velocityNow);
        player.GetComponent<CharacterController>().Move(Vector3.up * velocityNow); 
    }
}
