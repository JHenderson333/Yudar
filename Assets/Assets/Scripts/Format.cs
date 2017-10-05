using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Format{

    /// <summary>
    /// Converts mouse position to grid format
    /// </summary>
    /// <param name="position"></param>
    public static Vector2 mousePosition(Vector2 position)
    {
        Vector3 newVec = Camera.main.ScreenToWorldPoint(position);
        return new Vector2(Mathf.Round(newVec.x), Mathf.Round(newVec.y));
    }
}
