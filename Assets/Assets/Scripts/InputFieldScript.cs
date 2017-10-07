using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputFieldScript : MonoBehaviour {
    int lineCount;
    GameObject content;
    Text chatText;
    InputField input;
	// Use this for initialization
	void Start () {
        lineCount = 0;
        content = GameObject.Find("Content");
        chatText = content.GetComponentInChildren<Text>();
		input = gameObject.GetComponent<InputField> ();
		var se = new InputField.SubmitEvent ();
		se.AddListener (SubmitMessage);
		input.onEndEdit = se;
	}
	
	private void SubmitMessage(string arg0){
        Debug.Log("message submitted \n");
        lineCount++;
        chatText.text = chatText.text + "\n" + arg0;
        Debug.Log(chatText.text);
        input.text = "";
	}

}
