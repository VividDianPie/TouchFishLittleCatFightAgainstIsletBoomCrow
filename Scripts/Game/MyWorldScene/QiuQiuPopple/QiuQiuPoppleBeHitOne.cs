using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiuQiuPoppleBeHitOne : ActionState
{
    public QiuQiuPoppleBeHitOne(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
        : base(type, am, master, pnt, cld)
    {

    }


    override public void Enter()
    {
        mAt.CrossFade("QQRBeHitOne", 0.05f);
        // mAt.Play("Idle");
        mMaster.GetComponent<QiuQiuPoppleData>().qiuQiuPoppIsAnger = true;

        mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.parent = mMaster.GetComponent<QiuQiuPopple>().QQRActor;
        mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.transform.localPosition = mMaster.GetComponent<QiuQiuPopple>().QQRActor.localPosition;
        mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.localPosition = new Vector3(0.1987187f, 1.399445f, -0.1305424f);
        mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.localRotation = Quaternion.Euler(-239.239f, 49.55299f, 45.229f);
        mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.localScale = new Vector3(1.0f, 1.0f, 1.0f);

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
            mAm.ChangeAction(EActionType.QiuQiuPoppleBeHitTwo);
            return;
        }
    }
}

