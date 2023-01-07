using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMstAttack : BehaviorState
{
    public AIMstAttack(GameObject master, ActionMachine am, BehaviorTree tree, BehaviorCondition.EType type,
        Func<bool> cdn = null,
        BehaviorState parent = null)
        : base(master, am, tree, type, cdn, parent)
    {

    }


    //刚进入这个状态的时候
    override public void OnEnter()
    {
    }



    //更新的时候
    override public void Update()
    {
    }



    //状态退出的时候
    override public void OnExit()
    {

    }
}
