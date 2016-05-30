using UnityEngine;

public class OrbBehaviour : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GameController._instance.CollectedOrb();
        Destroy(this.gameObject);
    }
}
