using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiuQiuPoppleBeHitTwo : ActionState
{
    public QiuQiuPoppleBeHitTwo(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
        : base(type, am, master, pnt, cld)
    {

    }


    override public void Enter()
    {
        mAt.CrossFade("QQRBeHitOneTwo", 0.05f);
        // mAt.Play("Idle");

        mMaster.GetComponent<QiuQiuPoppleData>().qiuQiuPoppIsAnger = true;

    }


    override public void Update()
    {
        if (BackBengMei.heroActor.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AttackFive") ||
             BackBengMei.heroActor.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("EvadeEndToAttack"))
        {
            mAm.ChangeAction(EActionType.QiuQiuPoppleDie);
            return;
        }
    }


    override public void OnAnimationStart()
    {

    }



    override public void OnAnimationHit(int i)
    {

    }



    override public void OnAnimationEnd()
    {
        if (mMaster.GetComponent<QiuQiuPoppleData>().qiuQiuPoppIsAnger == true)
        {
            mAm.ChangeAction(EActionType.QiuQiuPoppleRun);
            return;
        }
        mAm.ChangeAction(EActionType.QiuQiuPoppleWalk);
    }
    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ÎíÇÐÖ®»Ø¹â" || other.tag == "»êÑýµ¶_ÑªÓ£¼ÅÃð")
        {
            mAm.ChangeAction(EActionType.QiuQiuPoppleBeHitOne);
            return;
        }
    }
}

