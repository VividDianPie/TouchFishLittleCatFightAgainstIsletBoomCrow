using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//判断当前动画是否结束
public class BehaviorActionBase: ActionState
{
    bool mIsStateFinish;
    public bool StateFinish
    {
        get
        {
            return mIsStateFinish;
        }
    }


    public BehaviorActionBase(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
        :base(type, am, master, pnt, cld)
    {
        mIsStateFinish = false;
    }


    override public void Enter()
    {
        mIsStateFinish = false;
    }

    //动画退出
    //override public void Exit()
    //{
    //    mIsStateFinish = true;
    //}

    //动画结束
    override public void OnAnimationEnd()
    {
        mIsStateFinish = true;
    }
}
