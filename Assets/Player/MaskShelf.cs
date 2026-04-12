using UnityEngine;

public class MaskShelf : Interactable
{

    public MaskUi maskUi;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnInteract()
    {
        maskUi.Show();
    }

    protected override void Awake()
    {
        base.Awake();
        if (maskUi == null)
        {
            Debug.LogError("MaskShelf is missing reference to MaskUi");
        }
    }
}
