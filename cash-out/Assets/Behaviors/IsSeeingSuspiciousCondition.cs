using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable, Unity.Properties.GeneratePropertyBag]
[Condition(
    name: "isSeeingSuspicious",
    story: "Is the [FOV] script returning true for seeingSuspicious",
    category: "Conditions",
    id: "2030116b23a0d0bbf9ef52005c2115db"
)]
public partial class IsSeeingSuspiciousCondition : Condition
{
    [SerializeReference]
    public BlackboardVariable<FieldOfView> FOV;

    public override bool IsTrue()
    {
        return FOV.Value.seeSuspicous;
    }

    public override void OnEnd() { }
}
