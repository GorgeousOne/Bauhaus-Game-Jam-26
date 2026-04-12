using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    Interactable[] interacts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        interacts = FindObjectsByType<Interactable>(FindObjectsSortMode.None);

    }

    Interactable last;

    void Update()
	{
        if (last != null)
        {
            last.SetHighlighted(false);
            last = null;
        }
        Vector3 playerPos = transform.position;
        Interactable closest = null;
        float minDist = float.PositiveInfinity;

        //check the distance between player and each interact
        foreach (Interactable interact in interacts)
        {
            if (!interact.isEnabled)
            {
                continue;
            }
            //calculate the distance to the interact
            float dist = (interact.transform.position - playerPos).magnitude;

            //save the smallest distance to any interact
            if (dist < interact.radius && dist < minDist)
            {
                minDist = dist;
                closest = interact;
            }
        }
        if (closest != null)
        {
            if (InputManager.I.InteractInput)
            {
                closest.OnInteract();
            }
            if (last != closest) {
                closest.SetHighlighted(true);
                last = closest;
            }
        }
	}
}



