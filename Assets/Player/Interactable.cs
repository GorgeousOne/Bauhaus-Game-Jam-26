using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public float radius = 1f;

    public abstract void OnInteract();

}
