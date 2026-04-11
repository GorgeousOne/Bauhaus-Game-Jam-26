using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MaskUi : MonoBehaviour
{

    public UI_Inventory inv;
    public GameObject background;
    List<MaskItem> masks = new();


    void Awake()
    {
        if (inv == null)
        {
            Debug.LogError("MaskUi is missing reference to inventory");
        }
        masks.AddRange(GetComponentsInChildren<MaskItem>());

        foreach (MaskItem mask in masks)
        {
            mask.MaskClick.AddListener(OnMoveMaskToInv);
        }

    }

    void OnMoveMaskToInv(MaskItem mask)
    {
        //if mask gets clicked, simply check if inventory does not already contain mask
        UI_ItemSlot slot0 = inv.slots[0];
        if (slot0.item != null)
        {
            return;
        }
        slot0.item = mask.GetComponent<RectTransform>();
        mask.AnimateMoveToInv(slot0.Pos());
    }

    public void Show()
    {
        transform.gameObject.SetActive(true);
        background.SetActive(true);
    }

    public void Hide()
    {
        transform.gameObject.SetActive(false);
        background.SetActive(false);
    }
}
