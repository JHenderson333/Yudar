using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {
    PlayerScript playerScript;
    Image healthbar;
	// Use this for initialization
	void Start () {
        GameObject player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        healthbar = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        healthbar.fillAmount = (float)playerScript.getHealth() / (float)playerScript.maxHealth;
	}
}
