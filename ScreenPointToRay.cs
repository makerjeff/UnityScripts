/* =============================
 * screen point to ray RnD v0.1
 */

using UnityEngine;
using System.Collections;

public class ScreenPointToRay : MonoBehaviour {
	
	public Camera mainCamera;

	public float rayLength = 50f;

	// Use this for initialization
	void Start () {
		//mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		Debug.Log (mainCamera);
	
	}
	
	// Update is called once per frame
	void Update () {
		shootScreenRay();
	
	}


	void shootScreenRay() {
		if(Input.GetButtonDown ("Fire1")) {

			Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit myHit;

			//draws line
			Debug.DrawRay(myRay.origin, myRay.direction * rayLength, Color.green);

			if(Physics.Raycast(myRay.origin, myRay.direction, out myHit, rayLength)) {
				Debug.Log("I hit " + myHit.collider.name + ", Cap'n!");

				// hightlight

				SimpleHighlight sh = myHit.collider.gameObject.GetComponent <SimpleHighlight>();

				sh.isHighlighted = true;
					
			}
			else{
				Debug.Log ("I hit nothing, Cap'n!");
			}
		}
	}
}
