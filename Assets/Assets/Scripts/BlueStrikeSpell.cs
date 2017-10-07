using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueStrikeSpell : Spell {
    HashSet<MovingObject> colliders;
    private int base_damage = 10;
    private readonly float cast_time = 1f;
    private readonly float timeout = 3f;

    private void Awake()
    {
        Start();
        setCastTime(cast_time);
        setTimeout(timeout);
        setType(SpellBook.SpellName.BlueStrike);
        colliders = new HashSet<MovingObject>();
    }

    // Update is called once per frame
    void Update () {
        handleCollision();

	}

    private float getBaseDamange()
    {
        return base_damage;
    }

    new public void cast(Vector2 pos)
    {
        BlueStrikeSpell spell = this;
        BlueStrikeSpell clone = Instantiate(spell, pos, new Quaternion(0, 0, 0, 0));
        clone.setTimeout(timeout);
    }

    public void handleCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(getTransform().position, Vector2.right, 0);
        if(hit.collider.tag == "Player" || hit.collider.tag == "Monster")
        {
            MovingObject obj = hit.collider.GetComponent<MovingObject>();
            if (!colliders.Contains(obj))
            {
                colliders.Add(obj);
                StartCoroutine("damage", obj);
            }
            
        }
    }

    IEnumerator damage(MovingObject obj)
    {
        RaycastHit2D hit = Physics2D.Raycast(getTransform().position, Vector2.right, 0);
        while(null != hit.collider.GetComponent<MovingObject>() && 
                hit.collider.GetComponent<MovingObject>() == obj)
        {
            hit = Physics2D.Raycast(getTransform().position, Vector2.right, 0);
            yield return new WaitForSeconds(1f);
            obj.takeDamage(base_damage);
        }
        colliders.Remove(obj);
        
    }


    
}
