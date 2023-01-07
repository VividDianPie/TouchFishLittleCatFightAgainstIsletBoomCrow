using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorState
{

    //父节点
    protected BehaviorState mParent;


    //子节点
    protected List<BehaviorState> mChildren;


    //所属游戏物体
    protected GameObject mMaster;


    //状态机
    protected ActionMachine mAm;


    //进入本状态的条件
    protected BehaviorCondition mCondition;


    //行为树
    protected BehaviorTree mTree;


    public BehaviorState Parent
    {
        get
        {
            return mParent;
        }
        set
        {
            mParent = value;
        }
    }


    public List<BehaviorState> Children
    {
        get
        {
            return mChildren;
        }
    }


    public BehaviorCondition Condition
    {
        get
        {
            return mCondition;
        }
    }


    public BehaviorState(GameObject master, ActionMachine am, BehaviorTree tree, BehaviorCondition.EType type,
        Func<bool> cdn = null, BehaviorState parent = null)
    {
        mMaster = master;
        mAm = am;
        mParent = parent;
        mTree = tree;
        mChildren = new List<BehaviorState>();
        mCondition = new BehaviorCondition(type, cdn);
    }


    //添加子节点
    public bool AddChild(BehaviorState child)
    {
        if (mChildren.LastIndexOf(child) >= 0)
        {
            return false;
        }
        mChildren.Add(child);
        return true;
    }


    //删除子节点
    public bool DltChild(BehaviorState child)
    {
        int index = mChildren.LastIndexOf(child);
        if (index < 0)
        {
            return false;
        }
        mChildren.RemoveAt(index);
        return true;
    }


    //添加子条件
    public bool AddSub(Func<bool> cdn)
    {
        return mCondition.AddSub(cdn);
    }


    //删除子条件
    public bool DltSub(Func<bool> cdn)
    {
        return mCondition.DltSub(cdn);
    }


    //刚进入这个状态的时候
    virtual public void OnEnter()
    {

    }



    //更新的时候
    virtual public void Update()
    {

    }



    //状态退出的时候
    virtual public void OnExit()
    {

    }

}
