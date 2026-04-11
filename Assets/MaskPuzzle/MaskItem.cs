using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MaskItem : MonoBehaviour, IPointerClickHandler
{

    public bool IsCorrectMask;

    public UnityEvent<MaskItem> MaskClick;

    public float animTime = 0.5f;
    float animStart = -1;
    float targetScale = 0.5f;

    RectTransform rect;
    Vector2 origin;
    Vector2 targetPos;


    void Awake()
    {
        rect = GetComponent<RectTransform>();
        origin = rect.position;
    }

    void Update()
    {
        //idk fuck this, trying to animate the movement of mask from original place to inventory
        if (animStart > 0) {
            float delta = (Time.time - animStart) / animTime;

            if (delta < 1)
            {
                float deltaQuad = easeOutQuad(delta);
                rect.position = Vector3.Lerp(origin, targetPos, deltaQuad);
            } else
            {
                rect.position = targetPos;
                animStart = -1;
                rect.localScale = new Vector3(targetScale, targetScale, 1f);
            }
        }
    }


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(name + " Game Object Clicked!");
        MaskClick.Invoke(this);
    }

    public void AnimateMoveToInv(Vector2 target)
    {
        targetPos = target;
        animStart = Time.time;
    }

    //https://easings.net/
    float easeOutQuad(float t){
        return 1 - (1 - t) * (1 - t);
    }
}
