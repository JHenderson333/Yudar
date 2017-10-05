using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueStrikeSpell : Spell {
    private float base_damage = 10;
	// Use this for initialization
	void Start () {
        setCastTime(1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private float getBaseDamange()
    {
        return base_damage;
    }
    
}
