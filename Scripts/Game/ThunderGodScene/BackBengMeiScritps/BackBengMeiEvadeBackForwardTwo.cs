using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiEvadeBackForwardTwo : ActionState
{
    private bool currentAnimationAbleMove;
    float mouseLongHitTime;
    RaycastHit hitinfo;
    Vector2 joyPos;
    public BackBengMeiEvadeBackForwardTwo(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
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

        mAt.Play("EvadeBackwardTwo");
        currentAnimationAbleMove = false;
        mouseLongHitTime = 0;
        joyPos = Vector2.zero;

        SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengMeiEvadeBackForword");
        mMaster.GetComponent<CapsuleCollider>().radius = 0.5f;
    }


    override public void Update()
    {
        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() > 0) { return; };

        // Pc 端输入操作 
        if (Application.platform != RuntimePlatform.Android)
        {
            if (currentAnimationAbleMove == true)
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
                if (Input.GetMouseButton(0))
                {
                    if ((mouseLongHitTime += Time.unscaledDeltaTime) >= mMaster.GetComponent<BackBengMeiData>().JumpTurnSwordMousLongHitTime)
                    {
                        mAm.ChangeAction(EActionType.JumpTurnSword);
                    }
                }
            }
        }

        // Android 端输入操作
        if (Application.platform == RuntimePlatform.Android)
        {
            if (currentAnimationAbleMove == true)
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
                if (EasyTouchEvent.buttonAState == "APressed")
                {
                    if ((mouseLongHitTime += Time.unscaledDeltaTime) >= mMaster.GetComponent<BackBengMeiData>().JumpTurnSwordMousLongHitTime)
                    {
                        mAm.ChangeAction(EActionType.JumpTurnSword);
                    }
                }
            }
        }


        //射线检测
        bool isCollider = Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, -mMaster.transform.forward, out hitinfo, 0.5f);
        if (isCollider == false && currentAnimationAbleMove == false)
        {
            mMaster.transform.position +=
                -mMaster.transform.forward * mMaster.GetComponent<BackBengMeiData>().backBengMeiEvadeBackForwardSpeed * Time.unscaledDeltaTime;
        }


    }
    override public void Exit()
    {
        //避免重复代码
        EventManager.Instance.DeleteListener(EEventType.JoyStickDown, OnJoyStickDown);
        EventManager.Instance.DeleteListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        EventManager.Instance.DeleteListener(EEventType.JoyStickUp, OnJoyStickUp);
        mMaster.GetComponent<CapsuleCollider>().radius = 0.25f;
    }


    override public void OnAnimationStart()
    {

    }



    override public void OnAnimationHit(int i)
    {
    }



    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.Idle);
    }

    override public void AnimationEventOne()
    {
        currentAnimationAbleMove = true;
    }

    override public void OnTriggerEnter(Collider other)
    {
        if (mMaster.GetComponent<BackBengMeiData>().backBengMeiTimeScaleIsColling == false &&
            other.tag == "天殛之境_裁决"/*||other.tag == "QQRWeapon"*/)
        {
            SoundMgr.Instance.PlayEffect("Sound/Effects/时空断裂");
            Time.timeScale = 0.1f;
            TimerMgr.Instance.OneShot("TimeScaleReSet", 0.5f, TimeScaleReSet);
            mMaster.GetComponent<BackBengMeiData>().backBengMeiTimeScaleIsColling = true;
            TimerMgr.Instance.OneShot("TimeScaleIsCollingIsFalse", 0.9f, TimeScaleIsCollingIsFalse);
        }
    }

    void TimeScaleReSet()
    {
        Time.timeScale = 1;
    }
    void TimeScaleIsCollingIsFalse()
    {
        mMaster.GetComponent<BackBengMeiData>().backBengMeiTimeScaleIsColling = false;
    }
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
