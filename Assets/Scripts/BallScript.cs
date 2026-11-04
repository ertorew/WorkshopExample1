using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallScript : MonoBehaviour
{
    private Rigidbody2D rigid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position.y < -6f)
        {
            //transform.position = new Vector3(-0.949999988f, 0f, 0f);
            rigid.linearVelocity = Vector2.zero;
            rigid.angularVelocity = 0f;

            Transform targetLocation = GameSystem.instance.BallSpawnLocations[Random.Range(0, GameSystem.instance.BallSpawnLocations.Length)];
            rigid.position = targetLocation.position + new Vector3(Random.Range(-0.2f, 0.2f),0f,0f);
		}

	}
}
