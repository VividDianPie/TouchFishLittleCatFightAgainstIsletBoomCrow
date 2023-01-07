using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : BehaviorActionBase
{

    //构造函数
    public MonsterIdle(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
         : base(type, am, master, pnt, cld)
    {
        
    }


    override public void Enter()
    {
        base.Enter();

        //切换动画到idle
        mAt.CrossFade("Idle", 0.05f);
    }


    override public void OnAnimationEnd()
    {
        base.OnAnimationEnd();

        //todo
    }
}
