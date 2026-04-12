using UnityEngine;

public class CandlUiTrigger : Interactable
{
    public CandlePuzzle candlePuzzle;
    public Sprite candlesLitSprite;

    public override void OnInteract()
    {
        candlePuzzle.Show();
    }

    protected override void Awake()
    {
        base.Awake();
        if (candlePuzzle == null)
        {
            Debug.LogError(gameObject.name + " is missing reference to CandlePuzzle");
        }
    }
