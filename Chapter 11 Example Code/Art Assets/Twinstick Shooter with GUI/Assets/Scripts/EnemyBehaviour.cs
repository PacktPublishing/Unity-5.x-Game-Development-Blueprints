using UnityEngine; // MonoBehaviour

public class EnemyBehaviour : MonoBehaviour 
{
	// How many times should enemy get hit before it dies
	public int health = 2;

	// When the enemy dies, we play an explosion
	public Transform explosion;

	// What sound to play when hit
	public AudioClip hitSound;

	
	void OnCollisionEnter2D(Collision2D theCollision)
	{
		// Uncomment this line to check for collision
		//Debug.Log("Hit"+ theCollision.gameObject.name);
		
		// this line looks for "Laser" in the name of 
		// the object we collided with.
		if(theCollision.gameObject.name.Contains("Laser"))
		{
			LaserBehaviour laser = theCollision.gameObject.GetComponent<LaserBehaviour>();
			health -= laser.damage;
			Destroy (theCollision.gameObject);

			// Plays a sound from this object's AudioSource
			GetComponent<AudioSource>().PlayOneShot(hitSound);
		}
		
		if (health <= 0) 
		{
			Destroy (this.gameObject);

			GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
			controller.KilledEnemy();
			controller.IncreaseScore(10);

			// Check if explosion was set
			if(explosion)
			{
				GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, this.transform.rotation)).gameObject;

				Destroy(exploder, 2.0f);
			}


		}
	}
}
