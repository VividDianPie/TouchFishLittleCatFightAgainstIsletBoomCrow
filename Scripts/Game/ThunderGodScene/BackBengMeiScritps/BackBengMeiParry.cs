using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiParry : ActionState
{
    bool isCanCounterAttack;
   // private Collider[] colliders;
    public BackBengMeiParry(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        isCanCounterAttack = false;
    }


    override public void Enter()
    {
        mAt.CrossFade("Parry", 0.01f);




        //joyPos = Vector2.zero;

        /**************************************************************************************************************************************************************************************************************/
        Collider[] colliders = Physics.OverlapSphere(mMaster.transform.position, 20);
        float maxDis = 19;
        Collider colliderObj = null;
        for (int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i].tag == "HerrscherOfThunderMei")
            {
                mMaster.transform.forward = (new Vector3(HerrscherOfThunderMei.herrscherOfThunderMeiActor.position.x, mMaster.transform.position.y,
                HerrscherOfThunderMei.herrscherOfThunderMeiActor.position.z) - mMaster.transform.position).normalized;
                return;
            }
            else if (colliders[i].tag == "QiuQiuPopple")
            {
                float tempDis = Vector3.Distance(mMaster.transform.position, colliders[i].transform.position);
                if (tempDis < maxDis)
                {
                    colliderObj = colliders[i];
                    maxDis = tempDis;
                }
            }
        }
        if (colliderObj != null)
        {
            mMaster.transform.forward = (new Vector3(colliderObj.transform.position.x, mMaster.transform.position.y, colliderObj.transform.position.z)
              - mMaster.transform.position).normalized;
        }
        /****************************************************************************************************************************************************************************/
    }


    override public void Update()
    {
        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() > 0) { return; };

        // Pc 端输入操作 
        if (Application.platform != RuntimePlatform.Android)
        {
            if (isCanCounterAttack == true && Input.GetKeyUp(KeyCode.E))
            {
                mAm.ChangeAction(EActionType.AttackFive);
                return;
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                mAm.ChangeAction(EActionType.Idle);
                return;
            }
        }

        // Android 端输入操作
        if (Application.platform == RuntimePlatform.Android)
        {
            if (isCanCounterAttack == true && EasyTouchEvent.buttonEState == "EUp")
            {
                mAm.ChangeAction(EActionType.AttackFive);
                return;
            }
            else if (EasyTouchEvent.buttonEState == "EUp")
            {
                mAm.ChangeAction(EActionType.Idle);
                return;
            }
        }


    }

    override public void Exit()
    {
        mAt.speed = 1;
        isCanCounterAttack = false;
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



    override public void AnimationEventThree()
    {
        mAt.speed = 0;
    }

    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "天殛之境_裁决")
        {
            //判断双方正面朝向角度
            if (Vector3.Angle(HerrscherOfThunderMei.herrscherOfThunderMeiActor.forward, mMaster.transform.forward) >= 145)
            {
                SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengMeiParry");
                isCanCounterAttack = true;
                TimerMgr.Instance.OneShot("BackBengMeiNoCounterAttack", 0.7f, BackBengMeiNoCounterAttack);
            }
            else if (HerrscherOfThunderMei.herrscherOfThunderMeiActor.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DoublieKnifeAttack") ||
               HerrscherOfThunderMei.herrscherOfThunderMeiActor.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AtkFive"))
            {
                mAm.ChangeAction(EActionType.BeHitRetreat);
            }
            else
            {
                if (Random.Range(0, 1) == 1)
                {
                    mAm.ChangeAction(EActionType.BeHitOne);
                }
                else
                {
                    mAm.ChangeAction(EActionType.BeHitTwo);
                }
            }
        }
        else if (other.tag == "QQRWeapon")
        {
            SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengMeiParry");
            isCanCounterAttack = true;
            TimerMgr.Instance.OneShot("BackBengMeiNoCounterAttack", 0.7f, BackBengMeiNoCounterAttack);
        }
    }
    void BackBengMeiNoCounterAttack()
    {
        isCanCounterAttack = false;
    }
    //获取向量之间的正负角度   其中n垂直与v1与v2形成的平面
    public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }
}

