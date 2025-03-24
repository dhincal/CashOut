using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "MoveToPlayerPosition",
    story: "Move the npc to target [player] depending on the [susLevel]",
    category: "Action",
    id: "a7949448b5d626bc93a4418d7d610dae"
)]
public partial class MoveToPlayerPositionAction : Action
{
    [SerializeReference]
    public BlackboardVariable<GameObject> Player;

    [SerializeReference]
    public BlackboardVariable<SusEnum> susEnum;

    public float speed = 5f; // Adjust the speed as needed
    public Vector3 lastSeenPos; // The position to move to
    public Vector3 direction; // The direction to move in

    protected override Status OnStart()
    {
        Debug.Log("Moving to player position...");
        if (Player.Value != null)
        {
            lastSeenPos = Player.Value.transform.position;
            // Move the NPC to the player's position
            direction = (lastSeenPos - GameObject.transform.position).normalized;
            return Status.Running;
        }
        else
        {
            Debug.Log("Player not found!");
            return Status.Failure;
        }
    }

    protected override Status OnUpdate()
    {
        // Chase Player with a certain speed
        GameObject.transform.position += direction * speed * Time.deltaTime;
        if (susEnum.Value == SusEnum.Figured)
        {
            return Status.Failure;
        }

        if (Vector3.Distance(GameObject.transform.position, lastSeenPos) < 0.1f)
        {
            return Status.Success;
        }
        return Status.Running;
    }

    protected override void OnEnd() { }
}
