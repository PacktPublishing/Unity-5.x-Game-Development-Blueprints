using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour 
{
	private Transform player;
	public float speed = 2.0f;
	
	// Use this for initialization
	void Start () 
	{
		// Save a reference to the player
		player = GameObject.Find("Player Ship").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 delta = player.position - transform.position;
		delta.Normalize();
		float moveSpeed = speed * Time.deltaTime;
		transform.position = transform.position + 
			(delta * moveSpeed);
	}
}
