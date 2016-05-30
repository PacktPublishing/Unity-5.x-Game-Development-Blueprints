using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    public enum State
    {
        Idle,
        Follow,
        Die,
    }

    // The current state the player is in
    public State state;

    // The object the enemy wants to follow
    public Transform target;

    // How fast should the enemy move?
    public float moveSpeed = 3.0f;
    public float rotateSpeed = 3.0f;

    // How close should the enemy be before they follow?
    public float followRange = 10.0f;

    // How far should the target be before the enemy gives up 
    // following? 
    // Note: Needs to be >= followRange
    public float idleRange = 10.0f;

    public float health = 100.0f;
    private float currentHealth;


    IEnumerator IdleState()
    {
        //OnEnter
        Debug.Log("Idle: Enter");
        while (state == State.Idle)
        {
            //OnUpdate
            if (GetDistance() < followRange)
            {
                state = State.Follow;
            }

            yield return 0;
        }
        //OnEnd
        Debug.Log("Idle: Exit");
        GoToNextState();
    }

    IEnumerator FollowState()
    {
        Debug.Log("Follow: Enter");
        while (state == State.Follow)
        {
            transform.position =
      Vector3.MoveTowards(transform.position,
                          target.position,
                          Time.deltaTime * moveSpeed);

            RotateTowardsTarget();

            if (GetDistance() > idleRange)
            {
                state = State.Idle;
            }

            yield return 0;
        }
        Debug.Log("Follow: Exit");
        GoToNextState();
    }

    IEnumerator DieState()
    {
        Debug.Log("Die: Enter");

        Destroy(this.gameObject);
        yield return 0;
    }

    public float GetDistance()
    {
        return (transform.position -
    target.transform.position).magnitude;
    }

    private void RotateTowardsTarget()
    {
        transform.rotation =
    Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(target.position - 
                                        transform.position),
                               rotateSpeed * Time.deltaTime);
    }

    void GoToNextState()
    {
        // Find out the name of the function we want to call
        string methodName = state.ToString() + "State";

        // Searches this class for a function with the name of 
        // state + State (for example: idleState)
        System.Reflection.MethodInfo info =
        GetType().GetMethod(methodName,
        System.Reflection.BindingFlags.NonPublic |
        System.Reflection.BindingFlags.Instance);
        StartCoroutine((IEnumerator)info.Invoke(this, null));
    }


    // Use this for initialization
    void Start () {
        GoToNextState();
        currentHealth = health;
    }

    public void TakeDamage()
    {
        // The closer I am, the more damage I do
        float damageToDo = 100.0f - (GetDistance() * 5);

        if (damageToDo < 0)
            damageToDo = 0;
        if (damageToDo > health)
            damageToDo = health;

        currentHealth -= damageToDo;

        if (currentHealth <= 0)
        {
            state = State.Die;
        }
        else
        {
            // If we're not dead, now that we took a picture the 
            // enemy 
            // knows where we are
            followRange = Mathf.Max(GetDistance(), followRange);
            state = State.Follow;
        }

        print("Ow! - Current Health: " +
    currentHealth.ToString());

    }


}
