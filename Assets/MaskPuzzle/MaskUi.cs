using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn.Unity;

public class MaskUi : MonoBehaviour
{

    public UI_Inventory inv;
    public GameObject background;
    public GameObject maskContainer;
    List<MaskItem> masks = new();

    DialogueRunner runner;

    void Awake()
    {
        runner = GameObject.FindWithTag("Yarn").GetComponent<DialogueRunner>();

        if (inv == null)
        {
            Debug.LogError("MaskUi is missing reference to inventory");
        }
        masks.AddRange(maskContainer.GetComponentsInChildren<MaskItem>());

        foreach (MaskItem mask in masks)
        {
            mask.MaskClick.AddListener(OnMoveMaskToInv);
        }
        Hide();
    }

    void OnMoveMaskToInv(MaskItem mask)
    {
        //if mask gets clicked, simply check if inventory does not already contain mask
        UI_ItemSlot slot0 = inv.slots[0];
        if (GameState.Instance.MaskSolved || slot0.item != null)
        {
            runner.StartDialogue("MaskShelfComplete");
            return;
        }
        slot0.item = mask.GetComponent<RectTransform>();
        mask.transform.SetParent(slot0.transform);
        mask.AnimateMoveToInv(slot0.Pos());
        masks.Remove(mask);
        Invoke(nameof(Hide), 1f);
    }

    public void Show()
    {
        if (!masks.Any())
        {
            //TODO show dialog "no more masks to pick"
        }
        UI_ItemSlot slot0 = inv.slots[0];
        if (slot0.item != null)
        {
            //TODO show dialog "i already picked a mask"
            return;
        }
        maskContainer.SetActive(true);
        background.SetActive(true);
    }

    public void Hide()
    {
        maskContainer.SetActive(false);
        background.SetActive(false);
    }
}
