/*===============
 *  Grabber 0.1
 * ==============
 * DESCRIPTION:
 * Use this to pick up items by clicking LMB.
 * - stores the original position of the object
 * - locks it into the 'grabber' position (locked to camera)
 * - RMB to put back to original position.
 * 
 * 2015.MAY.03: beginning of file
 * 
 */

using UnityEngine;
using System.Collections;

public class Grabber : MonoBehaviour {

	//raylength
	public float rayLength;

	//temporary object
	private GameObject targetOBJ;

	//stores the original location of the object picked
	private Vector3 originalLocation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.DrawRay(this.transform, this.transform.forward,
	}
}
