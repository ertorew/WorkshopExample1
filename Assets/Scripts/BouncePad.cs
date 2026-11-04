using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float BounceStrength = 1.5f;
	public Vector2 Normal;

	protected struct RecentCollision
	{
		public Collider2D collider;
		public float moment;

		public RecentCollision(Collider2D Collider, float Moment)
		{
			collider = Collider;
			moment = Moment;
		}
	}

	private readonly System.Collections.Generic.List<RecentCollision> RecentColliders = new(32);

	private void Start()
	{
		Normal = Vector2.Normalize(Normal);
	}

	private void Update()
	{
		for (int i = 0; i < RecentColliders.Count; i++)
		{
			RecentCollision collision = RecentColliders[i];
			if (Time.time - collision.moment > 1.25f)
			{
				RecentColliders.Remove(collision);
				i--;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (RecentColliders.Exists(b => b.collider == collision.collider)) return;

		RecentColliders.Add(new RecentCollision(collision.collider, Time.time));

		Vector2 direction = new Vector2(collision.rigidbody.position.x - transform.position.x, collision.rigidbody.position.y - transform.position.y).normalized;

		Vector2 normalizedNormal = Normal;

		float dotProduct = Vector2.Dot(collision.rigidbody.linearVelocity, normalizedNormal);
		collision.rigidbody.linearVelocity = (collision.rigidbody.linearVelocity - 2 * dotProduct * normalizedNormal) * BounceStrength;
	}
}
