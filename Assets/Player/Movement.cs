using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

	public float speed = 200f;

	private Rigidbody2D body;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
	{
		Vector3 vel = InputManager.I.MoveInput * speed * Time.fixedDeltaTime;
		body.linearVelocity = vel;
		// transform.position += vel;
	}
}