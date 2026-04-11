using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CandleLogic : MonoBehaviour, IPointerClickHandler
{

    public Sprite imgLit;
    public Sprite imgUnlit;
    public UnityEvent<CandleLogic> CandleClick;
    Image texture;

    void Awake()
    {
        texture = GetComponent<Image>();
    }

    public void SetLit(bool state)
    {
        texture.sprite = state ? imgLit : imgUnlit;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        CandleClick.Invoke(this);
    }
}
