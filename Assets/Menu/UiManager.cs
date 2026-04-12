using UnityEngine;
using UnityEngine.SceneManagement;
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
		introCanvasGroup = introScreen.GetComponent<CanvasGroup>();
		startScreen.SetActive(false);
		introScreen.SetActive(true);
		introCanvasGroup.alpha = 1f;
		currentFrame = 0;
		isFading = false;
		introImage.sprite = introFrames[0];
		isInIntro = true;
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
	}

	void EndIntro()
	{
		isInIntro = false;
		introScreen.SetActive(false);
		SceneManager.LoadScene("Game");
	}

}