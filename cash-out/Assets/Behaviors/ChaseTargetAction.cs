using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "ChaseTarget",
    story: "Chase The [Target]",
    category: "Action",
    id: "67e7daa96e8399dc37a476835c78b491"
)]
public partial class ChaseTargetAction : Action
{
    [SerializeReference]
    public BlackboardVariable<GameObject> Target;

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

        Vector3 direction = (
            Target.Value.transform.position - GameObject.transform.position
        ).normalized;
        GameObject.transform.position += direction * 5f * Time.deltaTime;

        GameObject.transform.LookAt(Target.Value.transform);

        if (Vector3.Distance(GameObject.transform.position, Target.Value.transform.position) < 0.1f)
        {
            return Status.Success;
        }

        return Status.Running;
    }

    protected override void OnEnd() { }
}
