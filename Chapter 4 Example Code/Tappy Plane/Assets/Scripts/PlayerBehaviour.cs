using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour
{

    [Tooltip("The force which is added when the player jumps")]
    public Vector2 jumpForce = new Vector2(0, 300);

    /// <summary>
    /// If we've been hit, we can no longer jump
    /// </summary>
    private bool beenHit;

    private Rigidbody2D rigidbody2D;

    // Use this for initialization
    void Start ()
    {
        beenHit = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	// Will be called after Update
	void LateUpdate ()
    {
        // Check if we should jump as long as we haven't been hit yet
        if ((Input.GetKeyUp("space") || Input.GetMouseButtonDown(0)) && !beenHit)
        {
            // Reset velocity and then jump up
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.AddForce(jumpForce);
        }
    }

    /// <summary>
    /// If we collide with any of the polygon colliders then we crash
    /// </summary>
    /// <param name="other">Who we collided with</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        // We have now been hit
        beenHit = true;
        GameController.speedModifier = 0;

        // The animation should no longer play, so we can set the speed 
        //to 0 or destroy it
        GetComponent<Animator>().speed = 0.0f;

        // Finally, create a GameEndBehaviour so we can restart
        if (!gameObject.GetComponent<GameEndBehaviour>())
        {
            gameObject.AddComponent<GameEndBehaviour>();
        }
    }
}
