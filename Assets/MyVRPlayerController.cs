using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using HoloToolkit.Unity;

public class MyVRPlayerController : NetworkBehaviour
{

    /// The position relative to the shared world anchor.
    [SyncVar]
    private Vector3 localPosition;

    /// The rotation relative to the shared world anchor.
    [SyncVar]
    private Quaternion localRotation;


    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }
        // if we are the remote player then we need to update our worldPosition and then set our 
        // local (to the shared world anchor) position for other clients to update our position in their world.
        transform.position = CameraCache.Main.transform.position;
        transform.rotation = CameraCache.Main.transform.rotation;
        CmdTransform(transform.localPosition, transform.localRotation);
    }


    /// Sets the localPosition and localRotation on clients.
    /// </summary>
    /// <param name="postion">the localPosition to set</param>
    /// <param name="rotation">the localRotation to set</param>
    [Command(channel = 1)]
    public void CmdTransform(Vector3 postion, Quaternion rotation)
    {
        localPosition = postion;
        localRotation = rotation;
    }
}
