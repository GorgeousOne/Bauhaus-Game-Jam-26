using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IDropHandler
{

    public RectTransform item;

    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    // snap anything dropped on this slot
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            RectTransform itemRect = eventData.pointerDrag.GetComponent<RectTransform>();
            itemRect.position = rect.position;
            item = itemRect;
        }
    }


    public Vector3 Pos()
    {
        return rect.position;
    }
}