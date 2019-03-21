using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : FSMStates
{
    public ChaseState(Transform[] wp)
    {
        wayPoints = wp;
        stateID = FSMStateID.Chasing;

        currentRotSpeed = 1.0f;
        currentSpeed = 2.2f;

        //find next waypoint position
        FindNextPoint();
    }

    public override void Reason(Transform player, Transform npc)
    {
        
        destPos = player.position;

        //switches to attack state when closer to the target
        float dist = Vector3.Distance(npc.position, destPos);
        if (dist <= 7.0f)
        {
            Debug.Log("Switched to Attack state");
            npc.GetComponent<EnemyController>().SetTransition(Transition.ReachedPlayer);

        }
        //switches to patrol state if target is too far
        else if (dist >= 15.0f)
        {
            Debug.Log("Switched to Patrol state");
            npc.GetComponent<EnemyController>().SetTransition(Transition.LostPlayer);
        }
    }

    public override void Act(Transform player, Transform npc)
    {
        
        destPos = player.position;

        Quaternion targetRotation = Quaternion.LookRotation(destPos - npc.position);
        npc.rotation = Quaternion.Slerp(npc.rotation, targetRotation, Time.deltaTime * currentRotSpeed);

        
        npc.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
    }
}
