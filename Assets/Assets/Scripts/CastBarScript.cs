using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastBarScript : MonoBehaviour {
    public GameObject player;
    Image castbar;
    Image backbar; // background bar
    float castStartTime;
    float castTime;
    PlayerScript playerScript;


	// Use this for initialization
	void Start () {
        if (player.GetComponent<PlayerScript>().isLocalPlayer)
        {
            playerScript = player.GetComponent<PlayerScript>();
            castbar = gameObject.GetComponent<Image>();
            backbar = gameObject.GetComponentInChildren<Image>();
            disable();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (player.GetComponent<PlayerScript>().isLocalPlayer)
        {
            if (playerScript.isCasting() && !castbar.enabled)
            {
                activateCastBar(playerScript.getCurrentCastTime());
                fillBar();
            }
            if (castbar.isActiveAndEnabled)
            {
                fillBar();
            }
        }
        
	}
    public void activateCastBar(float castTime)
    {
        enable();
        this.castTime = castTime;
        castStartTime = Time.time;
    }

    private void enable()
    {
        castbar.enabled = true;
        backbar.enabled = true;
    }

    private void disable()
    {
        castbar.enabled = false;
        backbar.enabled = false;
    }

    private void fillBar()
    {
        float curTime = Time.time;
        float fillAmount = Time.time - castStartTime / castTime;
        if (fillAmount >= 1)
        {
            fillAmount = 1;
            castbar.fillAmount = fillAmount;
            disable();
        }
        else
        {
            castbar.fillAmount = fillAmount;
        }
    }
}
