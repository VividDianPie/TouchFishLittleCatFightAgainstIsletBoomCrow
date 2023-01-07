using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderAtkFive : ActionState
{
    int randCound;
    public HerrscherOfThunderAtkFive(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
    }

    override public void Enter()
    {
        mAt.Play("AtkFive");

        mMaster.transform.forward = (new Vector3(BackBengMei.heroActor.position.x, mMaster.transform.position.y,
          BackBengMei.heroActor.position.z) - mMaster.transform.position).normalized;
    }


    override public void Update()
    {


    }

    override public void Exit()
    {
        //´Ë×´Ì¬ÍË³öÊ±Çå¿Õ×óÊÖ½£
        if (HerrscherOfThunderMei.leftHandWeaPon.gameObject == true)
        {
            HerrscherOfThunderMei.leftHandWeaPon.gameObject.SetActive(false);
        }
    }


    override public void OnAnimationStart()
    {
        HerrscherOfThunderMei.leftHandWeaPon.gameObject.SetActive(true);
    }

    //tesgt
    override public void OnAnimationHit(int i)
    {
        SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengAtk5");
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().enabled = true;
        HerrscherOfThunderMei.leftHandWeaPon.GetComponent<CapsuleCollider>().enabled = true;
        TimerMgr.Instance.OneShot("HerrscherOfThunderMeiAttackOneWeaPonRightUnActive", 0.5f,
            HerrscherOfThunderMeiAttackOneWeaPonRightUnActive);
    }
    void HerrscherOfThunderMeiAttackOneWeaPonRightUnActive()
    {
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().enabled = false;
        HerrscherOfThunderMei.leftHandWeaPon.GetComponent<CapsuleCollider>().enabled = false;
    }

    override public void OnAnimationEnd()
    {

    }

    override public void AnimationEventOne()
    {

    }

    override public void AnimationEventTwo()
    {
        HerrscherOfThunderMei.leftHandWeaPon.gameObject.SetActive(false);
    }

    override public void AnimationEventThree()
    {
        mAm.ChangeAction(EActionType.Walk);
    }


    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ÎíÇÐÖ®»Ø¹â" || other.tag == "»êÑýµ¶_ÑªÓ£¼ÅÃð")
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderBeHitOne);
        }
    }
}
