using UnityEngine;
using System.Collections;

public class PlayerStart : MonoBehaviour
{
    //A reference to our player prefab
    public Transform player;

    //Have we spawned yet?
    public static bool spawned = false;

    public static PlayerStart _instance;

    // Use this for initialization
    void Start()
    {
        // If another PlayerStart exists, this will replace it
        if (_instance != null)
            Destroy(_instance.gameObject);

        _instance = this;

        // Have we spawned yet? If not, spawn the player
        if (!spawned)
        {
            SpawnPlayer();
            spawned = true;
        }
    }

    void SpawnPlayer()
    {
        Transform newObject = Instantiate(player,
                                    this.transform.position,
                                    Quaternion.identity) as Transform;

        newObject.name = "Player";
    }

}
