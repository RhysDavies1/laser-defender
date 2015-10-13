using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject projectile;

	public float health = 200;
	public float enemyProjectileSpeed;
	public float shotsPerSecond = 5.0f;
	public int scoreValue = 150;

	public AudioClip enemyFire;
	public AudioClip enemyDamage;
	private float audioClipVolume = 0.5f;

	private ScoreKeeper scoreKeeper;

	void Start(){
	
		scoreKeeper = GameObject.Find ("Score").GetComponent<ScoreKeeper>();
	
	}

	void Update (){ 

		float probability = Time.deltaTime * shotsPerSecond;

		if (Random.value < probability) {
			Fire ();
		}

	}

	void Fire (){

		GameObject missile = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0,-enemyProjectileSpeed);

		AudioSource.PlayClipAtPoint (enemyFire, transform.position, audioClipVolume);
	
	}

	// Damage an enemy	
	void OnTriggerEnter2D(Collider2D collision){

		Projectile laser = collision.gameObject.GetComponent<Projectile> ();

		if (laser) {
			health -= laser.GetDamage ();
			laser.Hit();

			if (health <= 0){
				Die ();
			}
		}
	}

	void Die(){
		AudioSource.PlayClipAtPoint (enemyDamage, transform.position, audioClipVolume);
		scoreKeeper.Score (scoreValue);
		Destroy (gameObject);
	}

}
