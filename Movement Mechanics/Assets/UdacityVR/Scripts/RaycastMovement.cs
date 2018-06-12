using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastMovement : MonoBehaviour {

	public GameObject raycastHolder;
	public GameObject player;
	public GameObject raycastIndicator;

	public float height;
	public bool teleport;
	public float maxMoveDistance;

	private bool moving;
	private RaycastHit hit;


	void Update () {
		Vector3 forwardDir = raycastHolder.transform.TransformDirection (Vector3.forward) * 100f;

		if (Physics.Raycast (raycastHolder.transform.position, forwardDir, out hit)) {
			if (hit.collider.gameObject.tag == "movementCapable") {
				ManageIndicator ();
				if (hit.distance <= maxMoveDistance) { // If we are close enough
					if (raycastIndicator.activeSelf == false) { // If the indicator isn't active already make it active.
						raycastIndicator.SetActive (true);
					}
					if (Input.GetMouseButtonDown (0)) {
						if (teleport) {
							teleportMove (hit.point);
						} else {
							DashMove (hit.point);
						}
					}
				} else {
					if (raycastIndicator.activeSelf == true) {
						raycastIndicator.SetActive (false);
					}
				}
			}
		}
	}


	public void ManageIndicator () {
		if (!teleport) {
			if (moving != true) {
				raycastIndicator.transform.position = hit.point;
			}
			if (Vector3.Distance (raycastIndicator.transform.position, player.transform.position) <= 2.5f) {
				moving = false;
			}
		} else {
			raycastIndicator.transform.position = hit.point;
		}
	}


	public void DashMove (Vector3 location) {
		moving = true;
		iTween.MoveTo (player,
			iTween.Hash (
				"position", new Vector3 (location.x, location.y + height, location.z),
				"time", .2f,
				"easetype", "linear"
			)
		);
	}


	public void teleportMove (Vector3 location) {
		player.transform.position = new Vector3 (location.x, location.y + height, location.z);
	}
}
