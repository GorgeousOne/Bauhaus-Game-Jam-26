using System;
using UnityEngine;

public class CandlUiTrigger : Interactable
{
    public CandlePuzzle candlePuzzle;
    public Sprite candlesLitSprite;
    public string dialogueName;

    public override void OnInteract()
    {
        runner.StartDialogue(dialogueName);
    }

    protected override void Awake()
    {
        base.Awake();
        if (candlePuzzle == null)
        {
            Debug.LogError(gameObject.name + " is missing reference to CandlePuzzle");
        }
        candlePuzzle.FinishEvent.AddListener(UpdateSprite);
        runner.AddCommandHandler("StartCandleGame", (Action)StartCandleGame);
    }

    void UpdateSprite()
    {
        isEnabled = false;
        GetComponent<SpriteRenderer>().sprite = candlesLitSprite;
    }

    public void StartCandleGame()
    {
        candlePuzzle.Show();
    }
}
