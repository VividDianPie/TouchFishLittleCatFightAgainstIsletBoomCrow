using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiEvadeEndToAttack : ActionState
{
    private bool CurrentAnimationAbleMove;
    Vector2 joyPos;
    public BackBengMeiEvadeEndToAttack(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {

    }

    override public void Enter()
    {
        //避免重复代码
        EventManager.Instance.AddListener(EEventType.JoyStickDown, OnJoyStickDown);
        EventManager.Instance.AddListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        EventManager.Instance.AddListener(EEventType.JoyStickUp, OnJoyStickUp);
        EventManager.Instance.AddListener(EEventType.ScreenFingerZero, TouchReset);

        mAt.Play("EvadeEndToAttack");
        CurrentAnimationAbleMove = false;
        joyPos = Vector2.zero;

        SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengMeiEvadeEndToAttack");

        //避免重复代码
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
        // Pc 端输入操作 
        if (Application.platform != RuntimePlatform.Android)
        {
            if (CurrentAnimationAbleMove == true)
            {
                float AxisX = Input.GetAxis("Horizontal");
                float AxisY = Input.GetAxis("Vertical");
                if (joyPos.x != 0.0f || joyPos.y != 0.0f)
                {
                    AxisX = joyPos.x;
                    AxisY = joyPos.y;
                }
                if (AxisX != 0.0f || AxisY != 0.0f)
                {
                    mAm.ChangeAction(EActionType.Run);
                    return;
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    mAm.ChangeAction(EActionType.AttackOne);
                    return;
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    mAm.ChangeAction(EActionType.EvadeBackwardOne);
                    return;
                }
                else if (Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E))
                {
                    mAm.ChangeAction(EActionType.Parry);
                    return;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    mAm.ChangeAction(EActionType.Jump);
                    return;
                }
            }
        }

        // Android 端输入操作
        if (Application.platform == RuntimePlatform.Android)
        {
            if (CurrentAnimationAbleMove == true)
            {
                float AxisX = Input.GetAxis("Horizontal");
                float AxisY = Input.GetAxis("Vertical");
                if (joyPos.x != 0.0f || joyPos.y != 0.0f)
                {
                    AxisX = joyPos.x;
                    AxisY = joyPos.y;
                }
                if (AxisX != 0.0f || AxisY != 0.0f)
                {
                    mAm.ChangeAction(EActionType.Run);
                    return;
                }
                else if (EasyTouchEvent.buttonAState == "ADown")
                {
                    mAm.ChangeAction(EActionType.AttackOne);
                    return;
                }
                else if (EasyTouchEvent.buttonEvadeState == "EvadeDown")
                {
                    mAm.ChangeAction(EActionType.EvadeBackwardOne);
                    return;
                }
                else if (EasyTouchEvent.buttonEState == "EDown" || EasyTouchEvent.buttonEState == "EPressed")
                {
                    mAm.ChangeAction(EActionType.Parry);
                    return;
                }
                else if (EasyTouchEvent.buttonJumpState == "JumpDown")
                {
                    mAm.ChangeAction(EActionType.Jump);
                    return;
                }
            }
        }
    }

    override public void Exit()
    {
        //避免重复代码
        EventManager.Instance.DeleteListener(EEventType.JoyStickDown, OnJoyStickDown);
        EventManager.Instance.DeleteListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        EventManager.Instance.DeleteListener(EEventType.JoyStickUp, OnJoyStickUp);
        if (BackBengMei.weaPonLeftHand.gameObject == true)
        {
            BackBengMei.weaPonLeftHand.gameObject.SetActive(false);
        }
    }

    override public void OnAnimationStart()
    {
        BackBengMei.weaPonLeftHand.gameObject.SetActive(true);
    }



    override public void OnAnimationHit(int i)
    {
        BackBengMei.weaPonLeftHand.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
        BackBengMei.weaPonRightHand.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
        TimerMgr.Instance.OneShot("BackBengMeiEvadeEndToAttackWeaPonRightHandUnActive",
            0.25f, BackBengMeiEvadeEndToAttackWeaPonRightHandUnActive);

        
    }
    void BackBengMeiEvadeEndToAttackWeaPonRightHandUnActive()
    {
        BackBengMei.weaPonLeftHand.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
        BackBengMei.weaPonRightHand.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
    }


    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.Idle);
    }

    override public void AnimationEventOne()
    {
        CurrentAnimationAbleMove = true;
    }


    override public void AnimationEventTwo()
    {
        BackBengMei.weaPonLeftHand.gameObject.SetActive(false);
    }

    //override public void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "天殛之境_裁决")
    //    {
    //        if (HerrscherOfThunderMei.herrscherOfThunderMeiActor.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DoublieKnifeAttack") ||
    //            HerrscherOfThunderMei.herrscherOfThunderMeiActor.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("AtkFive"))
    //        {

    //            mAm.ChangeAction(EActionType.BeHitRetreat);
    //        }
    //        else
    //        {
    //            if (Random.Range(0, 1) == 1)
    //            {
    //                mAm.ChangeAction(EActionType.BeHitOne);
    //            }
    //            else
    //            {
    //                mAm.ChangeAction(EActionType.BeHitTwo);
    //            }
    //        }
    //    }
    //}

    public void OnJoyStickDown(MyEvent evt)
    {
        MyVec2 pos = evt.data as MyVec2;
        if (pos != null)
        {
            joyPos.x = pos.x;
            joyPos.y = pos.y;
        }
    }


    public void OnJoyStickDrag(MyEvent evt)
    {
        MyVec2 pos = evt.data as MyVec2;
        if (pos != null)
        {
            joyPos.x = pos.x;
            joyPos.y = pos.y;
        }
    }

    public void OnJoyStickUp(MyEvent evt)
    {
        MyVec2 pos = evt.data as MyVec2;
        joyPos.x = pos.x;
        joyPos.y = pos.y;
    }
    public void TouchReset(MyEvent evt)
    {
        joyPos = Vector2.zero;
    }
}
