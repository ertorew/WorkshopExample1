using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextScript : MonoBehaviour
{
    private InputAction LMB;
    private InputAction RMB;
    private InputAction Space;

    private bool Destroying = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LMB = InputSystem.actions.FindAction("Left");
        RMB = InputSystem.actions.FindAction("Right");
        Space = InputSystem.actions.FindAction("SpawnBall");
	}

    IEnumerator StartDestruction()
    {
        yield return new WaitForSeconds(3f);

        GameObject.Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Destroying == false && (Space.WasPressedThisFrame() || LMB.WasPressedThisFrame() || RMB.WasPressedThisFrame()))
        {
            Destroying = true;
            StartCoroutine(StartDestruction());
        }
    }
}
