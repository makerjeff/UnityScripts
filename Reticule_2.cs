/*===================
 * Reticule 2.0 v0.1
 *===================
 * initial GIT commit

 */

using UnityEngine;
using System.Collections;

public class Reticule_2 : MonoBehaviour {

	public bool debugOn = false;

	//FPS controller
	public GameObject player;

	//ray distance
	public float rayLength = 100f;

	//my color
	private Color myColor = new Color(0f, 1.0f, 00f, 0.75f);

	// store the hit location's data
	private RaycastHit myHit;

	//reticule pointer offset
	public Vector3 reticuleOffset = new Vector3(0.0f, 0.0f, 0.0f);




	// Use this for initialization
	void Start () {

		if(debugOn == true) {
			Debug.Log ("Debug mode ON");
		}
		else {
			Debug.Log ("Debug mode OFF");
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(player.transform.position, player.transform.forward * rayLength, myColor);

		if(Physics.Raycast (player.transform.position, player.transform.forward, out myHit, rayLength)) {

			if(debugOn == true) {
				Debug.Log ("Point: " + myHit.point + "Distance: " + myHit.distance);
			}

			this.transform.position = myHit.point;
			this.transform.LookAt(player.transform.position);


		}
	
	}
}
