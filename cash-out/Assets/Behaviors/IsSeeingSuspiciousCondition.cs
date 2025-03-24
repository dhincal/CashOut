using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(
    name: "isSeeingSuspicious",
    story: "Is the [SusController] script returning true for seeingSuspicious",
    category: "Conditions",
    id: "2030116b23a0d0bbf9ef52005c2115db"
)]
public partial class IsSeeingSuspiciousCondition : Condition
{
    [SerializeReference]
    public BlackboardVariable<SuspicionController> SusController;

    public override bool IsTrue()
    {
        if (SusController.Value.suspicionLevel > 75)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void OnEnd() { }
}
