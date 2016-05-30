using UnityEngine;
using System.Collections; // IEnumerator
using UnityEngine.UI; // Image
using System.Collections.Generic; // List

public class PhoneBehaviour : MonoBehaviour
{

    public List<GameObject> phoneObjects;

    public Image cameraFlash;

    private bool cameraActive = false;

    private bool shotStarted = false;

    void Start()
    {
        SetCameraActive(false);
    }

    IEnumerator CameraFlash()
    {
        yield return StartCoroutine(Fade(0.0f, 0.8f, 0.2f,
    cameraFlash));
        yield return StartCoroutine(Fade(0.8f, 0.0f, 0.2f,
    cameraFlash));
        StopCoroutine("CameraFlash");
    }


    IEnumerator Fade(float start, float end, float length,
                  UnityEngine.UI.Image currentObject)
    {
        if (currentObject.color.a == start)
        {
            Color curColor;
            for (float i = 0.0f; i < 1.0f; 
                 i += Time.deltaTime * (1 / length))
            {
                // Cannot modify the color property directly, so we 
                // need to create a copy
                curColor = currentObject.color;

                // Do a linear interpolation of the value of the
                // transparency from the start 
                // value to the end value in equal increments
                curColor.a = Mathf.Lerp(start, end, i);

                // Then we assign the copy to the original object
                currentObject.color = curColor;

                yield return null;
            }
            curColor = currentObject.color;

            // ensure the fade is completely finished (because 
            // lerp doesn't always end on the exact value due to 
            // rounding errors)
            curColor.a = end;
            currentObject.color = curColor;
        }
    }


    // Update is called once per frame
    void Update () {
        // Are we holding down the right mouse button
        if ((Input.GetMouseButton(1) || (Input.GetAxis("360 Left Trigger") > 0)) && !cameraActive)
        {
            SetCameraActive(true);
        }
        else if(cameraActive && !(Input.GetMouseButton(1) || (Input.GetAxis("360 Left Trigger") > 0)))
        {
            SetCameraActive(false);
        }

        if (cameraActive && (Input.GetMouseButton(0) || (Input.GetAxis("360 Right Trigger") > 0)))
        {
            shotStarted = true;

            StartCoroutine(CameraFlash());

            GameObject[] enemyList = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemyList)
            {
                if (enemy.activeInHierarchy)
                {
                    EnemyBehaviour behaviour = enemy.GetComponent<EnemyBehaviour>();
                    behaviour.TakeDamage();
                }
            }

        }

        if (Input.GetAxis("360 Right Trigger") == 0)
        {
            shotStarted = false;
        }


    }

    void SetCameraActive(bool active)
    {
        cameraActive = active;

        foreach (var obj in phoneObjects)
        {
            obj.SetActive(active);
        }
    }
}
