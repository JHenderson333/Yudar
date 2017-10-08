using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HealthBarScript : NetworkBehaviour {
    public GameObject player;
    PlayerScript playerScript;
    Image healthbar;
	// Use this for initialization
	void Start () {
        healthbar = gameObject.GetComponent<Image>();
        playerScript = player.GetComponent<PlayerScript>();
	}
	
	// Update is called once per frame
	void Update () {
        healthbar.fillAmount = (float)playerScript.getHealth() / (float)playerScript.maxHealth;
	}


}
