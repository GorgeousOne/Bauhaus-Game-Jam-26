using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using Yarn.Unity.Attributes;

public class MaskCultist : Cultist
{


    public UI_Inventory inv;
    InMemoryVariableStorage varStorage;

    protected override void Awake()
    {
        base.Awake();
        varStorage = runner.VariableStorage as InMemoryVariableStorage;
        runner.AddCommandHandler<bool>(
            "delete_mask",
            DeleteMask
        );
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
        Debug.Log("destroy", inv.slots[0].item.gameObject);
        Destroy(inv.slots[0].item.gameObject);
        inv.slots[0].item = null;
    }
}
