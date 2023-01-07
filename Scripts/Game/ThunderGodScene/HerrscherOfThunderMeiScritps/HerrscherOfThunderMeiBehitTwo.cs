using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderMeiBehitTwo : ActionState
{
    int randCound;
    SkinnedMeshRenderer facialExpression;

    public HerrscherOfThunderMeiBehitTwo(
        EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        facialExpression = mMaster.transform.Find("Body").transform.GetComponent<SkinnedMeshRenderer>();

    }

    override public void Enter()
    {
        mAt.Play("BeHitTwo");
        facialExpression.SetBlendShapeWeight(21, 100);

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
        mAm.ChangeAction(EActionType.Walk);
    }

    override public void AnimationEventOne()
    {
        //�����ܻ���Ч
    }

    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "����֮�ع�" || other.tag == "������_Ѫӣ����")
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderBeHitOne);
        }
    }


}
