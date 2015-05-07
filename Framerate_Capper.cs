using UnityEngine;
using System.Collections;

public class Framerate_Capper : MonoBehaviour {

	public int maxFrameRate = 60;

	void Awake() {
		Application.targetFrameRate = maxFrameRate;
	}
}
