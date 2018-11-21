using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using HoloToolkit.Unity;
using UnityEngine.XR;

public class MyVRPlayerController : NetworkBehaviour
{
    public GameObject head;
    public GameObject rightHand;
    public GameObject leftHand;

    [SyncVar]
    private Vector3 localPosition;
    [SyncVar]
    private Quaternion localRotation;
    [SyncVar]
    private Vector3 remoteRightControllerPosition;
    [SyncVar]
    private Vector3 remoteLeftControllerPosition;
    [SyncVar]
    private Quaternion remoteRightControllerRotation;
    [SyncVar]
    private Quaternion remoteLeftControllerRotation;
    [SyncVar]
    private Quaternion remoteHeadRotation;

    private void OnEnable()
    {
        InputTracking.nodeAdded += InputTracking_nodeAdded;
        InputTracking.nodeRemoved += InputTracking_nodeRemoved;
    }
    private void OnDisable()
    {
        InputTracking.nodeAdded -= InputTracking_nodeAdded;
        InputTracking.nodeRemoved -= InputTracking_nodeRemoved;
    }

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            head.GetComponent<Renderer>().material.color = Color.green;
            rightHand.GetComponent<Renderer>().material.color = Color.yellow;
            leftHand.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            head.GetComponent<Renderer>().material.color = Color.red;
            rightHand.GetComponent<Renderer>().material.color = Color.black;
            leftHand.GetComponent<Renderer>().material.color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        InputTracking.nodeAdded += InputTracking_nodeAdded;
        InputTracking.nodeRemoved += InputTracking_nodeRemoved;

        if (!isLocalPlayer)
        {
            rightHand.transform.localRotation = remoteRightControllerRotation;
            leftHand.transform.localRotation = remoteLeftControllerRotation;
            rightHand.transform.localPosition = remoteRightControllerPosition;
            leftHand.transform.localPosition = remoteLeftControllerPosition;
            head.transform.localRotation = remoteHeadRotation;
            return;
        }

        remoteLeftControllerRotation = InputTracking.GetLocalRotation(XRNode.LeftHand);
        remoteRightControllerRotation = InputTracking.GetLocalRotation(XRNode.RightHand);

        remoteRightControllerPosition = (InputTracking.GetLocalPosition(XRNode.RightHand) - CameraCache.Main.transform.localPosition);
        remoteLeftControllerPosition = (InputTracking.GetLocalPosition(XRNode.LeftHand) - CameraCache.Main.transform.localPosition);

        rightHand.transform.rotation = remoteRightControllerRotation;
        leftHand.transform.rotation = remoteLeftControllerRotation;
        rightHand.transform.localPosition = remoteRightControllerPosition;
        leftHand.transform.localPosition = remoteLeftControllerPosition;

        head.transform.rotation = CameraCache.Main.transform.localRotation;
        transform.position = CameraCache.Main.transform.position;
        transform.rotation = CameraCache.Main.transform.parent.rotation;
        CmdTransform(CameraCache.Main.transform.position, CameraCache.Main.transform.rotation, leftHand.transform.rotation, rightHand.transform.rotation, rightHand.transform.localPosition, leftHand.transform.localPosition, head.transform.rotation);
    }

    private void InputTracking_nodeRemoved(XRNodeState obj)
    {
        if (obj.nodeType == XRNode.LeftHand)
        {
            leftHand.SetActive(false);
        }

        if (obj.nodeType == XRNode.RightHand)
        {
            rightHand.SetActive(false);
        }

        InputTracking.nodeRemoved -= InputTracking_nodeRemoved;
    }

    private void InputTracking_nodeAdded(XRNodeState obj)
    {
        if (obj.nodeType == XRNode.LeftHand)
        {
            leftHand.SetActive(true);
        }

        if (obj.nodeType == XRNode.RightHand)
        {
            rightHand.SetActive(true);
        }
    }


    /// Sets the localPosition and localRotation on clients.
    /// </summary>
    /// <param name="postion">the localPosition to set</param>
    /// <param name="rotation">the localRotation to set</param>
    [Command(channel = 1)]
    public void CmdTransform(Vector3 position, Quaternion rotation, Quaternion rightHandRotation, Quaternion leftHandRotation, Vector3 rightControllerPosition, Vector3 leftControllerPosition, Quaternion headRotation)
    {
        localPosition = position;
        localRotation = rotation;
        remoteRightControllerRotation = rightHandRotation;
        remoteLeftControllerRotation = leftHandRotation;
        remoteLeftControllerPosition = leftControllerPosition;
        remoteRightControllerPosition = rightControllerPosition;
        remoteHeadRotation = headRotation;
    }
}
