/* ======================
 * 	Network Manager v0.1
 * ======================
 * Based on the Paladin Studio PUN tutorial,
 * this is a simple networking manager script.
 * 
 * ChangeLog:
 * 2015.JUN.11:
 * - Start of file

 */

using UnityEngine;
using System.Collections;


public class NetworkManager : MonoBehaviour {

	//networking variables
	private const string roomName = "RoomName";	// room name string
	private RoomInfo[] roomsList;				// list of rooms available

	//object to spawn
	public GameObject playerPrefab;				// player prefab object
	
	// Use this for initialization
	void Start () {
		//connect to Photon Network as version "0.1".  Only users with the same
		//version number can join
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	//draw on the GUI 
	void OnGUI() {

		//if photon network isn't connecting to the cloud
		if(!PhotonNetwork.connected) {
			//display error in detail
			GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
		}
		// else, if we're connected to the Photon server but we're not in a room:
		else if (PhotonNetwork.room == null) {
			// create a button that creates a room
			if (GUI.Button (new Rect(100,100, 250,100), "Start Server")) {
				//with the name 'roomName', isVisible, isOpen, maxPlayers
				PhotonNetwork.CreateRoom(roomName, true, true, 5);
			}
			//if roomsList isn't empty:
			if(roomsList != null) {
				//cycle through the list
				for (int i = 0; i < roomsList.Length; i++) {
					//and create a button for each of the rooms
					if (GUI.Button (new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].name)){
						//that joins a room with the corresponding name and number.
						PhotonNetwork.JoinRoom (roomsList[i].name);
					}
				}
			}
		}
	}

	//(Automatically called when a room is added or removed)
	//If the RoomList is updated,
	void OnReceivedRoomListUpdate() {
		// load all the information into the 'roomsList' array.
		roomsList = PhotonNetwork.GetRoomList ();
	}

	//(Automatically called after joining a aroom)
	//If we've joined a room,
	void OnJoinedRoom() {
		//tell us in the console.
		Debug.Log ("Connected to a Room!");

		//Immediatedly, Photon-Network-instantiate the object 'playerPrefab' 5 meters above origin, facing it's
		// original rotation, on to group layer 0.
		PhotonNetwork.Instantiate(playerPrefab.name, Vector3.up * 5, Quaternion.identity, 0);
	}

}
