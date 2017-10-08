using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class MovingObject : NetworkBehaviour {
    [SyncVar]
    int health;
    bool alive;

    public void setHealth(int health)
    {
        this.health = health;
    }
    public void takeDamage(int damage)
    {
        if (isAlive())
        {
            health -= damage;
        }
    }
    public bool isAlive()
    {
        if(health <= 0)
        {
            alive = false;
            StartCoroutine("die");
        }
        return alive;
    }
    protected void setAlive(bool alive)
    {
        this.alive = alive;
    }

    public int getHealth()
    {
        return health;
    }

    protected abstract IEnumerator die();
    // Use this for initialization
    protected abstract void Start();

    // Update is called once per frame
    protected abstract void Update();

    //Determines the next movement by the object
    protected abstract void Move(Vector2 input);

    //Performs the next movement
    protected abstract IEnumerator SmoothMovement(Vector3 end);
}
