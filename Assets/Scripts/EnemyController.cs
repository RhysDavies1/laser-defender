using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject projectile;

	public float health = 200;
	public float enemyProjectileSpeed;

	public float shotsPerSecond = 5.0f;

	void Update (){ 

		float probability = Time.deltaTime * shotsPerSecond;

		if (Random.value < probability) {
			Fire ();
		}

	}

	void Fire (){

		GameObject missile = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0,-enemyProjectileSpeed);
	
	}

	// Damage an enemy	
	void OnTriggerEnter2D(Collider2D collision){

		Projectile laser = collision.gameObject.GetComponent<Projectile> ();

		if (laser) {
			health -= laser.GetDamage ();
			laser.Hit();

			if (health <= 0){
				Destroy (gameObject);
			}
		}
	}

}
