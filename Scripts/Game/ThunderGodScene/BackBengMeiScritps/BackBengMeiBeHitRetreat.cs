using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiBeHitRetreat : ActionState
{
    SkinnedMeshRenderer facialExpression;

    public BackBengMeiBeHitRetreat(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
        : base(type, am, master, pnt, cld)
    {
        facialExpression = mMaster.transform.Find("Body").transform.GetComponent<SkinnedMeshRenderer>();
    }


    override public void Enter()
    {
        mMaster.transform.forward = (new Vector3(HerrscherOfThunderMei.herrscherOfThunderMeiActor.position.x, mMaster.transform.position.y, 
            HerrscherOfThunderMei.herrscherOfThunderMeiActor.position.z) - mMaster.transform.position).normalized;
        mAt.Play("BeHitRetreat");
        facialExpression.SetBlendShapeWeight(62, 100);
    }

    
    override public void Update()
    {


    }

    public override void Exit()
    {
        facialExpression.SetBlendShapeWeight(62, 0); ;
    }


    override public void OnAnimationStart()
    {

    }



    override public void OnAnimationHit(int i)
    {

    }



    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.Run);
    }

}

