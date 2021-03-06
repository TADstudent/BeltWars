﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class ShipDestruction : MonoBehaviour, IEntity
{
    public float health { get; set; }
    public float maxHealth = 100;
    public HealthbarBehaviour healthbar;
    public GameObject debrisInstance;


    void Start()
    {
        this.Initialize();
    }


    public void Initialize()
    {
        Debug.Log(health);
        this.maxHealth = 100;
        this.health = this.maxHealth;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Damage damage = collision.gameObject.GetComponent<Damage>();

        if (damage)
        {
            this.ApplyDamage(damage.damageValue);
        }

    }

    public void ApplyDamage(float points)
    {
        this.health -= points;

        if (this.health <= 0)
        {
            Explosion();
        }
    }

    public void Explosion()
    {
        Explosion expl = gameObject.GetComponent<Explosion>();
        expl.StartExplosion();
        debrisInstance = GameObject.Instantiate(Resources.Load(ResourcePathConstants.debris) as GameObject as GameObject, gameObject.transform.position, Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("ship_explosion");
    }

}
