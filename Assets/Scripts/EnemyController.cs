using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : FSMTwo
{
    protected override void Initialize()
    {
        //get the tag 'Player'
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;

        //display error if Player tag is missing
        if (!playerTransform)
            print("No player detected. Add a 'Player' tag.");

        ConstructFSM();
    }

    protected override void FSMUpdate()
    {
        //nothing yet
    }

    protected override void FSMFixedUpdate()
    {
        CurrentState.Reason(playerTransform, transform);
        CurrentState.Act(playerTransform, transform);
    }

    public void SetTransition(Transition t)
    {
        PerformTransition(t);
    }

    private void ConstructFSM()
    {
        pointList = GameObject.FindGameObjectsWithTag("DestinationPoint");

        Transform[] wayPoints = new Transform[pointList.Length];

        int i = 0;

        foreach(GameObject obj in pointList)
        {
            wayPoints[i] = obj.transform;
            i++;
        }

        PatrolState patrol = new PatrolState(wayPoints);
        patrol.AddTransition(Transition.FoundPlayer, FSMStateID.Chasing);

        ChaseState chase = new ChaseState(wayPoints);
        chase.AddTransition(Transition.LostPlayer, FSMStateID.Patrolling);
        chase.AddTransition(Transition.ReachedPlayer, FSMStateID.Attacking);


        AddFSMState(patrol);
        AddFSMState(chase);

    }
}
