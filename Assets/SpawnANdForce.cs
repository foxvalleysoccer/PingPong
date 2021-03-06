﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using HoloToolkit.Unity;
using UnityEngine.XR;

public class SpawnANdForce : NetworkBehaviour
{

    public GameObject pingPongBallPrefab;
    public Transform ballSpawnPoint;
    public float ballSpeed = 30.0f;
    public float ballLiveTime = 5.0f;
    private float spawnTime = 3;

    private void Start()
    {
        InvokeRepeating("SpwanPongBall", spawnTime, spawnTime);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpwanPongBall();
        }
    }



    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    public void SpwanPongBall()
    {
        if (isServer)
        {
            CmdStartBall();
        }
    }
    [Command]
    public void CmdStartBall()
    {
        GameObject bullet = Instantiate(pingPongBallPrefab);
        bullet.transform.position = ballSpawnPoint.position;

        Vector3 rotation = bullet.transform.rotation.eulerAngles;

        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        //bullet.GetComponent<Rigidbody>().AddForce(ballSpawnPoint.forward * ballSpeed, ForceMode.Impulse);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 12;

        NetworkServer.Spawn(bullet);
        StartCoroutine(DestroyBulletAfterTime(bullet, ballLiveTime));
        Debug.Log("fIRED");
    }
}
