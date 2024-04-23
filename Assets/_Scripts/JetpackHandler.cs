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
        fuelMax *= 0.1f; 
        jetpackPower *= 0.1f; 
        velocityMax *= 0.01f;
        fuel = fuelMax; 
    }

    // Update is called once per frame
    void Update()
    {
        GameObject output = GameObject.Find("Fuel/BarCover"); 
        if (jetpackAction.action.IsPressed()) {
            
        //if (Input.GetButton("Jump")) {
            Debug.Log("Is Pressed: JUMP");
            if (fuel > 0) {
                fuel -= Time.deltaTime; 
                velocityNow += jetpackPower * Time.deltaTime * 1.0f; 
                thrust();
            } else if (fuel < 0.25 * fuelMax) {
                    velocityNow += jetpackPower * Time.deltaTime * 0.1f;
                    thrust(); 
            }          
        } else {
            velocityNow = 0.0f;
            if (fuel < fuelMax) {
                fuel += Time.deltaTime; 
            } 
        }
        float percentage = fuel/fuelMax; 
        output.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 18*(1-percentage));
        Debug.Log(velocityNow);
        
    }
    void thrust() {
        Debug.Log(jetpackPower);
        Debug.Log(fuelMax); 
        Debug.Log("Thrust with: " + velocityNow);
       
        if (velocityNow > velocityMax) {
            velocityNow = velocityMax; 
        } 
        player.GetComponent<CharacterController>().Move(Vector3.up * velocityNow); 
    }
}
