﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Shoot : NetworkBehaviour
{
    public GameObject loadedWeapon;
    public Transform projectileSpawnPoint;
    private float speed;
    public Weapontype weapontype;
    [SyncVar]
    public bool active = false;
    public int missileAmount = 3;
    public int laserAmount = 0;

    public enum Weapontype
    {
        MACHINE_GUN,
        MISSILE,
        LASERBEAM
     }
    // Start is called before the first frame update
    void Start()
    {
        cmdInitWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (active) 
        { 
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                cmdSetWeapon(0);
            }
            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                cmdSetWeapon(1);
            }
            if (Input.GetKeyDown(KeyCode.Space)) { cmdShoot(); }
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    if (weapontype == Weapontype.MISSILE && missileAmount > 0)
            //    {
            //        StartCoroutine(fire(0.0f));
            //        missileAmount--;
            //        active = false;
            //    }
            //    else if (weapontype == Weapontype.MACHINE_GUN)
            //    {
            //        StartCoroutine(fire(0.0f));
            //        StartCoroutine(fire(0.1f));
            //        StartCoroutine(fire(0.2f));
            //        StartCoroutine(fire(0.3f));
            //        StartCoroutine(fire(0.4f));
            //        StartCoroutine(fire(0.5f));
            //        active = false;
            //    }
            //}
        }
    }


    IEnumerator fire(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        float projectilePosX = projectileSpawnPoint.position.x;
        float projectilePosY = projectileSpawnPoint.position.y;
        Vector3 direction = transform.rotation * Vector3.up;

        GameObject weaponToFire = (GameObject) Instantiate(loadedWeapon, projectileSpawnPoint.position, transform.rotation);
        NetworkServer.Spawn(weaponToFire);
        weaponToFire.GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }



    [Command]
    private void cmdShoot()
    {
        if (weapontype == Weapontype.MISSILE && missileAmount > 0)
        {
            StartCoroutine(fire(0.0f));
            missileAmount--;
            active = false;
        }
        else if (weapontype == Weapontype.MACHINE_GUN)
        {
            StartCoroutine(fire(0.0f));
            StartCoroutine(fire(0.1f));
            StartCoroutine(fire(0.2f));
            StartCoroutine(fire(0.3f));
            StartCoroutine(fire(0.4f));
            StartCoroutine(fire(0.5f));
            active = false;
        }
    }


    [Command]
    private void cmdSetWeapon(int n)
    {

        if(n == 0)
        {
            loadedWeapon = Resources.Load(ResourcePathConstants.MACHINE_GUN) as GameObject;
            weapontype = Weapontype.MACHINE_GUN;
            speed = 1;
            Debug.Log("Machine Gun loaded");
        }else if(n == 1){
            loadedWeapon = Resources.Load(ResourcePathConstants.MISSILE) as GameObject;
            weapontype = Weapontype.MISSILE;
            speed = 5;
            Debug.Log("Rocket Launcher loaded");
        }

    }

    [Command]
    private void cmdInitWeapon()
    {
        GameObject wep = Resources.Load(ResourcePathConstants.MACHINE_GUN) as GameObject;
        loadedWeapon = wep;
        weapontype = Weapontype.MACHINE_GUN;
        speed = 1;
        Debug.Log("Machine Gun loaded");
    }
}

