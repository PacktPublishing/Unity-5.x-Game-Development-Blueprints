using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Lists
//You must include these namespaces
//to use BinaryFormatter
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;



public class LevelEditor : MonoBehaviour {

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

    int xMin = 0;
    int xMax = 0;
    int yMin = 0;
    int yMax = 0;

    public List<Transform> tiles;

    GameObject dynamicParent;

    //The object we are currently looking to spawn
    private Transform toCreate;

    private string levelName = "Level1";

    void BuildLevel()
    {

        //Go through each element inside our level variable
        for (int yPos = 0; yPos < level.Length; yPos++)
        {
            for (int xPos = 0; xPos < (level[yPos]).Length; xPos++)
            {
                CreateBlock(level[yPos][xPos], xPos, level.Length - yPos);
            }
        }
    }

    public void CreateBlock(int value, int xPos, int yPos)
    {
        Transform toCreate = null;

        // We need to know the size of our level to save later
        if (xPos < xMin)
        {
            xMin = xPos;
        }
        if (xPos > xMax)
        {
            xMax = xPos;
        }

        if (yPos < yMin)
        {
            yMin = yPos;
        }
        if (yPos > yMax)
        {
            yMax = yPos;
        }

        //If value is set to 0, we don't want to spawn anything
        if (value != 0)
        {
            toCreate = tiles[value - 1];
        }

        if (toCreate != null)
        {
            //Create the object we want to create
            Transform newObject = Instantiate(toCreate, new Vector3(xPos, yPos, 0), Quaternion.identity) as Transform;

            //Give the new object the same name as ours
            newObject.name = toCreate.name;

            if (toCreate.name == "Goal")
            {
                // We want to have a reference to the particle system 
                // for later
                GameController._instance.GoalPS = newObject.gameObject.GetComponent<ParticleSystem>();

                // Move the particle system so it'll face up
                newObject.transform.Rotate(-90, 0, 0);
            }

            // Set the object's parent to the DynamicObjects
            // variable so it doesn't clutter our Hierarchy
            newObject.parent = dynamicParent.transform;
        }
    }


    public void Start()
    {
        // Get the DynamicObjects object so we can make it our
        // newly created objects' parent
        dynamicParent = GameObject.Find("Dynamic Objects");
        BuildLevel();

        enabled = false;
        toCreate = tiles[0];

        GameController._instance.UpdateOrbTotals(true);
    }


    void Update()
    {
        // Left click - Create object
        if (Input.GetMouseButton(0) && GUIUtility.hotControl == 0)
        {
            Vector3 mousePos = Input.mousePosition;

            //Set the position in the z axis to the opposite of the 
            // camera's so that the position is on the world so 
            // ScreenToWorldPoint will give us valid values.
            mousePos.z = Camera.main.transform.position.z * -1;

            Vector3 pos = Camera.main.ScreenToWorldPoint(mousePos);

            // Deal with the mouse being not exactly on a block
            int posX = Mathf.FloorToInt(pos.x + .5f);
            int posY = Mathf.FloorToInt(pos.y + .5f);

            Collider[] hitColliders = Physics.OverlapSphere(pos, 0.25f);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (toCreate.name != hitColliders[i].gameObject.name)
                {
                    DestroyImmediate(hitColliders[i].gameObject);
                }
                else
                {
                    // Already exists, no need to create another
                    return;
                }
                i++;
            }

            CreateBlock(tiles.IndexOf(toCreate) + 1, posX, posY);
            
            GameController._instance.UpdateOrbTotals();
        }

        // Right clicking - Delete object
        if (Input.GetMouseButton(1) && GUIUtility.hotControl == 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            Physics.Raycast(ray, out hit, 100);

            // If we hit something other than the player, we 
            // want to destroy it!
            if ((hit.collider != null) && (hit.collider.name != "Player"))
            {
                Destroy(hit.collider.gameObject);
            }

            GameController._instance.UpdateOrbTotals();
        }

    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 110, 20, 100, 800));
        foreach (Transform item in tiles)
        {
            if (GUILayout.Button(item.name))
            {
                toCreate = item;

            }
        }
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(10, 20, 100, 100));
        levelName = GUILayout.TextField(levelName);
        if (GUILayout.Button("Save"))
        {
            SaveLevel();
        }
        if (GUILayout.Button("Load"))
        {
            //If we have a file with the name typed in, load it!
            if (File.Exists(Application.persistentDataPath + "/" + levelName + ".lvl"))
            {
                LoadLevelFile(levelName);
                PlayerStart.spawned = false;

                // We need to wait one frame before UpdateOrbTotals 
                // will work (Orbs need to have Tag assigned)
                StartCoroutine(LoadedUpdate());
            }
            else
            {
                levelName = "Error";
            }
        }
        if (GUILayout.Button("Quit"))
        {
            enabled = false;
        }
        GUILayout.EndArea();

    }

    void SaveLevel()
    {
        List<string> newLevel = new List<string>();

        for (int i = yMin; i <= yMax; i++)
        {
            string newRow = "";
            for (int j = xMin; j <= xMax; j++)
            {
                Vector3 pos = new Vector3(j, i, 0);
                Ray ray = Camera.main.ScreenPointToRay(pos);
                RaycastHit hit = new RaycastHit();

                Physics.Raycast(ray, out hit, 100);

                // Will check if there is something hitting us within 
                // a distance of .1
                Collider[] hitColliders = Physics.OverlapSphere(pos, 0.1f);

                if (hitColliders.Length > 0)
                {
                    // Do we have a tile with the same name as this object?
                    for (int k = 0; k < tiles.Count; k++)
                    {
                        // If so, let's save that to the string
                        if (tiles[k].name == hitColliders[0].gameObject.name)
                        {
                            newRow += (k + 1).ToString() + ",";
                        }
                    }
                }
                else
                {
                    newRow += "0,";
                }
            }
            newRow += "\n";
            newLevel.Add(newRow);
        }
        // Reverse the rows to make the final version rightside up
        newLevel.Reverse();

        string levelComplete = "";

        foreach (string level in newLevel)
        {
            levelComplete += level;
        }
        // This is the data we're going to be saving
        print(levelComplete);

        //Save to a file
        BinaryFormatter bFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + levelName + ".lvl");
        bFormatter.Serialize(file, levelComplete);
        file.Close();

    }

    private void LoadLevelFile(string level)
    {
        // Destroy everything inside our currently level that's created 
        // dynamically
        foreach (Transform child in dynamicParent.transform)
        {
            Destroy(child.gameObject);
        }

        BinaryFormatter bFormatter = new BinaryFormatter();
        FileStream file = File.OpenRead(Application.persistentDataPath + "/" + level + ".lvl");

        // Convert the file from a byte array into a string
        string levelData = bFormatter.Deserialize(file) as string;

        // We're done working with the file so we can close it
        file.Close();

        LoadLevelFromString(levelData);

        // Set our text object to the current level.
        levelName = level;
    }

    public void LoadLevelFromString(string content)
    {
        // Split our string by the new lines (enter)
        List<string> lines = new List<string>(content.Split('\n'));
        // Place each block in order in the correct x and y position
        for (int i = 0; i < lines.Count; i++)
        {
            string[] blockIDs = lines[i].Split(',');
            for (int j = 0; j < blockIDs.Length - 1; j++)
            {
                CreateBlock(int.Parse(blockIDs[j]), j, lines.Count - i);
            }
        }
    }

    IEnumerator LoadedUpdate()
    {
        //returning 0 will make it wait 1 frame
        yield return 0;

        GameController._instance.UpdateOrbTotals(true);
    }



}
