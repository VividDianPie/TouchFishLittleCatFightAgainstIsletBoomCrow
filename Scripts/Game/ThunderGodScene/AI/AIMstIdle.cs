using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//AI中idle
public class AIMstIdle : BehaviorState
{

    public AIMstIdle(GameObject master, ActionMachine am, BehaviorTree tree, BehaviorCondition.EType type,
        Func<bool> cdn = null,
        BehaviorState parent = null)
        : base(master, am, tree, type, cdn, parent)
    {
        
    }


    //刚进入这个状态的时候
    override public void OnEnter()
    {

        //先调整朝向
        mMaster.transform.forward = new Vector3(-1f, 0f, 0f);
    }



    //更新的时候  
    override public void Update()
    {
        //判断当前的动作状态结束
        if ((mAm.Current as BehaviorActionBase).StateFinish)
        {
        }
    }



    //状态退出的时候
    override public void OnExit()
    {

    }
}
