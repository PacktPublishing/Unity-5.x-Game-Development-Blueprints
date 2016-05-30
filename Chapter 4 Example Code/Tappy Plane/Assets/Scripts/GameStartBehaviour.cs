using UnityEngine;

public class GameStartBehaviour : MonoBehaviour {

    /// <summary>
    /// a reference to the player object.
    /// </summary>
    private GameObject player;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Plane");
        player.GetComponent<Rigidbody2D>().isKinematic = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Start the game
        if ((Input.GetKeyUp("space") || Input.GetMouseButtonDown(0)))
        {
            // After 1 second, spawn obstacles every 1.5 seconds
            GameController controller = GetComponent<GameController>();
            controller.InvokeRepeating("CreateObstacle", 1f, 1.5f);

            // We want the plane to start falling now
            player.GetComponent<Rigidbody2D>().isKinematic = false;

            Destroy(this);
        }
    }
}
