using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderMeiIdle : ActionState
{
    int randCound;
    public HerrscherOfThunderMeiIdle(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        randCound = 0;
    }

    override public void Enter()
    {
        mAt.CrossFade("Idle", 0.05f);
    }


    override public void Update()
    {
        //randCound = Random.Range(0, 500);
        //if (randCound == 0)
        //{
        //    //�����м�
        //    mAm.ChangeAction(EActionType.HerrscherOfThunderWeaPonTurn);
        //}
        //else if (randCound == 1)
        //{
        //    mAm.ChangeAction(EActionType.SkillOneLandThorn);
        //}
        //else if (randCound == 2)
        //{
        //    //������һ
        //    mAm.ChangeAction(EActionType.HerrscherOfThunderAtkOne);
        //}
        //else if (randCound == 3)
        //{
        //    mAm.ChangeAction(EActionType.HerrscherOfThunderMeiSkillTwoSwordTrack);
        //}
        //else if (randCound == 4)
        //{
        //    //����׷��
        //    mAm.ChangeAction(EActionType.HerrscherOfThunderEvadeForwardOne);
        //}
        //else if (randCound == 5)
        //{
        //    //���ɼ�������
        //    mAm.ChangeAction(EActionType.SkillThreeJumpTurnSword);
        //}
        //else if (randCound == 6 || randCound <= 20)
        //{ 
        //    mAm.ChangeAction(EActionType.HerrscherOfThunderEvadeBackwardOne);
        //}
        Debug.Log("AnimationIsIdle");
        mAm.ChangeAction(EActionType.Walk);


    }

    override public void Exit()
    {


    }


    override public void OnAnimationStart()
    {
    }


    override public void OnAnimationHit(int i)
    {

    }


    override public void OnAnimationEnd()
    {
    }


    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "����֮�ع�" || other.tag == "������_Ѫӣ����")
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderBeHitOne);
        }
    }


}
