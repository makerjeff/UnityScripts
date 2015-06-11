/* ===========================
 * 	NetworkPlayerControl v0.1
 * ===========================
 * Simple PlayerController based off of the Paladin PUN
 * tutorial.
 * 
 * ChangeLog:
 * 2015.JUN.11 (v0.1):
 * 	- start of file, cleaning up pseudo-code for GIT.
 * 
 */


using UnityEngine;
using System.Collections;


public class NetworkPlayerControl : Photon.MonoBehaviour {	
//modified to Photon.MonoBehavior, which automatically adds the property
//'photonView'.  Haven't verified it, but it might not require the need to
//create a reference to it.

	//networking related variables
	public PhotonView photonview;	//reference to the photonView

	//LERPing variables
	private float lastSyncTime = 0f;	
	private float syncDelay = 0f;
	private float syncTime = 0f;
	private Vector3 syncStartPos = Vector3.up * 5;	//where the object should start (spawn point)
	private Vector3 syncEndPos = Vector3.zero;	//temp 000

	//color changing variables
	private Renderer rend;

	//numbers
	public float speed = 10f;	//movement speed

	//references
	private Rigidbody rb;	//reference to rigidbody component which we'll use for movement

	//When this script is loaded,
	void Awake () {
		//get a handle on the photonView component,
		photonview = GetComponent<PhotonView>();
		//get a handle on the Rigidbody component,
		rb = GetComponent<Rigidbody>();
		//and get a handle on the Renderer component.
		rend = GetComponent<Renderer>();

	}
	// On every frame,
	void Update () {
		//check to see if instantiated object isMine. If it's true,
		if(photonview.isMine == true) {
			//move the object directly and check to see if I need to change color.
			InputMovement();
			InputColorChange();
		}
		//Otherwise,
		else {
			//Syncronize the movement over the network instead.
			SyncMovement();
		}
	}

	// (Automatically called every time PhotonView either sends or receives data).
	// When Photon sends or receives data,
	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		//check to see if data is being sent.  If so,
		if (stream.isWriting) {
			//send over the rigidbody's position,
			stream.SendNext(rb.position);
			//as well as the velocity (for predictions).
			stream.SendNext (rb.velocity);
		}
		//Otherwise, 
		else {
			//create a new Vector3 to hold the rigidbody's position data,
			Vector3 syncPos = (Vector3)stream.ReceiveNext ();
			//and another Vector3 to hold the rigidbody's velocity data.
			Vector3 syncVel = (Vector3)stream.ReceiveNext ();

			//Setup the Lerping variables.
			syncTime = 0f;
			syncDelay = Time.time - lastSyncTime;	
			lastSyncTime = Time.time;		

			syncEndPos = syncPos + syncVel * syncDelay;	//where to Lerp to
			syncStartPos = (Vector3) rb.position;	//where to start from.
		}
	}

	
	//CUSTOM FUNCTIONS

	// This function randomly changes the colors of your cube.
	void InputColorChange() {
		//If the 'R' key is pressed,
		if(Input.GetKeyDown (KeyCode.R)) {
			//run the ChangeColorTo() variable with a new Vector3 with random X,Y, and Z.
			ChangeColorTo(new Vector3(Random.Range (0f,1f), Random.Range (0f,1f), Random.Range (0f,1f)));
		}
	}

	// Mark this as a RemoteProcedureCall function.
	[RPC] void ChangeColorTo(Vector3 color) {
		// Set MY material color to the input argument (which is random XYZ).
		rend.material.color = new Color(color.x, color.y, color.z, 1f);

		// If this photonView is mine,
		if(photonview.isMine == true) {
			// Send an RCP of this function (ChangeColorTo), to all others buffered so that anyone
			// else joining the room later will also receive the same command upon joining.
			// (photonView.RCP(string nameOfMethod, target, parameters)
			photonview.RPC ("ChangeColorTo", PhotonTargets.OthersBuffered, color);
		}
	}

	// This function assesses movement inputs.
	void InputMovement() {
		//If the 'W' key is pressed,
		if(Input.GetKey (KeyCode.W)) {
			// move the rigidbody forward, multiplied by speed and the time difference between each frame. (deltaTime)
			rb.MovePosition (rb.position + Vector3.forward * speed * Time.deltaTime);
		}
		//Otherwise, if the 'S' key is pressed,
		else if (Input.GetKey (KeyCode.S)) {
			// move the rigidbody backwards, multiplied by speed and deltaTime.
			rb.MovePosition (rb.position - Vector3.forward * speed * Time.deltaTime);
		}
		// If the 'D' key is pressed,
		if(Input.GetKey (KeyCode.D)) {
			// move the rigidbody to the right, multiplied by speed and deltaTime.
			rb.MovePosition (rb.position + Vector3.right * speed * Time.deltaTime);
		}
		//Otherwise, if the 'A' key is pressed,
		else if (Input.GetKey (KeyCode.A)) {
			// move the object to the left, multiplied by speed and deltaTime.
			rb.MovePosition (rb.position - Vector3.right * speed * Time.deltaTime);
		}
	}

	// This function syncronizes movement of object that aren't mine.
	void SyncMovement() {
		syncTime += Time.deltaTime;
		//update the rigidbody's position, smoothly interpolating between the
		//starting position and end position, over x amount of time.
		rb.position = Vector3.Lerp (syncStartPos, syncEndPos, syncTime / syncDelay);
	}
}
