using UnityEngine;

public class HideCollider : MonoBehaviour
{
    private void Awake()
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            if (sr != null)
                sr.enabled = false;
        }
}
