using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : MonoBehaviour {
    Dictionary<SpellName, Spell> spellBook = new Dictionary<SpellName, Spell>();

	// Use this for initialization
	

    public void addSpell(Spell spell)
    {
        spellBook.Add(spell.identify(), spell);
    }

    public Spell getSpell(SpellName spellkey)
    {
        Spell spell;
        if (spellBook.TryGetValue(spellkey, out spell))
            return spell;
        return null;
    }

    public enum SpellName
    {
        None,
        BlueStrike,
        RedStrike

    }

}
