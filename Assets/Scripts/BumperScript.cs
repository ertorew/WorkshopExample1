using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BumperScript : MonoBehaviour
{
	private static WaitForSeconds _waitForSeconds0_07 = new WaitForSeconds(0.07f);
	public float BumpScale = 1.2f;
    public float BumpSpeed = 2f;
    public float BumpRecoverSpeed = 1.25f;

    private float startScale;
    private CircleCollider2D ccollider;
    private Coroutine coroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startScale = transform.localScale.x;
        ccollider = GetComponent<CircleCollider2D>();
    }

    IEnumerator HitScale()
    {
		transform.localScale = new Vector3(startScale, startScale, startScale);
        // ccollider.enabled = false;

        yield return _waitForSeconds0_07;

        float progress = 0f;
        while (progress < 1f)
        {
            yield return null;
            progress = Mathf.Min(progress += Time.deltaTime * BumpSpeed, 1f);
            float newScale = Mathf.LerpUnclamped(startScale, startScale * BumpScale, progress);
			transform.localScale = new Vector3(newScale, newScale, newScale);
            ccollider.radius = Mathf.LerpUnclamped(0.5f, 0.5f / BumpScale, progress);
		}

       // ccollider.enabled = true;

        while (progress > 0f)
        {
            yield return null;
            progress = Mathf.Max(progress -= Time.deltaTime * BumpRecoverSpeed, 0f);
            float newScale = Mathf.LerpUnclamped(startScale, startScale * BumpScale, progress);
			transform.localScale = new Vector3(newScale, newScale, newScale);
            ccollider.radius = Mathf.LerpUnclamped(0.5f, 0.5f / BumpScale, progress);
		}
		transform.localScale = new Vector3(startScale, startScale, startScale);
	}

   
	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(HitScale());
	}
}
