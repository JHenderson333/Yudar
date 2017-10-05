using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueStrikeSpell : Spell {
    private float base_damage = 10;
    private readonly float cast_time = 1f;

    private void Awake()
    {
        Start();
        setCastTime(cast_time);
        setType(SpellBook.SpellName.BlueStrike);
    }

    // Update is called once per frame
    void Update () {
		
	}

    private float getBaseDamange()
    {
        return base_damage;
    }
    
}
