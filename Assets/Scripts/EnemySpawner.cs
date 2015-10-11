﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;

	public float spawnerWidth = 10f;
	public float spawnerHeight = 5f;

	public float formationSpeed = 15f;

	private float xMin;
	private float xMax;

	private bool formationDirection = true;

	void Start () {
	
		//Setup screen boundaries
		float zDist = transform.position.z - Camera.main.transform.position.z;
		
		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, zDist));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, zDist));
		
		xMin = leftMost.x + (spawnerWidth * 0.5f);
		xMax = rightMost.x - (spawnerWidth * 0.5f);

		//Spawn enemies for each position in the formation
		foreach (Transform child in transform) {
			
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;

		}
	}

	void Update () {

		//Move our formation
		MoveFormation ();

	}

	void MoveFormation(){

		//Update direction
		if (transform.position.x <= xMin) {			
			formationDirection = false;		
		} else if (transform.position.x >= xMax){
			formationDirection = true;
		}

		// Move the formation
		if (formationDirection) {
			transform.position += Vector3.left * formationSpeed * Time.deltaTime;
		} else {
			transform.position += Vector3.right * formationSpeed * Time.deltaTime;
		}

		//Clamp the formation to the edges of the screen
		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

	}

	//Draw the boundaries of our formation in the editor
	public void OnDrawGizmos(){
		
		Gizmos.DrawWireCube (transform.position, new Vector3 (spawnerWidth, spawnerHeight, 0));
		
	}
}
