using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class OutroHandler : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject videorawimage;
    [SerializeField] GameObject blackBack;

	public GameObject outroScreen;
	public Image outroImage;
    public Sprite[] outroFrames;

    public float fadeDuration = 1f;
	private bool isFading;
    CanvasGroup outroCanvasGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool videoFinished = false;
    int currentFrame = 0;

    void Awake()
    {
        videoPlayer.loopPointReached += OnMovieFinished;
        outroImage.sprite = outroFrames[currentFrame];
        outroCanvasGroup = outroScreen.GetComponent<CanvasGroup>();
    }
   
    void Update()
	{
        if (!videoFinished)
        {
            return;
        }
		// outro: bei Klick nächster Frame
		if (isFading)
        {
            outroCanvasGroup.alpha -= Time.deltaTime / fadeDuration;
            if (outroCanvasGroup.alpha <= 0)
            {
                EndOutro();
            }
        } else if (InputManager.I.ClickInput)
		{
            currentFrame++;
            if (currentFrame < outroFrames.Length)
            {
                outroImage.sprite = outroFrames[currentFrame];
            }
            else
            {
                isFading = true;
            }
		}
	}

    void EndOutro()
    {
        SceneManager.LoadScene("Game");
    }

    //the action on finish
    void OnMovieFinished(VideoPlayer player)
    {
        videorawimage.SetActive(false);
        blackBack.SetActive(false);
        videoFinished = true;
    }
}
