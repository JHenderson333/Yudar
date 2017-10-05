using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Warp : MonoBehaviour {

	public  Transform target;

	void OnTriggerEnter2D(Collider2D other){
		other.gameObject.transform.position = target.position;
	}

	

}
