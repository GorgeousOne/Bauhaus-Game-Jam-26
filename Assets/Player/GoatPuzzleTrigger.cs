using UnityEngine;

public class GoatPuzzleTrigger : Interactable
{
    public GoatPuzzle goatPuzzle;

    public override void OnInteract()
    {
        goatPuzzle.Show();
    }

    void Awake()
    {
        if (goatPuzzle == null)
        {
            Debug.LogError("GoatPuzzleTrigger is missing reference to GoatPuzzle");
        }
    }
}