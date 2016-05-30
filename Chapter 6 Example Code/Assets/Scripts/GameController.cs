using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Text

public class GameController : MonoBehaviour {

    public static GameController _instance;
    private int orbsCollected;
    private int orbsTotal;


    private int[][] level = new int[][]
{
    new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 3, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3, 3, 0, 0, 0, 4, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    new int[]{1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 3, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 3, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
    new int[]{1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
    new int[]{1, 0, 2, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1},
    new int[]{1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
   };


    public Text scoreText;

    [Header("Object References")]
    public Transform wall;
    public Transform player;
    public Transform orb;
    public Transform goal;
    private ParticleSystem goalPS;


    void BuildLevel()
    {
        // Get the DynamicObjects object that we created already in the 
        // scene so we can make it our newly created objects’ parent
        GameObject dynamicParent = GameObject.Find("Dynamic Objects");

        //Go through each element inside our level variable
        for (int yPos = 0; yPos < level.Length; yPos++)
        {
            for (int xPos = 0; xPos < (level[yPos]).Length; xPos++)
            {
                Transform toCreate = null;
                switch (level[yPos][xPos])
                {
                    case 0:
                        //Do nothing because we don't want anything in this spot.
                        break;

                    case 1:
                        toCreate = wall;
                        break;

                    case 2:
                        toCreate = player;
                        break;

                    case 3:
                        toCreate = orb;
                        break;
                    case 4:
                        toCreate = goal;
                        break;

                    default:
                        print("Invalid number: " +
                           (level[yPos][xPos]).ToString());
                        break;
                }

                if (toCreate != null)
                {
                    Transform newObject = Instantiate(toCreate,
                                new Vector3(xPos, (level.Length - yPos),
                                0),
                                toCreate.rotation) as Transform;

                    if (toCreate == goal)
                    {
                        goalPS = newObject.gameObject.GetComponent<ParticleSystem>();
                    }


                    // Set the object's parent to the DynamicObjects
                    // variable so it doesn't clutter our Hierachy
                    newObject.parent = dynamicParent.transform;
                }

            }
        }
    }

    void Awake()
    {
        _instance = this;
    }

    public void CollectedOrb()
    {
        orbsCollected++;
        scoreText.text = "Orbs: " + orbsCollected + "/" + orbsTotal;

        if (orbsCollected >= orbsTotal)
        {
            goalPS.Play();
        }

    }


    // Use this for initialization
    void Start ()
    {
        
        BuildLevel();

        // After the level is created, let's see how many orbs there are

        GameObject[] orbs;
        orbs = GameObject.FindGameObjectsWithTag("Orb");

        orbsCollected = 0;
        orbsTotal = orbs.Length;

        scoreText.text = "Orbs: " + orbsCollected + "/" + orbsTotal;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
