using UnityEngine;
using Yarn.Unity;

public class YarnTrigger : MonoBehaviour
{
    [SerializeField] private string dialogueName;
    DialogueRunner runner;

    public void Awake()
    {
        runner = GameObject.FindWithTag("Yarn").GetComponent<DialogueRunner>();
    }

    public void StartDialogue()
    {
        runner.StartDialogue(dialogueName);
    }
}
