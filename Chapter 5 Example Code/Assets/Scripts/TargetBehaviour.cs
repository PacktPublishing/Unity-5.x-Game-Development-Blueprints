using UnityEngine;
using System.Collections;

public class TargetBehaviour : MonoBehaviour {

    private bool beenHit = false;
    private Animator animator;


    private bool activated;
    private Vector3 originalPos;

    private GameObject parent;


    void Start()
    {
        parent = transform.parent.gameObject;
        animator = parent.GetComponent<Animator>();
        originalPos = parent.transform.position;
        //ShowTarget();
        GameController._instance.targets.Add(this);
    }

    public void ShowTarget()
    {
        if (!activated)
        {
            activated = true;
            beenHit = false;
            animator.Play("Idle");

            iTween.MoveBy(parent, iTween.Hash("y", 1.4,
                                              "easeType", "easeInOutExpo",
                                              "time", 0.5,
                                              "oncomplete", "OnShown",
                                              "oncompletetarget", gameObject));
        }

    }

    public IEnumerator HideTarget()
    {
        yield return new WaitForSeconds(.5f);

        // Move down to the original spot
        iTween.MoveBy(parent.gameObject, iTween.Hash("y", (originalPos.y - 
                                              parent.transform.position.y),
                                              "easeType", "easeOutQuad",
                                              "loopType", "none",
                                              "time", 0.5,
                                              "oncomplete", "OnHidden",
                                              "oncompletetarget", gameObject));
    }

    /// <summary>
	/// After the tween finishes, we now make sure we can be shown again.
	/// </summary>
	void OnHidden()
    {
        //Just to make sure the object's position resets
        parent.transform.position = originalPos;
        activated = false;
    }

    void OnShown()
    {
        StartCoroutine("MoveTarget");
    }

    public float moveSpeed = 1f;    // How fast to move in x axis
    public float frequency = 5f;    // Speed of sine movement
    public float magnitude = 0.1f;  // Size of sine movement

    IEnumerator MoveTarget()
    {
        var relativeEndPos = parent.transform.position;

        // Are we facing right or left?
        if (transform.eulerAngles == Vector3.zero)
        {
            // if we're going right positive
            relativeEndPos.x = 6;
        }
        else
        {
            // otherwise negative
            relativeEndPos.x = -6;
        }    

        var movementTime = Vector3.Distance(parent.transform.position, relativeEndPos) * moveSpeed;

        var pos = parent.transform.position;
        var time = 0f;

        while (time < movementTime)
        {
            time += Time.deltaTime;

            pos += parent.transform.right * Time.deltaTime * moveSpeed;
            parent.transform.position = pos + (parent.transform.up * 
                Mathf.Sin(Time.time * frequency) * magnitude);

            yield return new WaitForSeconds(0);
        }

        StartCoroutine(HideTarget());
    }



    /// <summary>
    /// Called whenever the player clicks on the object. Only works if you 
    /// have a collider
    /// </summary>
    void OnMouseDown()
    {
        // Is it valid to hit it
        if (!beenHit && activated)
        {
            GameController._instance.IncreaseScore();
            beenHit = true;
            animator.Play("Flip");

            StopAllCoroutines();

            StartCoroutine(HideTarget());
        }
    }
}
