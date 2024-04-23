using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Transform FPS;
    public GameObject hand;

    public bool canPickup = false;
    public LayerMask gunLayerMask;

    public int pickupDistance = 5;


    public TextMeshPro healthText;

    private GunScript gun;
    private Health health;

    void Start()
    {
        gun = hand.GetComponentInChildren<GunScript>();
        health = GetComponent<Health>();
    }

    public void Damage(int amount)
    {
        health.health -= amount;
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Shoot
        {
            gun.Shoot();
        }

        canPickup = false;
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, gunLayerMask))
        {
            if (hit.transform.CompareTag("Gun") && hit.distance < pickupDistance)
            {
                canPickup = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (canPickup)
            {
                Transform oldGun = hand.transform.GetChild(0);
                oldGun.SetParent(null);
                oldGun.GetComponent<BoxCollider>().enabled = true;
                oldGun.GetComponent<Rigidbody>().isKinematic = false;
                oldGun.transform.position += Camera.main.transform.up;
                oldGun.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10);

                Transform newGun = hit.transform;

                newGun.SetParent(hand.transform);
                newGun.GetComponent<BoxCollider>().enabled = false;
                newGun.GetComponent<Rigidbody>().isKinematic = true;
                newGun.position = hand.transform.position;
                newGun.rotation = hand.transform.rotation;

                gun = newGun.gameObject.GetComponent<GunScript>();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            gun.ammo += other.GetComponent<AmmoPickup>().value;
            Destroy(other.gameObject);
        }
    }

   
}
