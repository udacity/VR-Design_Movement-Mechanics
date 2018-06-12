using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {

	public float speed;
	public float xMax, yMax, zMax;

	private float attractorX, attractorY, attractorZ;
	private Vector3 attractorPoint;
	private int counter, counterMax;


	void Start () {
		GeneratePoint (); // Generate our first point
		counter = 0;
		counterMax = 60;
	}


	void Update () {
		this.GetComponent<Rigidbody> ().AddForce ((attractorPoint - transform.position).normalized * speed);
		counter++;
		if (counter >= counterMax) {
			GeneratePoint ();
			counter = 0;
		}
	}


	// Generates a random point within the specified bounds
	void GeneratePoint () {
		attractorX = Random.Range (0, xMax) - (xMax / 2);
		attractorY = Random.Range (0, yMax) - (yMax / 2);
		attractorZ = Random.Range (0, zMax) - (zMax / 2);
		attractorPoint = new Vector3 (attractorX, attractorY, attractorZ);
	}
}
