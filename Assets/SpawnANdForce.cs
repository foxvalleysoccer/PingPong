using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnANdForce : MonoBehaviour
{

    public GameObject pingPongBallPrefab;
    public Transform ballSpawnPoint;
    public float ballSpeed = 30.0f;
    public float ballLiveTime = 5.0f;
    private float spawnTime = 3;

    private void Start()
    {
        InvokeRepeating("Fire", spawnTime, spawnTime);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }



    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(pingPongBallPrefab);
        // Physics.IgnoreCollision(bullet.GetComponent<Collider>());
        // Physics.IgnoreCollision(bullet.GetComponent<Collider>(), bulletSpawnpoint.parent.GetComponent<Collider>());
        bullet.transform.position = ballSpawnPoint.position;

        Vector3 rotation = bullet.transform.rotation.eulerAngles;

        bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        bullet.GetComponent<Rigidbody>().AddForce(ballSpawnPoint.forward * ballSpeed, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet, ballLiveTime));
        Debug.Log("fIRED");
    }
}
