using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spell : MonoBehaviour {
    private float castTime, timeout;
    private SpellBook.SpellName type;
    private Animator spellAnimation;
    private Transform position;
    private SpriteRenderer render;


	// Use this for initialization
	protected void Start () {
        spellAnimation = GetComponent<Animator>();
        position = GetComponent<Transform>();
        render = GetComponent<SpriteRenderer>();
        render.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public SpellBook.SpellName identify()
    {
        return type;
    }

    public void cast(Vector2 pos)
    {
        setPosition(pos);
        render.enabled = true;
    }

    public void setCastTime(float castTime)
    {
        this.castTime = castTime;
    }

    public float getCastTime()
    {
        return castTime;
    }

    private Animator getAnimation()
    {
        return spellAnimation;
    }

    private void setPosition(Vector2 position)
    {
        this.position.position = position;
    }

    protected void setType(SpellBook.SpellName type)
    {
        this.type = type;
    }

    protected void setTimeout(float timeout)
    {
        this.timeout = timeout;
    }

    protected Transform getTransform()
    {
        return position;
    }

}
