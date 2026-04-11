using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public float totalTime = 10;
    public Slider heightMeter;
    public GameObject gameOverScreen;

    public bool isPaused = false;
    public float startTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = Time.time;
        if (heightMeter == null)
        {
            Debug.LogError(gameObject.name + " missing reference of heightMeter ;_;");
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float delta = Time.time - startTime;
        float progress = delta / totalTime;
        heightMeter.value = 1 - progress;

        if (progress >= 1)
        {
            Debug.Log("BOOOOOM");
            gameOverScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void ReloadScene()
    {
        string gameScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(gameScene);
    }
}
