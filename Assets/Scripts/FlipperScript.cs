using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class FlipperScript : MonoBehaviour
{
    public bool isLeft = false;
    public float FlipRotation = 30f;
    public float FlipSpeed = 23f;

	private InputAction action;
	private float startRotation;
    private float flipProgress;
    private Rigidbody2D rigid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        startRotation = transform.localEulerAngles.z;
		action = InputSystem.actions.FindAction(isLeft ? "Left" : "Right");

	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (action.IsPressed())
        {
            flipProgress = Mathf.Clamp01(flipProgress += Time.fixedDeltaTime * FlipSpeed);
        }
        else
        {
            flipProgress = Mathf.Clamp01(flipProgress -= Time.fixedDeltaTime * FlipSpeed);
        }

        rigid.MoveRotation(startRotation + FlipRotation * flipProgress);
    }
}
