using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class GunScript : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;

    public float cooldownTime = 0.2f;
    public int ammo = 100;
    public int minDamage = 30;
    public int maxDamage = 30;

    private Transform cam;
    private bool canShoot = true;


    void Start()
    {
        cam = Camera.main.transform;
        
    }

    void Update()
    {

    }

    private IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

    public void Shoot()
    {
        if (canShoot && ammo > 0)
        {
            BulletScript bullet = Instantiate(bulletPrefab, spawnPoint.position, cam.rotation).GetComponent<BulletScript>();
            bullet.minDmg = minDamage;
            bullet.maxDmg = maxDamage;
            ammo--;
            StartCoroutine(Cooldown());
        }
    }
}
