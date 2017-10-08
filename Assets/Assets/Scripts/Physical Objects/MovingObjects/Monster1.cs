using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster1 : Monster {
    private int maxHealth = 100;
    private static int moveRange = 20;
    private static float attackRange = 1f;
    private float attackSpeed = 1f;
    private Vector2 spawnPos;
    private float speed = 2f;
    private int damage = 10;
    private bool hasFocus;
    private bool moving;
    private bool canAttack;


    private Animator animator;
    private Rigidbody2D rb2D;

    // Use this for initialization
    protected override void Start () {
        spawnPos = transform.position;
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        moving = false;
        canAttack = true;
    }
	
	// Update is called once per frame
	protected override void Update () {
        GameObject player = findPlayer();
        if (!player)
            return;
        if (!moving && playerInRange(player.transform.position) && canAttack)
            StartCoroutine(attack(player));
        Move(calcMoveInput(player.transform.position));
    }

    protected override void Move(Vector2 input)
    {
        Vector2 dir = new Vector2();
        Vector2 end = transform.position;

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

        animator.SetBool("iswalking", true);
        animator.SetFloat("input_x", end.x - rb2D.position.x);
        animator.SetFloat("input_y", end.y - rb2D.position.y);

        //Check space to move to for collidable object
        handleCollision(end, dir);

    }
    public void handleCollision(Vector2 end, Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(end, dir, 0);
        if (hit.collider == null || hit.collider.tag == "WeakCollisionAbility")
        { //No collider 
            moving = true;
            StartCoroutine(SmoothMovement(end));
        }
        else if (hit.collider.tag == "Transport")
        {
            rb2D.MovePosition(end);
            moving = false;
            animator.SetBool("iswalking", false);
        }
        else
        {
            Debug.Log(hit.collider.tag);
            moving = false;
            animator.SetBool("iswalking", false);
        }
    }
    protected override IEnumerator SmoothMovement(Vector3 end)
    {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon && moving)
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

    protected override IEnumerator attack(GameObject target)
    {
        canAttack = false;
        PlayerScript targetPlayer = target.GetComponent<PlayerScript>();
        if (targetPlayer.isAlive())
        {
            yield return new WaitForSeconds(attackSpeed);
            targetPlayer.takeDamage(damage);
            canAttack = true;

        }
       
    }

    public override GameObject findPlayer()
    {
        // use the -1 because boxcast cannot detect objects at starting point
        Vector2 castStartPoint = new Vector2(spawnPos.x - moveRange - 1, spawnPos.y);
        Vector2 box = new Vector2(1, moveRange * 2);
        RaycastHit2D[] results = Physics2D.BoxCastAll(castStartPoint, box, 0, Vector2.right, moveRange * 2, Physics.DefaultRaycastLayers);
        for(int i = 0; i < results.Length; i++)
        {
           if(results[i].collider.tag == "Player")
            {
                return results[i].collider.gameObject;
            } 
        }
        return null;
    }

    public override Vector2 calcMoveInput(Vector2 playerPos)
    {
        Vector2 input = new Vector2();
        input.x = playerPos.x - rb2D.position.x;
        input.y = playerPos.y - rb2D.position.y;
        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            if(input.x < 0)
            {
                return new Vector2(-1, 0);
            }
            return new Vector2(1, 0);
        }
        else if(input.y < 0)
        {
            return new Vector2(0, -1);
        }
        return new Vector2(0, 1);
    }

    public bool playerInRange(Vector2 playerPos)
    {
        if(Vector2.Distance(playerPos, rb2D.position) <= attackRange)
        {
            return true;
        }
        return false;
    }

    protected override IEnumerator die()
    {
        StopAllCoroutines();
        rb2D.MovePosition(Vector2.zero);
        transform.position = Vector2.zero;
        setHealth(maxHealth);
        moving = false;
        setAlive(true);
        yield return null;
    }
}

