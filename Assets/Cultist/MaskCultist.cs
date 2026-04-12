using UnityEngine;
using Yarn.Unity;

public class MaskCultist : Cultist
{


    public UI_Inventory inv;

    protected override void Awake()
    {
        base.Awake();
        runner.AddCommandHandler<bool>(
            "delete_mask",
            DeleteMask
        );

        if (inv == null)
        {
            Debug.LogWarning(gameObject.name + " missing inventory reference :(");
        }
    }

    public override void OnInteract()
    {
        UI_ItemSlot slot0 = inv.slots[0];
        if (slot0.item != null)
        {
            MaskItem mask = slot0.item.gameObject.GetComponent<MaskItem>();
            varStorage.SetValue("$Mask", mask.IsCorrectMask ? "right" : "wrong");
        } else
        {
            varStorage.SetValue("$Mask", "empty");
        }
        runner.StartDialogue(dialogueName);
    }

    public void DeleteMask(bool success)
    {
        if (inv.slots[0].item.gameObject.GetComponent<MaskItem>().IsCorrectMask)
        {
            GameState.Instance.solveMask();
        }
        Destroy(inv.slots[0].item.gameObject);
        inv.slots[0].item = null;


    }
}
