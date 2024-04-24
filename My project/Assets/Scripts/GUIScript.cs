using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIScript : MonoBehaviour
{

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI pickupText;
    public TextMeshProUGUI scoreText;

    public GameObject playerHand;
    public PlayerController playerController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pickupText.enabled = playerController.canPickup;

        ammoText.text = "Ammo: " + playerHand.GetComponentInChildren<GunScript>().ammo;
        healthText.text = "Health: " + playerController.GetComponent<Health>().health;
        scoreText.text = "Score: " + playerController.score;
    }
}
