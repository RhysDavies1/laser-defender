using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float shipSpeed;
	public float projectileSpeed;
	public float fireRate;

	public GameObject playerLaser;

	public float padding = 1f;

	private float xMin;
	private float xMax;

	void Start () {
	
		float zDist = transform.position.z - Camera.main.transform.position.z;

		Vector3 leftMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, zDist));
		Vector3 rightMost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, zDist));

		xMin = leftMost.x + padding;
		xMax = rightMost.x - padding;
	
	}

	void Update () {

		MoveShip ();
		ShootLaser ();
	}

	void MoveShip (){

		if (Input.GetKey(KeyCode.LeftArrow)) {
			//Move left
			transform.position += Vector3.left * shipSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow)) {
			//Move right
			transform.position += Vector3.right * shipSpeed * Time.deltaTime;
		} 

		//Restrict player to game space
		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

	}

	void ShootLaser (){

		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.000001f, fireRate);
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("Fire");
		}
	}

	void Fire (){
		GameObject laser = Instantiate (playerLaser, transform.position, Quaternion.identity) as GameObject;
		laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
		laser.transform.parent = transform;
	}
}
