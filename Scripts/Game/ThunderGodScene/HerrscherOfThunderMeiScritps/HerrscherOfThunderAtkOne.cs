using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderAtkOne : ActionState
{
    int randCound;
    public HerrscherOfThunderAtkOne(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
    }

    override public void Enter()
    {
        mAt.Play("AtkOne");

        mMaster.transform.forward = (new Vector3(BackBengMei.heroActor.position.x, mMaster.transform.position.y,
          BackBengMei.heroActor.position.z) - mMaster.transform.position).normalized;
    }


    override public void Update()
    {

    }

    override public void Exit()
    {


    }


    override public void OnAnimationStart()
    {
    }


    override public void OnAnimationHit(int i)
    {
        SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengAtk1");
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().enabled = true;
        TimerMgr.Instance.OneShot("HerrscherOfThunderMeiAttackOneWeaPonRightUnActive", 0.3f, 
            HerrscherOfThunderMeiAttackOneWeaPonRightUnActive);
    }
    void HerrscherOfThunderMeiAttackOneWeaPonRightUnActive()
    {
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().enabled = false;
    }

    override public void OnAnimationEnd()
    {
    }

    override public void AnimationEventOne()
    {
        mAm.ChangeAction(EActionType.HerrscherOfThunderAtkTwo);
    }

    override public void AnimationEventTwo()
    {

    }


    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "����֮�ع�" || other.tag == "������_Ѫӣ����")
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderBeHitOne);
        }
    }

}
