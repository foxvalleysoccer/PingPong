using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnball : MonoBehaviour {

    public GameObject ball;
    public GameObject spawnpoint;
    public float spawnTime = 3f;
    // Use this for initialization
    void Start()
    {
        InvokeRepeating("SpawnBall", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void SpawnBall()
    {
        var newBall = GameObject.Instantiate(ball, spawnpoint.transform);
    }
}
