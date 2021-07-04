using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{

    public Transform firePoint;

    public GameObject bulletPrefab;

    public AudioSource shootAudio;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(name = "Fire1")) {
            Shoot();
        }
    }
    void Shoot() {
        shootAudio.Play();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
