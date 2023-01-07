using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderMeiSkillBig : ActionState
{
    int randCound;
    GameObject skillBigPrefab;

    public HerrscherOfThunderMeiSkillBig(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {

    }

    override public void Enter()
    {
        mAt.Play("SkillBig");
        mAt.speed = 0.5f;

        EventManager.Instance.DispatchEvent(new MyEvent(EEventType.HerrscherOfThunderMeiEventSkillBig, null));
    }


    override public void Update()
    {


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
        skillBigPrefab = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/SkillBigPrefab"));
        skillBigPrefab.transform.position = HerrscherOfThunderMei.herrscherOfThunderMeiActor.transform.parent.
            gameObject.GetComponentInChildren<FllowHerrscherOfThunderMei>().transform.position + Vector3.up*0f;
    }


    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.DoublieKnifeAttack);
    }



    //override public void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "ÎíÇÐÖ®»Ø¹â" || other.tag == "»êÑýµ¶_ÑªÓ£¼ÅÃð")
    //    {
    //        mAm.ChangeAction(EActionType.HerrscherOfThunderBeHitTwo);
    //    }
    //}

}
