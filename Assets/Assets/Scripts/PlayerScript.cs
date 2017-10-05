using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MovingObject {
    public int maxHealth = 100;
    public int health = 100;
	private Animator animator;
	private Rigidbody2D rb2D;
    private SpellBook spells;
    bool alive;

    //Determines if the player is current moving
	bool moving;

    //Multipliers for the speed of the player
    float speed = 10f;

    //Objects for checking if the chat box is selected
    GameObject chatObject;
    InputField chatInput;


    // Use this for initialization
    protected override void Start () {
        alive = true;
		animator = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();
		moving = false;
        chatObject = GameObject.Find("Chat Input");
        chatInput = chatObject.GetComponent<InputField>();

        //creates the spellbook with intial spell
        loadSpells();


    }
	protected override void Update(){
        if(health <= 0)
        {
            alive = false;
            StartCoroutine(die());
            Debug.Log("Player has died");
        }
		float x = Input.GetAxisRaw ("Horizontal"); 
		float y = Input.GetAxisRaw ("Vertical");
        Move(new Vector2(x, y));

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(cast(spells.getSpell(SpellBook.SpellName.BlueStrike)));
        }
	}

		
	protected override void Move (Vector2 input)
	{
        Vector2 dir = new Vector2();
        Vector2 end = transform.position;
        if (chatInput.isFocused || !alive)//Checks if player is currently using the chatbox
            return;

        if (moving || (input.x == 0 && input.y == 0)) //Already moving or no input movement
        {
            return;
        }
        else if (input.x > 0)
        {
            end.x += 1;
            dir = Vector2.right;
        }
        else if (input.x < 0)
        {
            end.x -= 1;
            dir = Vector2.left;
        }
        else if (input.y > 0)
        {
            end.y += 1;
            dir = Vector2.up;
        }
        else if (input.y < 0)
        {
            end.y -= 1;
            dir = Vector2.down;
        }

		animator.SetBool ("iswalking", true);
		animator.SetFloat("input_x", end.x - rb2D.position.x);
		animator.SetFloat("input_y", end.y - rb2D.position.y);

        //Check space to move to for collidable object
        RaycastHit2D hit = Physics2D.Raycast (end, dir, 0); 
		if (hit.collider == null) { //No collider 
			moving = true;
			StartCoroutine (SmoothMovement (end));
		} else if (hit.collider.tag == "Transport") {
			rb2D.MovePosition (end);
			moving = false;
            animator.SetBool("iswalking", false);
        }
		else {
			moving = false;
			animator.SetBool ("iswalking", false);
		}
	}

    protected override IEnumerator SmoothMovement(Vector3 end)
    {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon && moving )
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, speed * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb2D.MovePosition(newPosition);
            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
        moving = false;
        animator.SetBool("iswalking", false);
    }

    protected IEnumerator die()
    {
        StopAllCoroutines();
        rb2D.MovePosition(Vector2.zero);
        transform.position = Vector2.zero;
        health = 100;
        moving = false;
        alive = true;
        yield return null;
    }

    protected IEnumerator cast(Spell spell)
    {
        yield return new WaitForSeconds(spell.getCastTime());
        spell.cast(Format.mousePosition(Input.mousePosition));
        //TODO

    }


    void loadSpells()
    {
        GameObject spellsObject = GameObject.Find("SpellBook");
        spells = spellsObject.GetComponent<SpellBook>();
        GameObject blueStrikeObject = GameObject.Find("blue_strike");
        Spell blueStrikeSpell = blueStrikeObject.GetComponent<BlueStrikeSpell>();
        spells.addSpell(blueStrikeSpell);
    }
}
