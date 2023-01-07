using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorCondition 
{
    //与类型或类型
    public enum EType
    {
        And,
        Or,
    }


    //我自己条件的类型
    EType mType;


    //条件函数指针链表
    List<Func<bool>> mSubConditions;


    //带参构造添加首个条件回调函数指针
    public BehaviorCondition(EType type, Func<bool> cdn = null)
    {
        mType = type;
        mSubConditions = new List<Func<bool>>();
        if (cdn != null)
        {
            mSubConditions.Add(cdn);
        }
    }


    //添加条件
    public bool AddSub(Func<bool> cdn)
    {
        if (mSubConditions.Contains(cdn))
        {
            return false;
        }
        mSubConditions.Add(cdn);
        return true;
    }


    //删除条件
    public bool DltSub(Func<bool> cdn)
    {
        int index = mSubConditions.LastIndexOf(cdn);
        if (index < 0)
        {
            return false;
        }
        mSubConditions.RemoveAt(index);
        return true;
    }


    //得到条件的结果
    public bool Result()
    {
        if(mSubConditions.Count == 0)
        {
            return false;
        }
        bool ret = mType == EType.And ? true : false;
        for (int i = 0; i < mSubConditions.Count; i++)
        {
            if (mType == EType.And)
            {
                ret = ret && mSubConditions[i]();
            }
            else
            {
                ret = ret || mSubConditions[i]();
            }
        }
        return ret;
    }
}
