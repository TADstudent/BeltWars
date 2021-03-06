﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject startVFX;
    public Vector2 direction;

    public GameObject ship;
    Vector3 blockPosition;
    Quaternion blockRotation;


    private List<ParticleSystem> laserParticles = new List<ParticleSystem>();

    // Start is called before the first frame update
    void Start()
    {
        if(GlobalVariables.local)
            initialize();

    }

    // Update is called once per frame
    void Update()
    {
        //block the ship movement while fireing otherwise the laser becomes too powerful
        blockShipPosition();

    }

    public void blockShipPosition()
    {
        if (ship != null)
        {
            ship.transform.position = blockPosition;
            ship.transform.rotation = blockRotation;
        }
    }

    void playLaserParticle()
    {
        for (int i = 0; i < startVFX.transform.childCount; i++)
        {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();

            if (ps != null)
            {
                laserParticles.Add(ps);
            }
        }

        for (int i = 0; i < laserParticles.Count; i++)
        {
            laserParticles[i].Play();
        }
    }


    IEnumerator laserFires()
    {
        BoxCollider2D boxCollider = lineRenderer.GetComponent<BoxCollider2D>();
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }

    public void initialize()
    {
        blockPosition = ship.transform.position;
        blockRotation = ship.transform.rotation;
        BoxCollider2D boxCollider = lineRenderer.GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;

        playLaserParticle();
        StartCoroutine(laserFires());
    }
}
