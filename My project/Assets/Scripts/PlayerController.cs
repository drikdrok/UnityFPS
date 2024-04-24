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

    public GameObject WinScreen;
    public GameObject DeathScreen;
    public GameObject HUD;

    public int score = 0;

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
        if (health.health <= 0)
        {
            DeathScreen.SetActive(true);
            HUD.SetActive(false);
            GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Cursor.lockState == CursorLockMode.Locked) // Shoot
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
        else if (other.gameObject.CompareTag("Health"))
        {
            health.health += other.GetComponent<HealthPickup>().value;
            if (health.health > 100)
            {
                health.health = 100;
            }
            Destroy(other.gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (score >= 1000)
        {
            WinScreen.SetActive(true);
            HUD.SetActive(false);
            GetComponent<FirstPersonController>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

   
}
