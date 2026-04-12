using UnityEngine;
using UnityEngine.UI;

// yeah maybe move pause logic somewhere else
public class UiManager : MonoBehaviour
{

	public static UiManager Instance;

	public GameObject startScreen;
	public GameObject pauseScreen;

	[Header("Intro")]
	public GameObject introScreen;
	public Image introImage;
	public Sprite[] introFrames;
	private bool isGamePaused;
	private bool isInIntro;
	private int currentFrame;
	public float fadeDuration = 1f;
	private bool isFading;

	private CanvasGroup introCanvasGroup;

	void Awake()
	{
		Instance ??= this;
		startScreen.gameObject.SetActive(true);
		pauseScreen.gameObject.SetActive(false);
		introScreen.SetActive(false);
		introCanvasGroup = introScreen.GetComponent<CanvasGroup>();
	}

	public void HideStartScreen()
	{
		startScreen.gameObject.SetActive(false);
	}

	public void ShowMainMenu()
	{
		pauseScreen.gameObject.SetActive(false);
		startScreen.gameObject.SetActive(true);
	}

	public void ShowIntro()
	{
		startScreen.SetActive(false);
		introScreen.SetActive(true);
		introCanvasGroup.alpha = 1f;
		currentFrame = 0;
		isFading = false;
		introImage.sprite = introFrames[0];
		isInIntro = true;
	}

	void Update()
	{
		// Intro: bei Klick nächster Frame
		if (isInIntro)
		{
			if (isFading)
			{
				introCanvasGroup.alpha -= Time.deltaTime / fadeDuration;
				if (introCanvasGroup.alpha <= 0)
				{
					EndIntro();
				}
				return;
			}
			if (InputManager.I.ClickInput)
			{
				currentFrame++;
				if (currentFrame < introFrames.Length)
				{
					introImage.sprite = introFrames[currentFrame];
				}
				else
				{
					isFading = true;
				}
				return;
			}
		}
		if (GameManager.Instance.IsGameRunning && !isGamePaused && InputManager.I.MenuOpenInput)
		{
			PauseGame();
		}
		if (GameManager.Instance.IsGameRunning && isGamePaused && InputManager.I.MenuCloseInput)
		{
			ResumeGame();
		}
	}

	void EndIntro()
	{
		isInIntro = false;
		introScreen.SetActive(false);
		GameManager.Instance.StartGame();
	}

	// show pause ui & disable player inputs
	public void PauseGame()
	{
		isGamePaused = true;
		Time.timeScale = 0;
		pauseScreen.gameObject.SetActive(true);
		InputManager.PlayerInput.SwitchCurrentActionMap("UI");
	}

	public void ResumeGame()
	{
		isGamePaused = false;
		Time.timeScale = 1;
		pauseScreen.gameObject.SetActive(false);
		InputManager.PlayerInput.SwitchCurrentActionMap("Player");
	}
}