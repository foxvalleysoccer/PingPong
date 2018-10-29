using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerAndCameraLocation : MonoBehaviour {

    GameObject mixedrealitycameraParentPlayer;
    public GameObject Spawn1;

	// Use this for initialization
	void Start () {
        mixedrealitycameraParentPlayer = GameObject.FindWithTag("MainCamera");
        mixedrealitycameraParentPlayer.transform.position = Spawn1.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
