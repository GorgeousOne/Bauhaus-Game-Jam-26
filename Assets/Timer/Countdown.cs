using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class Countdown : MonoBehaviour
{
    public float totalTime = 60f;
    public Slider heightMeter;
    public GameObject gameOverScreen;

    public bool isPaused = false;
    public float startTime;

    public float startZoom = 0.5f;
    public float endZoom = 10f;

    [Header("Screen Shake")]
    public CinemachineImpulseSource impulseSource;
    public float shakeStartTime = 30f;
    public float minShake = 0.1f;
    public float maxShake = 1.2f;
    public float shakeInterval = 0.15f;

    private float nextShakeTime;

    void Start()
    {
        startTime = Time.time;

        if (heightMeter == null)
        {
            Debug.LogError(gameObject.name + " missing reference of heightMeter ;_;");
            gameObject.SetActive(false);
        }

        if (impulseSource == null)
            impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        float delta = Time.time - startTime;
        float remaining = totalTime - delta;
        float t = delta / totalTime;

        heightMeter.value = 1 - t;

        if (t >= 1)
        {
            Debug.Log("BOOOOOM");
            gameOverScreen.SetActive(true);
            gameObject.SetActive(false);
            return;
        }

        float zoom = startZoom * Mathf.Pow(endZoom / startZoom, t);
        transform.localScale = Vector3.one * zoom;

        // Increasing screen shake under 30s
        if (remaining <= shakeStartTime && Time.time >= nextShakeTime)
        {
            float dangerT = 1f - (remaining / shakeStartTime);
            float shakeStrength = Mathf.Lerp(minShake, maxShake, dangerT);

            impulseSource.GenerateImpulse(shakeStrength);

            // shake gets faster as timer gets lower
            float currentInterval = Mathf.Lerp(shakeInterval, 0.03f, dangerT);
            nextShakeTime = Time.time + currentInterval;
        }
    }

    public void ReloadScene()
    {
        string gameScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(gameScene);
    }
}