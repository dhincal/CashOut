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

    protected override Status OnStart()
    {
        Debug.Log("Moving to player position...");
        if (Player.Value != null)
        {
            // Move the NPC to the player's position
            GameObject.transform.position = Player.Value.transform.position;
            Debug.Log("Moved to player position: " + Player.Value.transform.position);
            return Status.Success;
        }
        else
        {
            Debug.Log("Player not found!");
            return Status.Failure;
        }
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd() { }
}
