using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

    // Use this for initialization
    protected abstract void Start();

    // Update is called once per frame
    protected abstract void Update();

    //Determines the next movement by the object
    protected abstract void Move(Vector2 input);

    //Performs the next movement
    protected abstract IEnumerator SmoothMovement(Vector3 end);
}
