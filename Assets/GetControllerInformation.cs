using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetControllerInformation : MonoBehaviour {
    public Vector3 leftControllerPosition;
    public Vector3 rightControllerPostion;
    public Quaternion leftControllerRotation;
    public Quaternion rightControllerRotation;

   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        leftControllerPosition = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.LeftHand);
        rightControllerPostion = UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.RightHand);
       
        leftControllerRotation = UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.LeftHand);
        rightControllerRotation = UnityEngine.XR.InputTracking.GetLocalRotation(UnityEngine.XR.XRNode.RightHand);
  
        Debug.Log("Left Controller position: " + leftControllerPosition);
        Debug.Log("Left Controller Rotation: " + leftControllerRotation);
        Debug.Log("Right Controller position: " + rightControllerPostion);
        Debug.Log("Right Controller rotation: " + rightControllerRotation);
    }
}
