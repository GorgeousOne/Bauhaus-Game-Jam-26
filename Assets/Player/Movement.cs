using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

	public float speed = 5f;

	void FixedUpdate()
	{
		Vector3 vel = InputManager.I.MoveInput * speed * Time.fixedDeltaTime;
		transform.position += vel;
	}
}