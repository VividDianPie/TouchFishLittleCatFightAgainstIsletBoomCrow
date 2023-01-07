using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : ActionState
{

    int randCound;
    Vector3 meiForwardDir;
    RaycastHit hitinfo;
    int signed;
    string IsBeHit;
    float randCoundTime;
    public Walk(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        signed = 1;
        randCound = -1;
        IsBeHit = "NotBeHit";
        randCoundTime = 0;
    }

    override public void Enter()
    {
        mAt.CrossFade("Walk", 0.05f);
        TimerMgr.Instance.Repeated("DirCtrl", 20, DirCtrl);
        randCound = -1;

    }


    override public void Update()
    {
        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() > 0) { return; };

        if (IsBeHit == "alreadyBeHit") 
        {
            if ((randCoundTime += Time.deltaTime) > 0.05f)
            {
                randCoundTime = 0;
                randCound = Random.Range(0, 100);
            }
        }
        if (randCound == 0)
        {
            //雷律招架
            mAm.ChangeAction(EActionType.HerrscherOfThunderWeaPonTurn);
            return;
        }
        if (randCound == 1)
        {
            mAm.ChangeAction(EActionType.SkillOneLandThorn);
            return;
        }
        if (randCound == 2)
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderMeiSkillTwoSwordTrack);
            return;
        }
        if (randCound == 3 || randCound == 8 || randCound == 9)
        {
            //闪避追击
            mAm.ChangeAction(EActionType.HerrscherOfThunderEvadeForwardOne);
            return;
        }
        if (randCound == 4 || randCound == 6 &&
            (BackBengMei.heroActor.position - mMaster.transform.position).sqrMagnitude >= 1 &&
             (BackBengMei.heroActor.position - mMaster.transform.position).sqrMagnitude <= 3)
        {
            //雷律技能其三
            mAm.ChangeAction(EActionType.SkillThreeJumpTurnSword);
            return;
        }
        if (randCound == 5)
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderEvadeBackwardOne);
            return;
        }

        if (Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, mMaster.transform.forward, out hitinfo, 0.5f) == false)
        {
            mMaster.transform.position += mMaster.transform.forward *
                mMaster.GetComponent<HerrscherOfThunderMeiData>().WalkSpeed * Time.deltaTime;
        }
        else if (Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, mMaster.transform.forward, out hitinfo, 0.5f) == true &&
         hitinfo.collider.gameObject.tag == "BackBengMei")
        {
            if ((randCoundTime += Time.deltaTime) > 0.01f)
            {
                Debug.Log("Test");
                randCoundTime = 0;
                randCound = Random.Range(100, 110);
            }

            if (randCound==101|| randCound == 102)
            {
                mAm.ChangeAction(EActionType.HerrscherOfThunderAtkOne);
                return;
            }
            else if (randCound == 103)
            {
                mAm.ChangeAction(EActionType.HerrscherOfThunderEvadeForwardOne);
                return;
            }
            else if (randCound == 104)
            {
                mAm.ChangeAction(EActionType.SkillOneLandThorn);
                return;
            }
            else if (randCound == 105)
            {
                mAm.ChangeAction(EActionType.SkillThreeJumpTurnSword);
                return;
            }
            else if (randCound == 106)
            {
                mAm.ChangeAction(EActionType.HerrscherOfThunderEvadeBackwardOne);
                return;
            }
        }






        if (mMaster.GetComponent<HerrscherOfThunderMeiData>().walkState == "LeftRightWalk")
        {
            //雷之律者左右徘徊
         //   Debug.Log("雷之律者左右徘徊");
            float meiIncludeAngle0 = Vector3.Angle(mMaster.transform.forward,
                 (BackBengMei.heroActor.position - mMaster.transform.position).normalized);
            float lerpTurn0 = Mathf.Lerp(0.0f, meiIncludeAngle0, Time.deltaTime/2);
            mRb.transform.Rotate(Vector3.up, signed * lerpTurn0);
        }
        else if (mMaster.GetComponent<HerrscherOfThunderMeiData>().walkState == "BackWalk")
        {
            //雷之律者转身后走
          //  Debug.Log("雷之律者转身后走");

            float meiIncludeAngle1 = AngleSigned(mMaster.transform.forward,
               (BackBengMei.heroActor.position - mMaster.transform.position).normalized, Vector3.up);
            float lerpTurn1 = Mathf.Lerp(0.0f, -meiIncludeAngle1, 0.002f);
            mRb.transform.Rotate(Vector3.up, lerpTurn1);
        }
        else if (mMaster.GetComponent<HerrscherOfThunderMeiData>().walkState == "ForwardWalk")
        {
            //雷之律者经直走来
           // Debug.Log("雷之律者经直走来");

            float meiIncludeAngle2 = AngleSigned(mMaster.transform.forward,
                (BackBengMei.heroActor.position - mMaster.transform.position).normalized, Vector3.up);
            float lerpTurn2 = Mathf.Lerp(0.0f, meiIncludeAngle2, 0.06f);
            mRb.transform.Rotate(Vector3.up, lerpTurn2);
        }
        else { Debug.LogError("HerrscherOfThunderMeiWalkError"); }







    }

    void DirCtrl()
    {
        signed = -signed;
        randCound = Random.Range(110, 120);
        if (randCound == 111|| randCound==113)
        {
            mMaster.GetComponent<HerrscherOfThunderMeiData>().walkState = "BackWalk";
        }
        else if (randCound == 112)
        {
            mMaster.GetComponent<HerrscherOfThunderMeiData>().walkState = "ForwardWalk";
        }
        else
        {
            mMaster.GetComponent<HerrscherOfThunderMeiData>().walkState = "LeftRightWalk";
        }
    }


    override public void Exit()
    {
        TimerMgr.Instance.DeleteTimer("MeiForwardDir");
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
    override public void AnimationEventOne()
    {
        SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengMeiStepLeft");
    }

    override public void AnimationEventTwo()
    {
        SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengMeiStepLeft");
    }

    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "雾切之回光" || other.tag == "魂妖刀_血樱寂灭")
        {
            mAm.ChangeAction(EActionType.HerrscherOfThunderBeHitOne);
            IsBeHit = "alreadyBeHit";
        }
    }

    //获取向量之间的正负角度   其中n垂直与v1与v2形成的平面
    public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }

}

