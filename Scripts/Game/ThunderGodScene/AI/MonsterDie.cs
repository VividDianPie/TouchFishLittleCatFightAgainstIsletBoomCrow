using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDie : ActionState
{
    public MonsterDie(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {

    }


    override public void Enter()
    {
        mAt.CrossFade("Die", 0.05f);
    }


    override public void Update()
    {

    }

    override public void Exit()
    {

    }


    override public void OnAnimationStart()
    {

    }



    override public void OnAnimationHit(int i)
    {
     
    }



    override public void OnAnimationEnd()
    {
        
    }
}
