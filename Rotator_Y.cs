using UnityEngine;
using System.Collections;

public class Rotator_Y : MonoBehaviour {

	[SerializeField] private float rotationSpeed;

	void Update () {

		this.gameObject.transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed);
	}
}
