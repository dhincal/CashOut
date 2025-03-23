using System;
using Unity.Behavior;
using Unity.Properties;
using UnityEngine;
using Action = Unity.Behavior.Action;

[Serializable, GeneratePropertyBag]
[NodeDescription(
    name: "MoveToPlayerPosition",
    story: "Move the npc to target [player]",
    category: "Action",
    id: "a7949448b5d626bc93a4418d7d610dae"
)]
public partial class MoveToPlayerPositionAction : Action
{
    [SerializeReference]
    public BlackboardVariable<GameObject> Player;

    public float speed = 5f; // Adjust the speed as needed

    protected override Status OnStart()
    {
        Debug.Log("Moving to player position...");
        if (Player.Value != null)
        {
            // Move the NPC to the player's position
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
        Vector3 direction = (
            Player.Value.transform.position - GameObject.transform.position
        ).normalized;
        GameObject.transform.position += direction * speed * Time.deltaTime;
        Debug.Log("Chasing player: " + Player.Value.transform.position);
        return Status.Running;
    }

    protected override void OnEnd() { }
}
