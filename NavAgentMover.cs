/* =====================
 * 	NavAgent Mover v0.1
 * =====================
 * Simple click and move controller for NavMeshAgent
 * 
 * ChangeLog:
 * 2015.MAY.27 (v0.1):
 * 	- setup basic click and move controlls
 *

 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//NEED TO ADD REFERENCE TO ANIMATOR COMPONENT

public class NavAgentMover : MonoBehaviour {

	private NavMeshAgent agent;	//reference to current agent
	[SerializeField]
	private Animator anim;

	public bool debugMode = false;
	public bool debugCurrentPosition = false;
	private bool isMoving;	//is the object moving?


	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire1")){
			Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if(Physics.Raycast(myRay, out hit, 100f)) {
				agent.SetDestination(hit.point);
			}
		}
		//if debugCurrentPosition is on

		if(debugCurrentPosition) {
			Debug.Log (agent.transform.position + ", " + agent.transform.localEulerAngles);
		}


		// if debug mode is on
		if(debugMode){
			if (agent.remainingDistance > 0) {
				Debug.Log ("I'm moving! " + (int)agent.remainingDistance + " units left.");
			}
			else if (agent.remainingDistance == 0) {
				Debug.Log ("I've stopped moving.");
			}
		}

		if(agent.remainingDistance > 0) {
			anim.SetBool ("IsWalking", true);
		}

		else if (agent.remainingDistance == 0) {
			//finished walking
			anim.SetBool("IsWalking", false);
		}
	}
}
