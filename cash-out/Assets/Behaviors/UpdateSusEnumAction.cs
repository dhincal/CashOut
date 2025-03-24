using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "UpdateSusEnum",
    story: "Updates the [EnumState] by SusLevel in [SusController]",
    category: "Action",
    id: "b327fedcd0987a4f45394096b1db6d8f"
)]
public partial class UpdateSusEnumAction : Action
{
    [SerializeReference]
    public BlackboardVariable<SusEnum> EnumState;

    [SerializeReference]
    public BlackboardVariable<SuspicionController> SusController;

    protected override Status OnUpdate()
    {
        float susLevel = SusController.Value.suspicionLevel;

        switch (susLevel)
        {
            case float n when n >= 90:
                EnumState.Value = SusEnum.Figured;
                break;
            case float n when n >= 75:
                EnumState.Value = SusEnum.MedSus;
                return Status.Success;
            default:
                EnumState.Value = SusEnum.NoSus;
                break;
        }

        return Status.Running;
    }
}
