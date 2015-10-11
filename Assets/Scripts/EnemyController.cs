using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float health = 200;

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
