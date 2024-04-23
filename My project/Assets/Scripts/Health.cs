using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int health = 100;
    public bool dead = false;

    public GameObject uiPrefab;

    Transform ui;

    Image healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (uiPrefab == null)
        {
            return;
        }
        foreach(Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                ui = Instantiate(uiPrefab, c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ui)
        {
            ui.position = transform.position + Vector3.up*2.5f; 
            ui.forward = -Camera.main.transform.forward;
        }
    }

    public void Damage(int amount)
    {
        health -= amount;
        healthSlider.fillAmount = health * 0.01f;
    }

    public void Kill()
    {
        if (ui)
        {
            Destroy(ui.gameObject);
        }
    }
}
