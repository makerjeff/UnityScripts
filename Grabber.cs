/*=================
 *  Grabber 0.2
 * ================
 * DESCRIPTION:
 * Use this to pick up items by clicking LMB.
 * - stores the original position of the object
 * - locks it into the 'grabber' position (locked to camera)
 * - RMB to put back to original position.
 * ================
 * 2015.MAY.06:
 * 	- implementing grabber function
 * 	shoots out it's own ray, may not be
 * 	optimized.
 * 
 * 2015.MAY.03: beginning of file
 * 
 */

using UnityEngine;
using System.Collections;

public class Grabber : MonoBehaviour {
	
	public float rayLength;	//raylength
	public GameObject pickedUpLocation; //where to send object after being picked up
	private GameObject targetOBJ;	//target object

	//selection holdTime
	public float holdTime;
	
	//stores the original location of the object picked
	private Vector3 originalLocation;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		originalLocation = targetOBJ.transform.position;
		//Debug.DrawRay(this.transform, this.transform.forward,
	}
}
