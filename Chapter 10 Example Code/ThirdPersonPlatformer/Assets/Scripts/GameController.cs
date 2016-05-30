using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Text

public class GameController : MonoBehaviour {

    public static GameController _instance;
    private int orbsCollected;
    private int orbsTotal;





    public Text scoreText;

    /*
    [Header("Object References")]
    public Transform wall;
    public Transform player;
    public Transform orb;
    public Transform goal;
    */

    private ParticleSystem goalPS;

    public ParticleSystem GoalPS
    {
        get
        {
            return goalPS;
        }

        set
        {
            goalPS = value;
        }
    }


    /*
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
    */
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
        
        //BuildLevel();

        // After the level is created, let's see how many orbs there are

        GameObject[] orbs;
        orbs = GameObject.FindGameObjectsWithTag("Orb");

        orbsCollected = 0;
        orbsTotal = orbs.Length;

        scoreText.text = "Orbs: " + orbsCollected + "/" + orbsTotal;
    }

    void Update()
    {
        if (Input.GetKeyDown("f2"))
        {
            this.gameObject.GetComponent<LevelEditor>().enabled = true;
        }
    }

    public void UpdateOrbTotals(bool reset = false)
    {
        if (reset)
            orbsCollected = 0;

        GameObject[] orbs;
        orbs = GameObject.FindGameObjectsWithTag("Orb");

        orbsTotal = orbs.Length;

        scoreText.text = "Orbs: " + orbsCollected + "/" + orbsTotal;
    }


}
