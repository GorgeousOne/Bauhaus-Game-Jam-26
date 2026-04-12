using UnityEngine;
using Yarn.Unity;

public class Cultist : Interactable
{

    public string dialogueName;

    public override void OnInteract()
    {
        runner.StartDialogue(dialogueName);
    }

}
