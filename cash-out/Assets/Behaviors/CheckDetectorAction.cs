using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "CheckDetector",
    story: "Check if [FieldOfView] value for [suspicious]",
    category: "Action",
    id: "aec522aeeac31016ac5732d62df52883"
)]
public partial class CheckDetectorAction : Action
{
    [SerializeReference]
    public BlackboardVariable<FieldOfView> FieldOfView;

    [SerializeReference]
    public BlackboardVariable<bool> Suspicious;

    protected override Status OnUpdate()
    {
        if (FieldOfView.Value.seeSuspicous)
        {
            Suspicious.Value = true;
            Debug.Log("Player On sight!");
            return Status.Running;
        }
        else
        {
            Suspicious.Value = false;
            Debug.Log("Player is not detected!");
            return Status.Running;
        }
    }
}
