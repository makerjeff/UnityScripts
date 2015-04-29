﻿using UnityEngine;
using System.Collections;

public class SimpleHighlight : MonoBehaviour {

	private Material baseMaterial;	//current object's shader, get dynamically
	public Material highlightMaterial;	//highlight material (assign in editor)
	
	private Renderer myRend;	//current object's renderer
	
	public bool isHighlighted = false;	//is this object highlighted?

	// Use this for initialization
	void Start () {

		//get current object's renderer
		myRend = GetComponent<Renderer>();
		
		//reference the base material to return to
		baseMaterial = myRend.material;

	
	}
	
	// Update is called once per frame
	void Update () {

		if(isHighlighted == true) {
			highlightMe ();
		}
		else {
			unHighlightMe();
		}
	
	}


	//custom functions

	void highlightMe () {
		myRend.material = highlightMaterial;
	}

	void unHighlightMe() {
		myRend.material = baseMaterial;
	}
}
