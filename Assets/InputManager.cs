using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputManager : MonoBehaviour
{

	public static InputManager I;
	public static PlayerInput PlayerInput;

	public Vector2 MoveInput {get; private set;}
	public bool InteractInput {get; private set;}
	public bool MenuOpenInput {get; private set;}
	public bool MenuCloseInput {get; private set;}

	public bool ClickInput { get; private set; }
	private InputAction clickAction;
	private InputAction moveAction;
	private InputAction interactAction;
	private InputAction menuOpenAction;
	private InputAction menuCloseAction;

	void Awake()
	{
		// if null, set to this
		I ??= this;
		PlayerInput = GetComponent<PlayerInput>();
		moveAction = PlayerInput.actions["Move"];
		interactAction = PlayerInput.actions["Interact"];
		menuOpenAction = PlayerInput.actions["MenuOPEN"];
		menuCloseAction = PlayerInput.actions["MenuCLOSE"];
		clickAction = PlayerInput.actions["UI/Click"];
	}

	void Update()
	{
		MoveInput = moveAction.ReadValue<Vector2>();
		InteractInput = interactAction.WasPressedThisFrame();
		MenuOpenInput = menuOpenAction.WasPressedThisFrame();
		MenuCloseInput = menuCloseAction.WasPressedThisFrame();
		ClickInput = clickAction.WasPressedThisFrame();
	}
}
