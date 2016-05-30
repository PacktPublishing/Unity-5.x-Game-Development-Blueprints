using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{
    // A reference to our player's rigidbody component
    private Rigidbody rigidBody;

    // Force to apply when player jumps
    public Vector2 jumpForce = new Vector2(0, 450);

    // How fast we'll let the player move in the x axis
    public float maxSpeed = 3.0f;

    // A modifier to the force applied
    public float speed = 50.0f;

    // The force to apply that we will get for the player's movement
    private float xMove;

    // Set to true when the player can jump
    private bool shouldJump;


    void FixedUpdate()
    {
        // Move the player left and right
        Movement();

        // Sets the camera to center on the player's position.
        // Keeping the camera’s original depth
        Camera.main.transform.position = new Vector3(transform.position.x,
                                                     transform.position.y,
                                                     Camera.main.transform.position.z);
    }


    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        shouldJump = false;
        xMove = 0.0f;

        onGround = false;
        yPrevious = Mathf.Floor(transform.position.y);

    }

    void Update()
    {
        // Check if we are on the ground
        CheckGrounded();

        // Have the player jump if they press the jump button
        Jumping();
    }

    void CheckGrounded()
    {
        // Check if the player is hitting something from 
        // the center of the object (origin) to slightly below the 
        // bottom of it (distance)
        float distance = (GetComponent<CapsuleCollider>().height / 2 * 
                         this.transform.localScale.y) + .01f;
        Vector3 floorDirection = transform.TransformDirection(-Vector3.up);
        Vector3 origin = transform.position;

        if (!onGround)
        {
            // Check if there is something directly below us
            if (Physics.Raycast(origin, floorDirection, distance))
            {
                onGround = true;
            }
        }
        // If we are currently grounded, are we falling down or 
        // jumping ?
        else if ((Mathf.Floor(transform.position.y) != yPrevious))
        {
            onGround = false;
        }

        // Our current position will be our previous next frame
        yPrevious = Mathf.Floor(transform.position.y);
    }


    void Movement()
    {
        // Get the player's movement (-1 for left, 1 for right, 
        // 0 for none)
        xMove = Input.GetAxis("Horizontal");

        if (collidingWall && !onGround)
        {
            xMove = 0;
        }


        if (xMove != 0)
        {
            // Setting player horizontal movement
            float xSpeed = Mathf.Abs(xMove * rigidBody.velocity.x);

            if (xSpeed < maxSpeed)
            {
                Vector3 movementForce = new Vector3(1, 0, 0);
                movementForce *= xMove * speed;

                RaycastHit hit;
                if (!rigidBody.SweepTest(movementForce, out hit, 0.05f))
                {
                    rigidBody.AddForce(movementForce);
                }

            }

            // Check speed limit
            if (Mathf.Abs(rigidBody.velocity.x) > maxSpeed)
            {
                Vector2 newVelocity;

                newVelocity.x = Mathf.Sign(rigidBody.velocity.x) *
                                maxSpeed;
                newVelocity.y = rigidBody.velocity.y;

                rigidBody.velocity = newVelocity;
            }
        }
        else
        {
            // If we’re not moving, get slightly slower
            Vector2 newVelocity = rigidBody.velocity;

            // Reduce the current speed by 10%
            newVelocity.x *= 0.9f;
            rigidBody.velocity = newVelocity;
        }
    }

    void Jumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            shouldJump = true;
        }

        // If the player should jump
        if (shouldJump && onGround)
        {
            rigidBody.AddForce(jumpForce);
            shouldJump = false;
        }
    }

    void OnDrawGizmos()
    {
        if (rigidBody)
        {
            Debug.DrawLine(transform.position, transform.position +
               rigidBody.velocity, Color.red);
        }
    }

    private bool onGround;
    private float yPrevious;

    private bool collidingWall = false;

    // If we hit something and we're not grounded, it must be a wall or 
    // a ceiling. 
    void OnCollisionStay(Collision collision)
    {
        if (!onGround)
        {
            collidingWall = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        collidingWall = false;
    }

}
