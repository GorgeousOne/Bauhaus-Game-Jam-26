using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    Color baseCol = new Color(1f, 1f, 1f);
    Color highCol = new Color(0.3f, 0.3f, 0.3f);

    public float radius = 1f;
    public bool isHigh;
    protected SpriteRenderer texture;

    protected virtual void Awake()
    {
        texture = GetComponent<SpriteRenderer>();

        if (texture == null)
        {
            Debug.LogWarning("Interactable "+ gameObject.name + " could not find sprite for highlighting :(");
        }
    }

    public abstract void OnInteract();


    public void SetHighlighted(bool state) {
        isHigh = state;
        texture.color = isHigh ? highCol : baseCol;
    }

}
