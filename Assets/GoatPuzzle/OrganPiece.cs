using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OrganPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Tooltip("Grid cells this organ occupies, relative to its anchor cell. (0,0) is the anchor.")]
    public Vector2Int[] shape;

    [HideInInspector] public bool isPlaced = false;

    CanvasGroup canvasGroup;
    RectTransform rect;
    Canvas canvas;

    Vector2 grabOffset;
    public Vector2Int placedAnchor;

    public UnityEvent<OrganPiece, Vector2, Camera> DragEnd;

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

        // RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, e.position, e.pressEventCamera, out Vector2 localClickPos);

        Vector2 rectScreenPoint = RectTransformUtility.WorldToScreenPoint(
            e.pressEventCamera,
            rect.position
        );

        grabOffset = e.position - rectScreenPoint;
        Debug.Log("grab: rect" +rectScreenPoint + " offset " + grabOffset + " total " + (e.position - grabOffset));
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
        Vector2 rectScreenPoint = RectTransformUtility.WorldToScreenPoint(
            e.pressEventCamera,
            rect.position
        );
        Debug.Log("drop: rect" + rectScreenPoint + " offset " + grabOffset + " total " + (e.position - grabOffset));
        //DragEnd.Invoke(this, e.position - grabOffset, e.pressEventCamera);
        DragEnd.Invoke(this, e.position - grabOffset, e.pressEventCamera);
    }

    public void move(Vector2 delta)
    {
        rect.anchoredPosition += delta;
    }
}