using UnityEngine;

public class GoalBehaviour : MonoBehaviour
{
    ParticleSystem ps;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (ps.isPlaying)
        {
            print("You Win!");
        }
    }
}

