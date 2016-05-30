using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameEndBehaviour : MonoBehaviour {

    /// <summary>
    /// Stops the player from quitting the game until a certain amount of 
    /// time has paseed.
    /// </summary>
    private bool canQuit = false;

    /// <summary>
    /// We've lost the game so display the Game Over text
    /// </summary>
    void Start()
    {
        // Start our timer coroutine
        StartCoroutine(DelayQuit());

        // We no longer need to spawn obstacles
        GameController controller = GameObject.Find("GameController").GetComponent<GameController>();
        controller.CancelInvoke();
    }

    /// <summary>
    /// Checks if the player presses space or clicks the mouse. If we can restart, we will
    /// </summary>
    void Update()
    {
        if ((Input.GetKeyUp("space") || Input.GetMouseButtonDown(0)) && canQuit)
        {
            // Will restart up to the same level as we are currently playing
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    /// <summary>
    /// Delays the player being able to restart instantly
    /// </summary>
    /// <returns>How long to wait before being called again</returns>
    IEnumerator DelayQuit()
    {
        // Give the player time before we end the game
        yield return new WaitForSeconds(.5f);

        //After .5 seconds have passed it will come here.
        canQuit = true;
    }
}
