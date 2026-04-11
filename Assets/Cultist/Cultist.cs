using UnityEngine;
using Yarn.Unity;

public class Cultist : Interactable
{

    public string dialogueName;

    protected DialogueRunner runner;

    protected virtual void Awake()
    {
        runner = GameObject.FindWithTag("Yarn").GetComponent<DialogueRunner>();

        if (runner == null)
        {
            Debug.LogError(gameObject.name + " could not locate tag 'Yarn' in hierarchy ;_;");
        }
    }

    public override void OnInteract()
    {
        runner.StartDialogue(dialogueName);
    }

}
