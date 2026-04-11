using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    Interactable[] interacts;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        interacts = FindObjectsByType<Interactable>(FindObjectsSortMode.None);

    }

    void Update()
	{
		if (InputManager.I.InteractInput)
        {
            Vector3 playerPos = transform.position;
            Interactable closest = null;
            float minDist = float.PositiveInfinity;

            //check the distance between player and each interact
            foreach (Interactable interact in interacts)
            {
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
                closest.OnInteract();
            }
        }

	}
}



