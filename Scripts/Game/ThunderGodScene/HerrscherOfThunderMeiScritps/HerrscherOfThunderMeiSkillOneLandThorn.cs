using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderMeiSkillOneLandThorn : ActionState
{
    float landThornForwordSpeed;

    GameObject skillOneLandThornForword;
    GameObject skillOneLandThornLeft;
    GameObject skillOneLandThornRight;

    Vector3 vecForword;
    Vector3 vecLeft;
    Vector3 vecRight;
    public HerrscherOfThunderMeiSkillOneLandThorn(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null,
        ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        landThornForwordSpeed = 6;
    }

    override public void Enter()
    {
        mAt.Play("SkillOneLandThorn");
        mAt.speed = 0.5f;
        //更新朝向
        mMaster.transform.forward = (new Vector3(BackBengMei.heroActor.position.x, mMaster.transform.position.y,
         BackBengMei.heroActor.position.z) - mMaster.transform.position).normalized;
        skillOneLandThornForword = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/SkillOneLandThorn"));
        skillOneLandThornForword.GetComponentInChildren<MeshRenderer>().enabled = false;
        skillOneLandThornLeft = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/SkillOneLandThorn"));
        skillOneLandThornLeft.GetComponentInChildren<MeshRenderer>().enabled = false;
        skillOneLandThornRight = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/SkillOneLandThorn"));
        skillOneLandThornRight.GetComponentInChildren<MeshRenderer>().enabled = false;
        //技能朝向
        skillOneLandThornForword.transform.forward = mMaster.transform.forward;
        skillOneLandThornLeft.transform.LookAt(
            new Vector3(mMaster.transform.position.x, mMaster.transform.position.y - 1.514f, mMaster.transform.position.z)
            + -mMaster.transform.right * 2 + mMaster.transform.forward * 5);
        skillOneLandThornRight.transform.LookAt(
                     new Vector3(mMaster.transform.position.x, mMaster.transform.position.y - 1.514f, mMaster.transform.position.z)
            + mMaster.transform.right * 2 + mMaster.transform.forward * 5);
        //保存对应方向
        vecForword = skillOneLandThornForword.transform.forward;
        vecLeft = skillOneLandThornLeft.transform.forward;
        vecRight = skillOneLandThornRight.transform.forward;

        TimerMgr.Instance.Repeated("SkillLandThornLoad", 0.1f, SkillLandThornLoad);
        TimerMgr.Instance.OneShot("SkillLandThornLoadStop", 1.5f, SkillLandThornLoadStop);
    }

    override public void Update()
    {
         
        if (skillOneLandThornForword != null && skillOneLandThornLeft != null && skillOneLandThornRight != null)
        {
            skillOneLandThornForword.transform.position += vecForword * landThornForwordSpeed * Time.deltaTime;
            skillOneLandThornLeft.transform.position += vecLeft * landThornForwordSpeed * Time.deltaTime;
            skillOneLandThornRight.transform.position += vecRight * landThornForwordSpeed * Time.deltaTime;
        }
      
    }



    override public void Exit()
    {
        mAt.speed = 1.0f;



    }


    override public void OnAnimationStart()
    {

    }


    override public void OnAnimationHit(int i)
    {

    }


    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.HerrscherOfThunderEvadeForwardOne);
    }


    override public void AnimationEventOne()
    {
    }



    void SkillLandThornLoad()
    {
        GameObject.Instantiate(Resources.Load<GameObject>("Prefab/SkillOneLandThorn")).transform.position =
            new Vector3(skillOneLandThornForword.transform.position.x, skillOneLandThornForword.transform.position.y - 1.514f,
            skillOneLandThornForword.transform.position.z);
        GameObject.Instantiate(Resources.Load<GameObject>("Prefab/SkillOneLandThorn")).transform.position =
            new Vector3(skillOneLandThornLeft.transform.position.x, skillOneLandThornLeft.transform.position.y - 1.514f,
            skillOneLandThornLeft.transform.position.z);
        GameObject.Instantiate(Resources.Load<GameObject>("Prefab/SkillOneLandThorn")).transform.position =
       new Vector3(skillOneLandThornRight.transform.position.x, skillOneLandThornRight.transform.position.y - 1.514f,
            skillOneLandThornRight.transform.position.z);
    }
    void SkillLandThornLoadStop()
    { 
        TimerMgr.Instance.DeleteTimer("SkillLandThornLoad");
    }

    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "雾切之回光" || other.tag == "魂妖刀_血樱寂灭")
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderBeHitOne);
        }
    }

}
