using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderMeiSkillTwoSwordTrack : ActionState
{
    int randCound;
    GameObject swordTrack;
    public HerrscherOfThunderMeiSkillTwoSwordTrack(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
    }

    override public void Enter()
    {
        mAt.CrossFade("SkillTwoSwordTrack", 0.05f);
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
        swordTrack = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/SwordTrack")); 
        swordTrack.transform.position =HerrscherOfThunderMei.herrscherOfThunderMeiActor.transform.parent.
            gameObject.GetComponentInChildren<FllowHerrscherOfThunderMei>().transform.position+new Vector3(0, 0.861f, -0.297f);


        TimerMgr.Instance.OneShot("DelayDestroySwordTrack", 9.0f, DelayDestroySwordTrack);

    }


    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.Walk);

    }


    override public void AnimationEventOne()
    {

    }

    override public void AnimationEventTwo()
    {

    }

    void DelayDestroySwordTrack()
    { 
        GameObject.Destroy(swordTrack);
    }


    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ÎíÇÐÖ®»Ø¹â" || other.tag == "»êÑýµ¶_ÑªÓ£¼ÅÃð")
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderBeHitOne);
        }
    }

}
