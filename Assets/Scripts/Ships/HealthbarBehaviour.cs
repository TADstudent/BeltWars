﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehaviour : MonoBehaviour
{
    public Slider slider;
    public Color low;
    public Color high;
    public Vector3 offset;
    public float health;
    public float maxHealth;
    public GameObject ship;


    void Update()
    {
        setHealth();

        if(checkShipActive() || health >= maxHealth)
        {
            slider.gameObject.SetActive(false);
        }
        else
        {
            slider.gameObject.SetActive(true);
        }
        
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }


    private bool checkShipActive()
    {
        if (GlobalVariables.singlePlayer && ship.name.StartsWith("Ship_Mars"))
        {
            return ship.GetComponent<AIBehaviour>().active;
        }
        else
        {
            return ship.GetComponent<ProtoMovement>().active;
        }
    }

    private void setHealth()
    {
        slider.value = this.health = ship.GetComponent<ShipDestruction>().health;
        slider.maxValue = this.maxHealth = ship.GetComponent<ShipDestruction>().maxHealth;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, slider.normalizedValue);
    }


}
