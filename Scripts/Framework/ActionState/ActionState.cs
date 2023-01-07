using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//动作状态行为
public class ActionState
{

    //行为父节点
    protected ActionState mParent;

    //行为子节点
    protected ActionState mChild;

    //行为状态的类型
    protected EActionType mType;

    //当前行为状态对象
    protected GameObject mMaster;

    //当前角色的动画控制器
    protected Animator mAt;

    //当前角色的刚体
    protected Rigidbody mRb;

    //状态机
    protected ActionMachine mAm;



    public ActionState Parent
    {
        get
        {
            return mParent;
        }
        protected set
        {
            mParent = value;
        }
    }


    public ActionState Child
    {
        get
        {
            return mChild;
        }
        protected set
        {
            mChild = value;
        }
    }

    
    public EActionType Type
    {
        get
        {
            return mType;
        }
        protected set
        {
            mType = value;
        }
    }

    //构造行为动作状态
    public ActionState(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    {
        mType = type;
        mAm = am;
        mMaster = master;
        Parent = pnt;
        Child = cld;
        mRb = mMaster.GetComponent<Rigidbody>();
        mAt = mMaster.GetComponent<Animator>();
    }


    virtual public void Enter()
    {

    }


    virtual public void Update()
    {

    }


    virtual public void Exit()
    {

    }


    virtual public void OnAnimationStart()
    {

    }

    virtual public void OnAnimationHit(int i)
    {

    }


    virtual public void OnAnimationEnd()
    {

    }

    virtual public void AnimationEventOne()
    {

    }

    virtual public void AnimationEventTwo()
    {

    }

    virtual public void AnimationEventThree()
    {

    }

    //对撞机
    virtual public void OnCollisionEnter(Collision collision)
    {

    }


    virtual public void OnCollisionStay(Collision collision)
    {

    }


    virtual public void OnCollisionExit(Collision collision)
    {

    }


    //触发器
    virtual public void OnTriggerEnter(Collider other)
    {

    }


    virtual public void OnTriggerStay(Collider other)
    {

    }


    virtual public void OnTriggerExit(Collider other)
    {

    }

}
