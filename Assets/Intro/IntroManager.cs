using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// yeah maybe move pause logic somewhere else
public class UiManager : MonoBehaviour
{

	public static UiManager Instance;

	public GameObject startScreen;

	[Header("Intro")]
	public GameObject introScreen;
	public Image introImage;
	public Sprite[] introFrames;
	private bool isInIntroActive;
	private int currentFrame;
	public float fadeDuration = 1f;
	private bool isFading;

	private CanvasGroup introCanvasGroup;

	void Awake()
	{
		Instance ??= this;
		
		introCanvasGroup = introScreen.GetComponent<CanvasGroup>();
		startScreen.SetActive(true);		
	}

	public void ShowIntro()
	{
		startScreen.SetActive(false);
		introScreen.SetActive(true);
		introCanvasGroup.alpha = 1f;
		currentFrame = 0;
		isFading = false;
		introImage.sprite = introFrames[0];
		isInIntroActive = true;
	}

	void Update()
	{
		// Intro: bei Klick nächster Frame
		if (isInIntroActive)
		{
			if (isFading)
			{
				introCanvasGroup.alpha -= Time.deltaTime / fadeDuration;
				if (introCanvasGroup.alpha <= 0)
				{
					EndIntro();
				}
			} else if (InputManager.I.ClickInput)
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
			}
		}
	}

	void EndIntro()
	{
		isInIntroActive = false;
		introScreen.SetActive(false);
		SceneManager.LoadScene("Level_1");
	}
}