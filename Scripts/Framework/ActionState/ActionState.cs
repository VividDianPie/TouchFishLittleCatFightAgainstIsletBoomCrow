using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//����״̬��Ϊ
public class ActionState
{

    //��Ϊ���ڵ�
    protected ActionState mParent;

    //��Ϊ�ӽڵ�
    protected ActionState mChild;

    //��Ϊ״̬������
    protected EActionType mType;

    //��ǰ��Ϊ״̬����
    protected GameObject mMaster;

    //��ǰ��ɫ�Ķ���������
    protected Animator mAt;

    //��ǰ��ɫ�ĸ���
    protected Rigidbody mRb;

    //״̬��
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

    //������Ϊ����״̬
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

    //��ײ��
    virtual public void OnCollisionEnter(Collision collision)
    {

    }


    virtual public void OnCollisionStay(Collision collision)
    {

    }


    virtual public void OnCollisionExit(Collision collision)
    {

    }


    //������
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
