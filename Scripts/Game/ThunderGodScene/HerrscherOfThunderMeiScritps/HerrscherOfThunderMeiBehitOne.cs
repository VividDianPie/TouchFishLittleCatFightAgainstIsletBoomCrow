using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderMeiBehitOne : ActionState
{
    int randCound;
    SkinnedMeshRenderer facialExpression;

    public HerrscherOfThunderMeiBehitOne(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        facialExpression = mMaster.transform.Find("Body").transform.GetComponent<SkinnedMeshRenderer>();

    }

    override public void Enter()
    {
        mAt.Play("BeHtiOne");
        facialExpression.SetBlendShapeWeight(21,100);
    }


    override public void Update()
    {


    }

    override public void Exit()
    {
        facialExpression.SetBlendShapeWeight(21, 0);
    }


    override public void OnAnimationStart()
    {
    }


    override public void OnAnimationHit(int i)
    {

    }


    override public void OnAnimationEnd()
    {
        randCound = Random.Range(0, 3);
        if (randCound == 1)
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderEvadeBackwardOne);
        }
        else
        { 
            mAm.ChangeAction(EActionType.Walk);
        }
    }



    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ÎíÇÐÖ®»Ø¹â" || other.tag == "»êÑýµ¶_ÑªÓ£¼ÅÃð")
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderBeHitTwo);
        }
    }

}
