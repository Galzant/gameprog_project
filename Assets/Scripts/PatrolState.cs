using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : FSMStates
{
    public PatrolState(Transform[] wp)
    {
        wayPoints = wp;
        stateID = FSMStateID.Patrolling;

        currentRotSpeed = 1.0f;
        currentSpeed = 2.0f;
    }

    public override void Reason(Transform player, Transform npc)
    {
        //transitions to chase state when player is detected
        if (Vector3.Distance(npc.position, player.position) <= 8.0f)
        {
            Debug.Log("Switched to Chase State");
            npc.GetComponent<EnemyController>().SetTransition(Transition.FoundPlayer);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        //finds another waypoint if one is already reached

        if (Vector3.Distance(npc.position, destPos) <= 2.0f)
        {
            Debug.Log("Reached destination. Moving to next destination.");
            FindNextPoint();
        }

        //rotate towards target
        Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position);
        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * currentRotSpeed);

        //move npc
        npc.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
    }
}
