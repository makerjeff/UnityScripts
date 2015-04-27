/*============================
 *= Reticule controller v0.1 =
 *============================

 */

using UnityEngine;
using System.Collections;

public class Reticule : MonoBehaviour {

	// game object to aim at (the player, aka firstPersonCharacter)
	public GameObject FPS_Controller;

	//size of reticule
	public float retScale = 1.0f;

	//find the current scale
	private Vector3 currentRetScale;

	//
	private Vector3 reticuleSize;

	// Use this for initialization
	void Start () {

		//handle on the current scale (in object transform)
		currentRetScale = this.transform.localScale;
		//write out to constole the current scale
		Debug.Log (this.transform.localScale);

		//multiply current with retScale
		reticuleSize = new Vector3(retScale * currentRetScale.x , retScale * currentRetScale.y, retScale * currentRetScale.z);


	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(FPS_Controller.transform.position);
		transform.Rotate(90f, 0.0f, 0.0f);

		transform.localScale = reticuleSize;
	}
}
