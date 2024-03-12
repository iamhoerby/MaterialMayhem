using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public enum tool {
    MaterialGun, 
    GrablingGloves
} 

public class ToolHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public tool currentTool; 
    public InputActionProperty switchToolAction; 
    public InputActionProperty grabingActionRight;
    public InputActionProperty grabingActionLeft;  
    bool grabingRightActive = false; 
    bool grabingLeftActive = false; 
    bool switchActive; 

    void Start()
    {
        currentTool = tool.MaterialGun; 
        switchTool(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (switchToolAction.action.IsPressed()) {
        //if (Input.GetButton("Jump")) {
            if (!switchActive) {
                switchTool(); 
                switchActive = true; 
            } 
        } else {
            if (switchActive) {
                switchActive = false; 
            }
        }
        GameObject gloveOld;
        GameObject gloveNew; 
        if (currentTool == tool.GrablingGloves) {
            if (grabingActionRight.action.IsPressed()) {
                if (!grabingRightActive) {
                    grabingRightActive = true; 
                    gloveOld = GameObject.Find("gloveRopen");
                    gloveNew = GameObject.Find("gloveRclosed"); 
                    switchModelState(gloveOld,false);
                    switchModelState(gloveNew,true); 
                }
            } else if (grabingRightActive) {
                grabingRightActive = false; 
                gloveOld = GameObject.Find("gloveRclosed");
                gloveNew = GameObject.Find("gloveRopen"); 
                switchModelState(gloveOld,false);
                switchModelState(gloveNew,true); 
            }
            if (grabingActionLeft.action.IsPressed()) {
                if (!grabingLeftActive) {
                    grabingLeftActive = true; 
                    gloveOld = GameObject.Find("gloveLopen");
                    gloveNew = GameObject.Find("gloveLclosed"); 
                    switchModelState(gloveOld,false);
                    switchModelState(gloveNew,true); 
                }
            } else if (grabingLeftActive) {
                grabingLeftActive = false; 
                gloveOld = GameObject.Find("gloveLclosed");
                gloveNew = GameObject.Find("gloveLopen"); 
                switchModelState(gloveOld,false);
                switchModelState(gloveNew,true); 
            }
        }
    }
    void switchTool() {
        GameObject controllerNew; 
        GameObject controllerOld; 
        var xrRayInteractors = GameObject.FindGameObjectsWithTag("RayInteractor"); 
        if (!grabingRightActive && !grabingLeftActive) {
            switch (currentTool)
            {
                case tool.MaterialGun: 
                    currentTool = tool.GrablingGloves;
                    foreach (var interactor in xrRayInteractors)
                    {
                        interactor.transform.GetComponent<XRRayInteractor>().enabled = true; 
                    }
                    gameObject.transform.GetComponent<ShootHandler>().enabled = false; 
                    controllerOld = GameObject.Find("materialgun");
                    if (controllerOld) {
                        switchModelState(controllerOld,false); 
                    }
                    controllerNew = GameObject.Find("gloveRopen");
                    if (controllerNew) {
                        switchModelState(controllerNew,true);
                    }
                    controllerOld = GameObject.Find("materialgunL");
                    if (controllerOld) {
                        switchModelState(controllerOld,false); 
                    }
                    controllerNew = GameObject.Find("gloveLopen");
                    if (controllerNew) {
                        switchModelState(controllerNew,true); 
                    }
                    break; 
                case tool.GrablingGloves: 
                    currentTool = tool.MaterialGun;
                    foreach (var interactor in xrRayInteractors)
                    {
                        interactor.transform.GetComponent<XRRayInteractor>().enabled = false; 
                    }
                    gameObject.transform.GetComponent<ShootHandler>().enabled = true;
                    controllerOld = GameObject.Find("gloveRopen");
                    if (controllerOld) {
                        switchModelState(controllerOld,false); 
                    }
                    controllerNew = GameObject.Find("materialgun");
                    if (controllerNew) {
                        switchModelState(controllerNew,true); 
                    }
                    controllerOld = GameObject.Find("gloveLopen");
                    if (controllerOld) {
                        switchModelState(controllerOld,false); 
                    }
                    controllerNew = GameObject.Find("materialgunL");
                    if (controllerNew) {
                        switchModelState(controllerNew,true); 
                    }
                    break; 
                default:
                break;
            }
        }
    }
    void switchModelState(GameObject parent, bool state) {
        foreach (Transform child in parent.transform) 
        {
            child.gameObject.SetActive(state); 
        }
    }
}
