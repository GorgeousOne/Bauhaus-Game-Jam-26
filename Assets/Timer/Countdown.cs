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

    public float startZoom = 0.5f;
    public float endZoom = 10f;


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
        float t = delta / totalTime;
        heightMeter.value = 1 - t;

        if (t >= 1)
        {
            Debug.Log("BOOOOOM");
            gameOverScreen.SetActive(true);
            gameObject.SetActive(false);
        }

        float easedT = t * t;
        easedT *= easedT;   // t^4

        float zoom = startZoom * Mathf.Pow(endZoom / startZoom, easedT);
        transform.localScale = Vector3.one * zoom;
   }

    public void ReloadScene()
    {
        string gameScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(gameScene);
    }
}
