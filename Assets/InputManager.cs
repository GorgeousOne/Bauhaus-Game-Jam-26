using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;

public class InputManager : MonoBehaviour
{

	public static InputManager I;
	public PlayerInput PlayerInput;

	public Vector2 MoveInput {get; private set;}
	public bool InteractInput {get; private set;}
	public bool MenuOpenInput {get; private set;}
	public bool MenuCloseInput {get; private set;}
	public bool IsUiOpen {get; set;}

	public bool ClickInput { get; private set; }
	private InputAction clickAction;
	private InputAction moveAction;
	private InputAction interactAction;
	private InputAction menuOpenAction;
	private InputAction menuCloseAction;

	private DialogueRunner runner;

	void Awake()
	{
		I = this;
		PlayerInput = GetComponent<PlayerInput>();
		moveAction = PlayerInput.actions["Move"];
		interactAction = PlayerInput.actions["Interact"];
		menuOpenAction = PlayerInput.actions["MenuOPEN"];
		menuCloseAction = PlayerInput.actions["MenuCLOSE"];
		clickAction = PlayerInput.actions["UI/Click"];

		runner = GameObject.FindWithTag("Yarn")?.GetComponent<DialogueRunner>();
	}

	void Update()
	{
		MoveInput = IsDialogue() || IsUiOpen ? Vector2.zero : moveAction.ReadValue<Vector2>();
		InteractInput = !IsUiOpen && interactAction.WasPressedThisFrame();
		MenuOpenInput = !IsUiOpen && menuOpenAction.WasPressedThisFrame();
		MenuCloseInput = IsUiOpen && menuCloseAction.WasPressedThisFrame();
		ClickInput = clickAction.WasPressedThisFrame();
	}

	bool IsDialogue()
	{
		return runner?.IsDialogueRunning ?? false;
	}
}
