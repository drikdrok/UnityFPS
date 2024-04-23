using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIScript : MonoBehaviour
{

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI pickupText;

    public GameObject playerHand;
    public PlayerController playerController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = "Ammo: " + playerHand.GetComponentInChildren<GunScript>().ammo;
        pickupText.enabled = playerController.canPickup;

        healthText.text = "Health: " + playerController.GetComponent<Health>().health;
    }
}
