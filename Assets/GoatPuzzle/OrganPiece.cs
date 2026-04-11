using UnityEngine;
using UnityEngine.EventSystems;

public class OrganPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("Grid cells this organ occupies, relative to its anchor cell. (0,0) is the anchor.")]
    public Vector2Int[] shape;

    [HideInInspector] public bool isPlaced = false;

    CanvasGroup canvasGroup;
    RectTransform rect;
    Canvas canvas;

    Vector2 startPos;
    public Vector2Int placedAnchor;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData e)
    {
        // If already placed, free those cells first
        if (isPlaced)
        {
            GoatPuzzle.Instance.RemovePiece(this, placedAnchor);
            isPlaced = false;
        }

        startPos = rect.anchoredPosition;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.7f;
    }

    public void OnDrag(PointerEventData e)
    {
        rect.anchoredPosition += e.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData e)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;

        if (GoatPuzzle.Instance.TryPlacePiece(this, e.position))
        {
            // Store where we were placed so we can remove later
            // (anchoredPosition was set by TryPlacePiece)
        }
        else
        {
            // Snap back to original position
            rect.anchoredPosition = startPos;
        }
    }
}