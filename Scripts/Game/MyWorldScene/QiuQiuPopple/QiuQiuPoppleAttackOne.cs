using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiuQiuPoppleAttackOne : ActionState
{
    public QiuQiuPoppleAttackOne(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
        : base(type, am, master, pnt, cld)
    {

    }


    override public void Enter()
    {
        mAt.CrossFade("QQRAttackOne", 0.05f);
        // mAt.Play("Idle");
        mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.parent = mMaster.GetComponent<QiuQiuPopple>().rightHand.transform;

        mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.localPosition = new Vector3(0.61f, 0.058f, -0.465f);
        mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.localRotation = Quaternion.Euler(-177.113f, 125.868f, -273.452f);
        mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.localScale = new Vector3(1.0f, 1.0f, 1.0f);

    }


    override public void Update()
    {

    }


    override public void OnAnimationStart()
    {

    }



    override public void OnAnimationHit(int i)
    {
        mMaster.transform.GetComponent<QiuQiuPopple>().weaPonRightHand.GetComponent<CapsuleCollider>().enabled = true;
        TimerMgr.Instance.OneShot("QQRRightAttackRectUnEnabled", 0.35f, QQRRightAttackRectUnEnabled);
    }
    void QQRRightAttackRectUnEnabled()
    {
        mMaster.transform.GetComponent<QiuQiuPopple>().weaPonRightHand.GetComponent<CapsuleCollider>().enabled = false;
    }


    override public void OnAnimationEnd()
    {
        
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

