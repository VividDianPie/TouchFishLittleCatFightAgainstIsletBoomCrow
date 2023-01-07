using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�жϵ�ǰ�����Ƿ����
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

    //�����˳�
    //override public void Exit()
    //{
    //    mIsStateFinish = true;
    //}

    //��������
    override public void OnAnimationEnd()
    {
        mIsStateFinish = true;
    }
}
