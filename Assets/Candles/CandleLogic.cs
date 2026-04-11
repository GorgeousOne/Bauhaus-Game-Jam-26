using UnityEngine;
using UnityEngine.Events;

public class CandleLogic : MonoBehaviour
{

    public Sprite imgLit;
    public Sprite imgUnlit;
    public UnityEvent<CandleLogic> CandleClick;
    SpriteRenderer sprite;


    public void SetLit(bool state)
    {
        sprite.sprite = state ? imgLit : imgUnlit;
    }
}
