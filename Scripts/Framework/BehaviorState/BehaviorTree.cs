using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BehaviorTree
{

    //如何表示这棵数
    BehaviorState mRoot;


    //当前状态
    BehaviorState mCurrent;


    //所属的游戏对象
    GameObject mMaster;


    //状态机
    ActionMachine mAm;


    //保存所有的子树
    List<BehaviorState> mAll;



    public BehaviorState Root
    {
        get
        {
            return mRoot;
        }
    }


    public BehaviorState Current
    {
        get
        {
            return mCurrent;
        }
    }


    public GameObject Master
    {
        get
        {
            return mMaster;
        }
    }


    public ActionMachine Am
    {
        get
        {
            return mAm;
        }
    }


    //构造函数
    public BehaviorTree(GameObject master, ActionMachine am, BehaviorState root)
    {
        mCurrent = null;
        mAll = new List<BehaviorState>();
        mMaster = master;
        mAm = am;
        mRoot = root;
        if (root == null)
        {
            throw new Exception("BehaviorTree root is null!");
        }
        else
        {
            mAll.Add(root);
            mCurrent = root;
        }
    }


    //添加子节点
    public bool AddChild(BehaviorState p, BehaviorState c)
    {
        if (p == null || c == null || !mAll.Contains(p) || mAll.Contains(c))
        {
            return false;
        }
        p.AddChild(c);
        c.Parent = p;
        mAll.Add(c);
        return true;
    }


    //删除子节点
    public bool DltChild(BehaviorState c)
    {
        if (c == null || !mAll.Contains(c))
        {
            return false;
        }
        c.Parent.DltChild(c);
        mAll.Remove(c);
        return true;
    }


    public void OnEnter()
    {
        Current.OnEnter();
    }


    public void Update()
    {
        Current.Update();

        //判断哪个子状态的条件是满足
        for (int i = 0; i < Current.Children.Count; i++)
        {
            //某一个子节点状态条件满足，进入子条件
            if (Current.Children[i].Condition.Result())
            {
                //当前状态退出
                Current.OnExit();
                //新子状态进入
                Current.Children[i].OnEnter();
                //设置为当前状态
                mCurrent = Current.Children[i];
                break;
            }
        }
    }


    public void OnExit()
    {
        Current.OnExit();
    }


    public bool ChangeState(string stateName)
    {
        BehaviorState bs = null;
        for (int i = 0; i < mAll.Count; i++)
        {
            if (mAll[i].GetType().Name.Equals(stateName))
            {
                bs = mAll[i];
            }
        }

        if (bs == null)
        {
            return false;
        }

        Current.OnExit();
        bs.OnEnter();
        mCurrent = bs;

        return false;
    }



    public void ChangeToRoot()
    {
        //当前状态退出
        Current.OnExit();

        //新状态进入
        mRoot.OnEnter();

        //设置为当前状态
        mCurrent = mRoot;
    }
}
