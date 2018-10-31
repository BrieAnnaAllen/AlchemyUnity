using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableManager : MonoBehaviour {

    private GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	}

    void DisableAll()
    {

    }
    public void DisablePlayerMovement()
    {
        PlayerMovement move = player.GetComponent<PlayerMovement>();
        if(move.enabled == true)
        {

            move.enabled = false;
        }
    }
    public void EnablePlayerMovement()
    {
        PlayerMovement move = player.GetComponent<PlayerMovement>();
        if (move.enabled == false)
        {

            move.enabled = true;
        }
    }
   
}
