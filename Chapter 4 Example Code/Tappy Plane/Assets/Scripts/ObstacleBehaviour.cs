using UnityEngine;

public class ObstacleBehaviour : RepeatingBackground
{

    protected override void Offscreen(ref Vector3 pos)
    {
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameController.Score++;
    }
}
