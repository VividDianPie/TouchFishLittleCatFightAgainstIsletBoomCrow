using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//状态机
public class ActionMachine
{
    //状态机对象
    GameObject mMaster;
    //对象当前动作
    ActionState mCurrent;
    //保存动作的字典
    Dictionary<EActionType, ActionState> mActs;

    //属性当前动作
    public ActionState Current
    {
        get
        {
            return mCurrent;
        }
    }

    //状态机带参构造初始化当前对象
    public ActionMachine(GameObject master)
    {
        mMaster = master;
        mCurrent = null;
        mActs = new Dictionary<EActionType, ActionState>();
    }

    //状态机添加动作
    public bool AddAction(ActionState action)
    {
        if (mActs.ContainsKey(action.Type))
        {
            return false;
        }
        mActs.Add(action.Type, action);
        if (mCurrent == null)
        {
            mCurrent = action;
            mCurrent.Enter();
        }
        return true;
    }

    //状态机根据状态类型至状态字典中销毁对应状态
    public bool DeleteAction(EActionType type)
    {
        if (!mActs.ContainsKey(type))
        {
            return false;
        }
        if (mCurrent == mActs[type])
        {
            mCurrent.Exit();
        }
        mActs.Remove(type);
        EActionType[] keys = new EActionType[mActs.Keys.Count];
        mActs.Keys.CopyTo(keys, 0);
        mCurrent = mActs[keys[0]];
        return true;
    }

    public bool ChangeAction(EActionType type)
    {
        if (mActs.ContainsKey(type) == false)
        {
            return false;
        }
        
        mCurrent.Exit();
        mCurrent = null;
        mCurrent = mActs[type];       
        mCurrent.Enter();
        return true;
    }



    virtual public void Enter()
    {
        if (mCurrent != null)
        {
            mCurrent.Enter();
        }
    }


    virtual public void Update()
    {
        if (mCurrent != null)
        {
            mCurrent.Update();
        }
    }


    virtual public void Exit()
    {
        if (mCurrent != null)
        {
            mCurrent.Exit();
        }
    }


    virtual public void OnAnimationStart()
    {
        if (mCurrent != null)
        {
            mCurrent.OnAnimationStart();
        }
    }


    virtual public void OnAnimationHit(int i)
    {
        if (mCurrent != null)
        {
            mCurrent.OnAnimationHit(i);
        }
    }


    virtual public void OnAnimationEnd()
    {
        if (mCurrent != null)
        {
            mCurrent.OnAnimationEnd();
        }
    }

    virtual public void AnimationEventOne()
    {
        if (mCurrent != null)
        {
            mCurrent.AnimationEventOne();
        }
    }

    virtual public void AnimationEventTwo()
    {
        if (mCurrent != null)
        {
            mCurrent.AnimationEventTwo();
        }
    }

    virtual public void AnimationEventThree()
    {
        if (mCurrent != null)
        {
            mCurrent.AnimationEventThree();
        }
    }

    virtual public void OnCollisionEnter(Collision collision)
    {
        if (mCurrent != null)
        {
            mCurrent.OnCollisionEnter(collision);
        }
    }


    virtual public void OnCollisionStay(Collision collision)
    {
        if (mCurrent != null)
        {
            mCurrent.OnCollisionStay(collision);
        }
    }


    virtual public void OnCollisionExit(Collision collision)
    {
        if (mCurrent != null)
        {
            mCurrent.OnCollisionExit(collision);
        }
    }

    //触发器

    virtual public void OnTriggerEnter(Collider other)
    {
        if (mCurrent != null)
        {
            mCurrent.OnTriggerEnter(other);
        }
    }

    virtual public void OnTriggerStay(Collider other)
    {
        if (mCurrent != null)
        {
            mCurrent.OnTriggerStay(other);
        }
    }

    virtual public void OnTriggerExit(Collider other)
    {
        if (mCurrent != null)
        {
            mCurrent.OnTriggerExit(other);
        }
    }

}
