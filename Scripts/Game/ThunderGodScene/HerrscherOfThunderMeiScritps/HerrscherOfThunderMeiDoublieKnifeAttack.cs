using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderMeiDoublieKnifeAttack : ActionState
{
    public HerrscherOfThunderMeiDoublieKnifeAttack(EActionType type, ActionMachine am,
        GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
    }

    override public void Enter()
    {
        mAt.Play("DoublieKnifeAttack");
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
        HerrscherOfThunderMei.leftHandWeaPon.gameObject.SetActive(true);
    }


    override public void OnAnimationHit(int i)
    {
        SoundMgr.Instance.PlayEffect("Sound/Effects/HerrscherOfThunderMeiDoublieKnifeAttack");
        HerrscherOfThunderMei.leftHandWeaPon.GetComponent<CapsuleCollider>().enabled = true;
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().enabled = true;
        TimerMgr.Instance.OneShot("HerrscherOfThunderMeiLeftRightCapsuleColliderFalse", 0.25f,
            HerrscherOfThunderMeiLeftRightCapsuleColliderFalse);
    }
    void HerrscherOfThunderMeiLeftRightCapsuleColliderFalse()
    {
        HerrscherOfThunderMei.leftHandWeaPon.GetComponent<CapsuleCollider>().enabled = false;
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().enabled = false;
    }

    override public void OnAnimationEnd()
    {

    }
       
    override public void AnimationEventOne()
    {
        HerrscherOfThunderMei.leftHandWeaPon.gameObject.SetActive(false);
        mAm.ChangeAction(EActionType.Walk);
    }


    //override public void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "ÎíÇÐÖ®»Ø¹â" || other.tag == "»êÑýµ¶_ÑªÓ£¼ÅÃð")
    //    {
    //        mAm.ChangeAction(EActionType.HerrscherOfThunderBeHitOne);
    //    }
    //}

}
