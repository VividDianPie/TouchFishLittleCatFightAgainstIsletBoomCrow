using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiBeHitOne : ActionState
{
    bool isCanMove;

    SkinnedMeshRenderer facialExpression;
    public BackBengMeiBeHitOne(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        isCanMove = false;
        facialExpression = mMaster.transform.Find("Body").transform.GetComponent<SkinnedMeshRenderer>();
    }

    override public void Enter()
    {
        mAt.Play("BeHitOne");
        isCanMove = false;
        facialExpression.SetBlendShapeWeight(62, 100); 

    }


    override public void Update()
    {
        // Pc 端输入操作 
        if (Application.platform != RuntimePlatform.Android)
        {
            if (isCanMove == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mAm.ChangeAction(EActionType.AttackOne);
                    return;
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    mAm.ChangeAction(EActionType.EvadeBackwardOne);
                    return;
                }
            }
        }

        // Android 端输入操作
        if (Application.platform == RuntimePlatform.Android)
        {
            if (isCanMove == true)
            {
                if (EasyTouchEvent.buttonAState == "ADown")
                {
                    mAm.ChangeAction(EActionType.AttackOne);
                    return;
                }
                else if (EasyTouchEvent.buttonEvadeState == "EvadeDown")
                {
                    mAm.ChangeAction(EActionType.EvadeBackwardOne);
                    return;
                }
            }
        }
    }

    override public void Exit()
    {
        isCanMove = false;
        facialExpression.SetBlendShapeWeight(62, 0); ;

    }


    override public void OnAnimationStart()
    {

    }



    override public void OnAnimationHit(int i)
    {

    }


    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.Idle);
    }



    override public void AnimationEventOne()
    {
        SoundMgr.Instance.PlayEffect("Sound/Effects/挺疼的");

    }
    override public void AnimationEventTwo()
    {
        isCanMove = true;
    }


    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "天殛之境_裁决")
        {
            if (HerrscherOfThunderMei.herrscherOfThunderMeiActor.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DoublieKnifeAttack") ||
                HerrscherOfThunderMei.herrscherOfThunderMeiActor.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AtkFive"))
            {
                mAm.ChangeAction(EActionType.BeHitRetreat);
            }
            else
            {
                mAm.ChangeAction(EActionType.BeHitTwo);
            }
        }
        else if (other.tag == "QQRWeapon")
        {
            mAm.ChangeAction(EActionType.BeHitTwo);
        }
    }

}
