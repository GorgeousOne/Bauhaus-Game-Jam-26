using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PagramTrigger : Interactable {
    public Sprite pentagramLitSprite;
    public string dialogueName;

    public float rainbowSpeed = 0.5f;

    public override void OnInteract()
    {
        runner.StartDialogue(dialogueName);
    }

    protected override void Awake()
    {
        base.Awake();
        texture = GetComponent<SpriteRenderer>();
        isEnabled = false;
        runner.AddCommandHandler("PlayWinSequence", (Action)PlayWinSequence);
    }

    void Start()
    {
        GameState.Instance.AllPuzzlesSolvedEvent.AddListener(UpdateSprite);
    }

    void Update()
    {
        if (isEnabled)
        {
            float hue = Mathf.Repeat(Time.time * rainbowSpeed, 1f);
            texture.color = Color.HSVToRGB(hue, 1f, 1f);
        }
    }

    void UpdateSprite()
    {
        Debug.Log("omg omg omg");
        isEnabled = true;
        GetComponent<SpriteRenderer>().sprite = pentagramLitSprite;

    }

    public void PlayWinSequence()
    {
        Debug.Log("We did it guys, we solved racism");
        SceneManager.LoadSceneAsync("Outro");
    }

}
