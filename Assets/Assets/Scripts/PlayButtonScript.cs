using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButtonScript : MonoBehaviour {
    Button playButton;
	// Use this for initialization
	void Start () {
        playButton = gameObject.GetComponent<Button>();
        playButton.onClick.AddListener(loadMainScene);
    }
	

    private void loadMainScene()
    {
        SceneManager.LoadScene("Main Scene", LoadSceneMode.Single);
    }
}
