using UnityEngine;
using System.Collections;

public class SimpleTimer : MonoBehaviour {

	private Renderer rend;	//declare reference to current object's Renderer
	public float holdTime = 1.0f;	//declare delay time
	public float held = 0f;		//declare held time variable

	// Use this for initialization
	void Start () {
		Debug.Log ("Starting SimpleTimer, initializing...");	//log start up message
		rend = GetComponent<Renderer>();	//initialize Renderer	
	}
	
	void FixedUpdate () {
		//debugging using the A key to print the current time since start
		if (Input.GetKeyDown (KeyCode.A)) {
			Debug.Log (Time.time);
		}

		//hold for 2 seconds ...
		if (Input.GetButton ("Jump")) {
			held = held + (1 * Time.deltaTime);
			Debug.Log (held);

			if(held >= holdTime) {
				activateObject();
			}
		}
		else {
			held = 0f;
			rend.material.color = Color.white;
		}
	}

	//CUSTOM FUNCTIONS
	void activateObject() {
		rend.material.color = Color.red;
	}
}
