using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "ChaseTarget",
    story: "Chase The [Target] Until [Suspicious] is 0",
    category: "Action",
    id: "67e7daa96e8399dc37a476835c78b491"
)]
public partial class ChaseTargetAction : Action
{
    [SerializeReference]
    public BlackboardVariable<GameObject> Target;

    [SerializeReference]
    public BlackboardVariable<SusEnum> Suspicious;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (Target.Value == null)
        {
            return Status.Failure;
        }

        GameObject.transform.LookAt(Target.Value.transform);
        GameObject.transform.position += GameObject.transform.forward * 5f * Time.deltaTime;

        if (Vector3.Distance(GameObject.transform.position, Target.Value.transform.position) < 0.1f)
        {
            return Status.Success;
        }

        if (Suspicious.Value == SusEnum.NoSus)
        {
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override void OnEnd() { }
}
