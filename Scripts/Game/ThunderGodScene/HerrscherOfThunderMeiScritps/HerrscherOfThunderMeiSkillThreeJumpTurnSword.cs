using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderMeiSkillThreeJumpTurnSword : ActionState
{
    int randCound;
    bool canMove;
    public HerrscherOfThunderMeiSkillThreeJumpTurnSword(
        EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {

    }

    override public void Enter()
    {
        mAt.Play("SkillThreeJumpTurnSword");
        mMaster.transform.forward = (new Vector3(BackBengMei.heroActor.position.x, mMaster.transform.position.y,
           BackBengMei.heroActor.position.z) - mMaster.transform.position).normalized;
        //mRb.velocity = mRb.velocity + 
        //    new Vector3(0f, mMaster.GetComponent<HerrscherOfThunderMeiData>().herrscherOfThunderMeiSkillThreeJumpTurnSwordJump, 0f);
        canMove = true;
    }


    override public void Update()
    {
        RaycastHit hitinfo;
        if ((canMove == true) &&
            (Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, mMaster.transform.forward, out hitinfo, 0.5f) == false))
        {
            mMaster.transform.position += mMaster.transform.forward *
            mMaster.GetComponent<HerrscherOfThunderMeiData>().herrscherOfThunderMeiSkillThreeJumpTurnSwordFword * Time.deltaTime;
        }

    }

    override public void Exit()
    {
        if (HerrscherOfThunderMei.leftHandWeaPon.gameObject == true)
        {
            HerrscherOfThunderMei.leftHandWeaPon.gameObject.SetActive(true);
        }
    }


    override public void OnAnimationStart()
    {
        HerrscherOfThunderMei.leftHandWeaPon.gameObject.SetActive(true);
    }


    override public void OnAnimationHit(int i)
    {
        SoundMgr.Instance.PlayEffect("Sound/Effects/JumpTurnSword");
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().enabled = true;
        TimerMgr.Instance.OneShot("HerrscherOfThunderMeiSkillThreeWeaPonRightUnActive", 0.8f,
            HerrscherOfThunderMeiSkillThreeWeaPonRightUnActive);
    }
    void HerrscherOfThunderMeiSkillThreeWeaPonRightUnActive()
    {
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().enabled = false;
    }

    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.Walk);
    }


    override public void AnimationEventOne()
    {
        canMove = false;
        HerrscherOfThunderMei.leftHandWeaPon.gameObject.SetActive(false);
        if (Vector3.Distance(mMaster.transform.position, BackBengMei.heroActor.position) <= 0.9f)
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderAtkFive);
            return;
        }
    }


    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ÎíÇÐÖ®»Ø¹â" || other.tag == "»êÑýµ¶_ÑªÓ£¼ÅÃð")
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderBeHitOne);
        }
    }


}

