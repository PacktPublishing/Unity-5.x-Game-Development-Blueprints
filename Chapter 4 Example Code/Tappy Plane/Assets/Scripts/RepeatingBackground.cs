using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{

    [Tooltip("How fast should this object move")] public float scrollSpeed;

    /// <summary>
    /// How far to move until the image is offscreen.
    /// </summary>
    public const float ScrollWidth = 8; // bg width / pixels per unit

    /// <summary>
    /// Called at a fixed time frame, moves the objects and if they
    /// are off the screen do the appropriate thing
    /// </summary>
    private void FixedUpdate()
    {
        // Grab my current position
        Vector3 pos = transform.position;

        // Move the object a certain amount to the left (negative in the
        // x axis)
        pos.x -= scrollSpeed * Time.deltaTime * GameController.speedModifier;

        //Check if object is now fully offscreen
        if (transform.position.x < -ScrollWidth)
        {
            Offscreen(ref pos);
        }

        // If not destroyed, set our new position
        transform.position = pos;
    }

    /// <summary>
    /// Called whenever the object this is attached to goes completely 
    /// offscreen
    /// </summary>
    /// <param name="pos"></param>
    protected virtual void Offscreen(ref Vector3 pos)
    {
        //Moves the object to be to offscreen on the right side
        pos.x += 2 * ScrollWidth;
    }
    
}

