//========================
//= HIGHLIGHTER v0.3 GIT =
//========================
// use this to highlight an object
// just changed this file to test out Git.
// updated to see if these change.
// adding another change, moving it to version 0.3.

using UnityEngine;
using System.Collections;

public class Highlighter : MonoBehaviour {
	
	private Material baseMaterial;	//current object's shader, get dynamically
	public Material highlightMaterial;	//highlight material (assign in editor)

	private Renderer myRend;	//current object's renderer
	
	public bool isHighlighted = false;	//is this object highlighted?

	// save original position and scales before transform
	public Vector3 currentPosition;
	public Vector3 currentScale;

	// for lerping
	private Vector3 fromVec3;
	private Vector3 toVec3;

	private float diff = 1.5f;
	//private float scale = 1.5f;

	// Use this for initialization
	void Start () {
		//get current object's renderer
		myRend = GetComponent<Renderer>();

//		//reference the base material to return to
		baseMaterial = myRend.material;

		//save the current positions
		currentPosition = this.transform.position;
		currentScale = this.transform.localScale;

		//define the lerp vectors
		this.fromVec3 = currentPosition;
		this.toVec3 = new Vector3(currentPosition.x, currentPosition.y * diff, currentPosition.z);


		// dump debug info
		Debug.Log("Current Renderer: " + myRend);
		Debug.Log("Current material: " + myRend.material);
		Debug.Log("BaseMaterial Variable: " + baseMaterial);
		Debug.Log("Shadow casting mode is: " + myRend.shadowCastingMode);
		Debug.Log("isHighlight: " + isHighlighted);
	}
	
	// Update is called once per frame
	void Update () {
		if (isHighlighted == true) {
			Debug.Log("isHighlighted is TRUE!");
			//scale object
			HighlightMe( 1.5f, 1.5f );
		}

		if (isHighlighted == false) {
			UnHighlightMe();
			
		}

		if (Input.GetButton("Jump")) {
			isHighlighted = true;
		}
		else {
			isHighlighted = false;
		}
	}

//	//CUSTOM FUNCTIONS
	void HighlightMe (float diff, float scale) {
		//tarnsform the object
		//this.transform.position = new Vector3(currentPosition.x, currentPosition.y * diff, currentPosition.z);
		this.transform.position = Vector3.Lerp(this.fromVec3, this.toVec3, 5.0f);
		this.transform.localScale = new Vector3(currentScale.x * scale,currentScale.y *scale,currentScale.z *scale);

		//change the material
		myRend.material = highlightMaterial;
	}
	
	void UnHighlightMe () {
		// return objects to their original place
		this.transform.position = Vector3.Lerp(this.toVec3, this.fromVec3, 5.0f) ;
		this.transform.localScale = currentScale;

		// change the material back
		myRend.material = baseMaterial;

	}
}
