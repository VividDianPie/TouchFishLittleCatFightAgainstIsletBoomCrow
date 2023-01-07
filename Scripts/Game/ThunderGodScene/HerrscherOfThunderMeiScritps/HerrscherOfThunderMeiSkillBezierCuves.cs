using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderMeiSkillBezierCuves : ActionState
{
    int randCound;
    public static Vector3 controlPointOne;
    public static Vector3 controlPointTwo;
    public HerrscherOfThunderMeiSkillBezierCuves(
        EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
    }

    override public void Enter()
    {
        mAt.Play("SkillBezierCuves");
        mAt.speed = 0.7f;
        mMaster.transform.forward = (new Vector3(BackBengMei.heroActor.position.x, mMaster.transform.position.y,
          BackBengMei.heroActor.position.z) - mMaster.transform.position).normalized;


        controlPointOne = new Vector3(
            Random.Range(HerrscherOfThunderMei.rightHandWeaPon.transform.position.x - 3, HerrscherOfThunderMei.rightHandWeaPon.transform.position.x + 3),
            Random.Range(HerrscherOfThunderMei.rightHandWeaPon.transform.position.y, HerrscherOfThunderMei.rightHandWeaPon.transform.position.y + 6),
            Random.Range(HerrscherOfThunderMei.rightHandWeaPon.transform.position.z - 1, BackBengMei.heroActor.transform.position.z + 1));

        controlPointTwo = new Vector3(
            Random.Range(HerrscherOfThunderMei.rightHandWeaPon.transform.position.x - 3, HerrscherOfThunderMei.rightHandWeaPon.transform.position.x + 3),
            Random.Range(HerrscherOfThunderMei.rightHandWeaPon.transform.position.y, HerrscherOfThunderMei.rightHandWeaPon.transform.position.y + 6),
            Random.Range(HerrscherOfThunderMei.rightHandWeaPon.transform.position.z - 1, BackBengMei.heroActor.transform.position.z + 1));
        
        TimerMgr.Instance.Repeated("LodBezierCuves", 0.12f, LodBezierCuves);
    }
    
    void LodBezierCuves()
    {
        GameObject.Destroy(GameObject.Instantiate(Resources.Load<GameObject>("Prefab/天殛之境_裁决BezierCuves")), 6.0f);
    }



    override public void Update()
    {

    }

    override public void Exit()
    {
        TimerMgr.Instance.DeleteTimer("LodBezierCuves");
    }
        

    override public void OnAnimationStart()
    {
    }


    override public void OnAnimationHit(int i)
    {

    }


    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.SkillBig);
        mAt.speed = 1;
        return;
    }



    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "雾切之回光" || other.tag == "魂妖刀_血樱寂灭")
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderBeHitOne);
            return;
        }
    }




}
