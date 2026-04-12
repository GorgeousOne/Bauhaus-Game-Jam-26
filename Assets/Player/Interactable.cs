using UnityEngine;
using Yarn.Unity;

public abstract class Interactable : MonoBehaviour
{

    Color baseCol = new Color(1f, 1f, 1f);
    Color highCol = new Color(0.3f, 0.3f, 0.3f);

    public float radius = 1f;
    bool isHigh;

    public bool isEnabled = true;

    protected SpriteRenderer texture;

    protected DialogueRunner runner;
    protected InMemoryVariableStorage varStorage;


    protected virtual void Awake()
    {
        texture = GetComponent<SpriteRenderer>();
        if (texture == null)
        {
            Debug.LogWarning("Interactable "+ gameObject.name + " could not find sprite for highlighting :(");
        }

        runner = GameObject.FindWithTag("Yarn").GetComponent<DialogueRunner>();
        if (runner == null)
        {
            Debug.LogError(gameObject.name + " could not locate tag 'Yarn' in hierarchy ;_;");
        }
        varStorage = runner.VariableStorage as InMemoryVariableStorage;

    }

    public abstract void OnInteract();


    public void SetHighlighted(bool state) {
        isHigh = state;
        texture.color = isHigh ? highCol : baseCol;
    }

}
