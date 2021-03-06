﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ProtoMovement : MonoBehaviour
{
	public float acceleration_amount = 100f;
	public float rotation_speed = 100f;
	private CameraMeasurements gameCamera;
	public bool active = false;


	public void onStart()
    {
		//gameCamera = new CameraMeasurements();
	}


    // Update is called once per frame
    protected void Update()
    {
		if (GlobalVariables.local)
		{
			keepObjectInCameraView();
			if (active)
			{
				moveShip();
			}
		}
	}


	public void keepObjectInCameraView()
    {
		gameCamera = new CameraMeasurements();
		if ((transform.position.x >= gameCamera.getHorizontalMax()))
		{
			transform.position = new Vector3(gameCamera.getHorizontalMax(), transform.position.y, 0);
		}

		if((transform.position.x <= gameCamera.getHorizontalMin()))
        {
			transform.position = new Vector3(gameCamera.getHorizontalMin(), transform.position.y, 0);
		}

		if ((transform.position.y >= gameCamera.getVerticalMax()))
		{
			transform.position = new Vector3(transform.position.x, gameCamera.getVerticalMax(), 0);
		}

		if ((transform.position.y <= gameCamera.getVerticalMin()))
		{
			transform.position = new Vector3(transform.position.x, gameCamera.getVerticalMin(), 0);
		}
	}

    public void moveShip() {

		if (Input.GetKeyDown(KeyCode.Escape))
			Screen.lockCursor = !Screen.lockCursor;

		if (Input.GetKey(KeyCode.W))
		{
			GetComponent<Rigidbody2D>().AddForce(transform.up * acceleration_amount * Time.deltaTime * 100);
		}

		if (Input.GetKeyDown(KeyCode.W))
        {
			FindObjectOfType<AudioManager>().Play("engine1");
		}
		if (Input.GetKeyUp(KeyCode.W))
        {
			FindObjectOfType<AudioManager>().Stop("engine1");
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			FindObjectOfType<AudioManager>().Play("engine1");
		}
		if (Input.GetKeyUp(KeyCode.S))
		{
			FindObjectOfType<AudioManager>().Stop("engine1");
		}

		if (Input.GetKey(KeyCode.S))
		{
			FindObjectOfType<AudioManager>().Play("engine1");
			GetComponent<Rigidbody2D>().AddForce((-transform.up) * acceleration_amount * Time.deltaTime * 100);
		}

		if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
		{
			GetComponent<Rigidbody2D>().AddForce((-transform.right) * acceleration_amount * 0.6f * Time.deltaTime * 100);
		}

		if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
		{
			GetComponent<Rigidbody2D>().AddForce((transform.right) * acceleration_amount * 0.6f * Time.deltaTime * 100);
		}

		if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift))
		{
			GetComponent<Rigidbody2D>().AddTorque(-rotation_speed * Time.deltaTime * 100);

		}
		if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift))
		{
			GetComponent<Rigidbody2D>().AddTorque(rotation_speed * Time.deltaTime * 100);

		}
		if (Input.GetKey(KeyCode.C))
		{
			GetComponent<Rigidbody2D>().angularVelocity = Mathf.Lerp(GetComponent<Rigidbody2D>().angularVelocity, 0, rotation_speed * 0.06f * Time.deltaTime * 100);
			GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(GetComponent<Rigidbody2D>().velocity, Vector2.zero, acceleration_amount * 0.06f * Time.deltaTime * 100);
		}
	}
}
