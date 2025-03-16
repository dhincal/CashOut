using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "CheckDetector",
    story: "Check if [FieldOfView] returns true for canSeePlayer",
    category: "Action",
    id: "aec522aeeac31016ac5732d62df52883"
)]
public partial class CheckDetectorAction : Action
{
    [SerializeReference]
    public BlackboardVariable<FieldOfView> FieldOfView;

    protected override Status OnStart()
    {
        if (FieldOfView.Value.seeSuspicous)
        {
            Debug.Log("Player On sight!");
            return Status.Success;
        }
        else
        {
            Debug.Log("Player is not detected!");
            return Status.Failure;
        }
    }
}
