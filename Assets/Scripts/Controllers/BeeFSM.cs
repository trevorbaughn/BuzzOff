using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeFSM : AIController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //start idle
        ChangeState(AIStates.Idle);

        //target start w/nothing
        AITarget = null;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    // Update is called once per frame
    protected override void Update()
    {
        MakeDecisions();
    }

    protected override void MakeDecisions()
    {
        if (pawn == null) return; //prevent null reference errors

        //FSM
        //based on current state
        switch (currentState)
        {
            case AIStates.Idle:
                //do state
                DoIdleState();
                //check for change state
                if (IsTimePassed(1))
                {
                    ChangeState(AIStates.ChooseTarget);
                }
                break;
            case AIStates.ChooseTarget:
                DoChooseTargetState(); //will stay here til' a target is found

                ChangeState(AIStates.Chase);
                break;
            case AIStates.Chase:
                if (AITarget == null)
                {
                    ChangeState(AIStates.ChooseTarget);
                }

                DoChaseState();
                break;

        }
    }
}
