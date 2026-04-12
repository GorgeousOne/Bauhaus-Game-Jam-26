using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
        goatPuzzle.SolveEvent.AddListener(UpdateSprite);
        if (goatPuzzle == null)
        {
            Debug.LogError("GoatPuzzleTrigger is missing reference to GoatPuzzle");
        }
    }

    void UpdateSprite()
    {
        var texture = GetComponent<SpriteRenderer>();
        texture.sprite = goatHealedSprite;
        texture.color = new Color(0.5489277f, 0.7735849f, 0.4539337f);
        isEnabled = false;
    }

    public override void SetHighlighted(bool state)
    {
        //keep goat color green when finisehd
        if (isEnabled) {
            base.SetHighlighted(state);
        }
    }
}