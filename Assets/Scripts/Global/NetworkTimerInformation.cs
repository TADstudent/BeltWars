﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

/**
 * Ship Information the same way
 */
public class NetworkTimerInformation : NetworkBehaviour
{
    [SyncVar]
    int timeLeft;

       // Update is called once per frame
       void Update()
    {
        getTime();
        displayInformation();
    }

    public void displayInformation()
    {
       
      gameObject.GetComponent<Text>().text = "" + timeLeft + " ";
    }

    [Server]
    void getTime()
    {
        timeLeft = (int)GameObject.Find("NetworkGameController").GetComponent<NetworkGameController>().timer;
    }
}