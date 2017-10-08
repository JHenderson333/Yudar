using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : NPC {



    protected abstract IEnumerator attack(GameObject target);

    public abstract GameObject findPlayer();

    public abstract Vector2 calcMoveInput(Vector2 playerPos);
}
