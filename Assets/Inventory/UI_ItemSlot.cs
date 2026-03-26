using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IDropHandler
{

    // snap anything dropped on this slot
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            RectTransform itemRect = eventData.pointerDrag.GetComponent<RectTransform>();
            itemRect.position =  GetComponent<RectTransform>().position;
        }
    }
}