using UnityEngine;

public class GoatPuzzleTrigger : Interactable
{
    public GoatPuzzle goatPuzzle;
    public Sprite goatHealedSprite;
    public string dialogueName;

    public override void OnInteract()
    {
        goatPuzzle.Show();
    }

    protected override void Awake()
    {
        base.Awake();
        if (goatPuzzle == null)
        {
            Debug.LogError("GoatPuzzleTrigger is missing reference to GoatPuzzle");
        }
    }
}