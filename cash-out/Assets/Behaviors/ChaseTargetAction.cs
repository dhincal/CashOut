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
        Rigidbody rb = GameObject.GetComponent<Rigidbody>();
        rb.transform.rotation = Quaternion.LookRotation(
            Target.Value.transform.position - GameObject.transform.position
        ); // Ensure the agent is facing the target
        rb.linearVelocity = GameObject.transform.rotation * Vector3.forward * 5f; // Stop any existing movement

        if (Vector3.Distance(GameObject.transform.position, Target.Value.transform.position) < 7f)
        {
            GameObject.GetComponent<EnemyNPCController>().AimToShoot(); // Call the AimToShoot method from EnemyNPCController
        }

        if (Vector3.Distance(GameObject.transform.position, Target.Value.transform.position) < 5f)
        {
            rb.linearVelocity = Vector3.zero; // Stop the agent when close to the target
            // Make enemy shoot if close to the target
            rb.transform.rotation = Quaternion.LookRotation(
                Target.Value.transform.position - GameObject.transform.position
            ); // Ensure the agent is facing the target
        }

        if (Suspicious.Value == SusEnum.NoSus)
        {
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override void OnEnd() { }
}
