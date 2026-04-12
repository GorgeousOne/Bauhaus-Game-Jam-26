using UnityEngine;

public class MaskShelf : Interactable
{

    public MaskUi maskUi;
    [SerializeField] private AudioSource audioSource;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void OnInteract()
    {
        maskUi.Show();
        audioSource.Play();

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
