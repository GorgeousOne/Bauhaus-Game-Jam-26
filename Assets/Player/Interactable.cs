using UnityEngine;

public class Interactable : MonoBehaviour
{
    SpriteRenderer sr;

    Color originalColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ColorUtility.TryParseHtmlString("#D99037", out originalColor);
    }


    bool playerInRange;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            sr.color = Color.yellow;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            ColorUtility.TryParseHtmlString("#D99037", out originalColor);
            sr.color = originalColor;
        }
            
    }

    void Update()
    {
        if (playerInRange && UnityEngine.InputSystem.Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }

    void Interact()
    {
        Debug.Log("Interaktion mit " + gameObject.name);
    }
}
