using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiuQiuPopplejump : ActionState
{
    public QiuQiuPopplejump(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
        : base(type, am, master, pnt, cld)
    {

    }


    override public void Enter()
    {
        mAt.CrossFade("QQR_Jump", 0.05f);
        mRb.velocity = mRb.velocity + new Vector3(0f, mMaster.GetComponent<QiuQiuPoppleData>().qiuQiuPoppleJumpPower, 0f);

        // mAt.Play("Idle");
    }


    override public void Update()
    {
        RaycastHit hitinfo;
        if (Physics.Raycast(mMaster.transform.position + Vector3.up * 0.00f, mMaster.transform.forward, out hitinfo, 0.5f) == false)
        {
            mMaster.transform.position +=
                mMaster.transform.forward * mMaster.GetComponent<QiuQiuPoppleData>().qiuQiuPoppleJumpSpeed * Time.deltaTime;
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

