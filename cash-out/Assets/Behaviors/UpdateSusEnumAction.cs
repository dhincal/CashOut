using System;
using System.Threading.Tasks;
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

    private bool alreadySus = false;

    protected override Status OnUpdate()
    {
        float susLevel = SusController.Value.suspicionLevel;
        if (!alreadySus)
        {
            switch (susLevel)
            {
                case float n when n >= 90:
                    EnumState.Value = SusEnum.Figured;
                    return Status.Success;
                case float n when n >= 50:
                    EnumState.Value = SusEnum.MedSus;
                    alreadySus = true; // Mark as already in a suspicious state.
                    break;
                default:
                    EnumState.Value = SusEnum.NoSus;
                    break;
            }
        }
        else
        {
            // If we are already in a suspicious state, we can check if we need to update it further.
            switch (susLevel)
            {
                case float n when n >= 90:
                    EnumState.Value = SusEnum.Figured;
                    return Status.Success; // We have reached the highest state, no need to check further.
                case float n when n == 0:
                    EnumState.Value = SusEnum.NoSus;
                    alreadySus = false; // Reset the flag since we are no longer in a suspicious state.
                    break;
                default:
                    // No change needed, we are already in the highest state.
                    break;
            }
        }

        return Status.Running;
    }
}
