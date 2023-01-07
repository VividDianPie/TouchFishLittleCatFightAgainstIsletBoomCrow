using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiuQiuPoppleRun : ActionState
{
    int randCound;
    public QiuQiuPoppleRun(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
        : base(type, am, master, pnt, cld)
    {
        randCound = -1;

    }


    override public void Enter()
    {
        mAt.CrossFade("QQR_Run", 0.05f);
        // mAt.Play(" ");
        randCound = Random.Range(0, 6);
    }


    override public void Update()
    {
        if (mMaster.transform.position.y < -10)
        {
            GameObject.Destroy(mMaster.transform.gameObject);
        }
        //如果没有碰撞那么可走
        RaycastHit hitinfo;
        if (Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, mMaster.transform.forward, out hitinfo, 0.5f) == false)
        {
            mMaster.transform.position +=
                mMaster.transform.forward * mMaster.GetComponent<QiuQiuPoppleData>().qiuQiuPoppleRunSpeed * Time.deltaTime;
            //小怪朝向角色
            mMaster.transform.forward = (new Vector3(BackBengMei.heroActor.position.x, mMaster.transform.position.y,
       BackBengMei.heroActor.position.z) - mMaster.transform.position).normalized;
        }
        //碰撞随机跳跃
        else if (hitinfo.transform.gameObject.name == "Chunk(Clone)")
        {
            if (randCound == 0)
            {
                mMaster.transform.forward = mMaster.transform.right;
            }
            else if (randCound == 1)
            {
                mMaster.transform.forward = -mMaster.transform.right;
            }
            mAm.ChangeAction(EActionType.QiuQiuPoppleJump);
            return;
        }
        if (Physics.Raycast(mMaster.transform.position + Vector3.up * 1.5f, mMaster.transform.forward, out hitinfo, 0.5f) == true &&
          hitinfo.transform.gameObject.name == "Chunk(Clone)")
        {
            mMaster.transform.forward = (randCound == 1) ? (mMaster.transform.right) : (-mMaster.transform.right);
            mAm.ChangeAction(EActionType.QiuQiuPoppleJump);
        }
        if ((BackBengMei.heroActor.position - mMaster.transform.position).sqrMagnitude <= 1f)
        {
            if (randCound < 3)
            {
                mAm.ChangeAction(EActionType.QiuQiuPoppleAttackOne);
            }
            else
            {
                mAm.ChangeAction(EActionType.QiuQiuPoppleAttackTwo);
            }
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

    }
    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "雾切之回光" || other.tag == "魂妖刀_血樱寂灭")
        {
            mAm.ChangeAction(EActionType.QiuQiuPoppleBeHitOne);
            return;
        }
    }

}

