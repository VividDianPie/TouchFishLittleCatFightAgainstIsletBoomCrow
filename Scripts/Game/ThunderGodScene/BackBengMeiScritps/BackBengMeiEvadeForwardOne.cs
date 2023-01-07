using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiEvadeForwardOne : ActionState
{
    private bool currentAnimationAbleMove;
    private bool isEvadeToAttack;
    private bool isCanRun;
    float startSpeed;
    float gravityAddSpeed;
    RaycastHit hitinfo;
    bool isCollider;
    Vector2 joyPos;
    public BackBengMeiEvadeForwardOne(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        currentAnimationAbleMove = false;
        isEvadeToAttack = false;
        isCanRun = false;
        //避免重复代码
        EventManager.Instance.AddListener(EEventType.JoyStickDown, OnJoyStickDown);
        EventManager.Instance.AddListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        EventManager.Instance.AddListener(EEventType.JoyStickUp, OnJoyStickUp);
        EventManager.Instance.AddListener(EEventType.ScreenFingerZero, TouchReset);

    }

    override public void Enter()
    {
      
        startSpeed = 0;
        gravityAddSpeed = -10;
        currentAnimationAbleMove = false;
        isEvadeToAttack = false;
        isCanRun = false;
        joyPos = Vector2.zero;
        mAt.Play("EvadeForwardOne");
        //放大防御矩形
        mMaster.GetComponent<CapsuleCollider>().radius = 0.5f;

        float AxisX = Input.GetAxis("Horizontal");
        float AxisY = Input.GetAxis("Vertical");
        if (joyPos.x != 0.0f || joyPos.y != 0.0f)
        {
            AxisX = joyPos.x;
            AxisY = joyPos.y;
        }
        if (AxisX != 0.0f || AxisY != 0.0f)
        {
            //角色朝向为输入轴朝向
            mRb.transform.forward = (new Vector3(AxisX, 0f, AxisY)).normalized;
            //获取相机世界平面朝向
            Vector3 CamForward = Camera.main.transform.forward;
            CamForward.y = 0;
            //获取相机平面朝向与世界正前方的向的夹角度
            float angle = Vector3.Angle(CamForward, Vector3.forward);
            //判断相机x轴朝向象限
            if (CamForward.x < 0)
            {
                angle = -angle;
            }
            //角色旋转至正确的位置
            mRb.transform.Rotate(Vector3.up, angle);
        }
        SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengMeiEvadeForword");
    }


    override public void Update()
    {
        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() > 0) { return; };

        // Pc 端输入操作 
        if (Application.platform != RuntimePlatform.Android)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isEvadeToAttack = true;
            }
            if (currentAnimationAbleMove == true)
            {
                if (isEvadeToAttack == true)
                {
                    mAm.ChangeAction(EActionType.EvadeEndToAttack);
                    return;
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    mAm.ChangeAction(EActionType.EvadeForwardTwo);
                }
            }
            if (isCanRun == true)
            {
                float AxisX = Input.GetAxis("Horizontal");
                float AxisY = Input.GetAxis("Vertical");
                if (AxisX != 0.0f || AxisY != 0.0f)
                {
                    mAm.ChangeAction(EActionType.Run);
                    return;
                }
            }
        }

        // Android 端输入操作
        if (Application.platform == RuntimePlatform.Android)
        {
            if (EasyTouchEvent.buttonAState== "ADown")
        {
            isEvadeToAttack = true;
        }
        if (currentAnimationAbleMove == true)
        {
            if (isEvadeToAttack == true)
            {
                mAm.ChangeAction(EActionType.EvadeEndToAttack);
                return;
            }
            else if (EasyTouchEvent.buttonEvadeState== "EvadeDown")
            {
                mAm.ChangeAction(EActionType.EvadeForwardTwo);
            }
        }
            if (isCanRun == true)
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
            }
        }



        if (Physics.Raycast(mMaster.transform.position, -Vector3.up, 0.1f) == false)
        {
            // 路程 = 初速度 * 时间 + 加速度 * 时间平方 / 2
            float _t = startSpeed * Time.unscaledDeltaTime + gravityAddSpeed * Time.unscaledDeltaTime * Time.unscaledDeltaTime / 2;
            startSpeed += gravityAddSpeed * Time.unscaledDeltaTime;
            mMaster.transform.position += Vector3.up * _t;
        }

        //射线检测
        isCollider = Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, mMaster.transform.forward, out hitinfo, 0.5f);
        if (isCollider == false && currentAnimationAbleMove == false)
        {
            mMaster.transform.position +=
                mMaster.transform.forward * mMaster.GetComponent<BackBengMeiData>().backBengMeiEvadeForwardSpeed * Time.unscaledDeltaTime;
        }

    }

    override public void Exit()
    {
      // //避免重复代码
      // EventManager.Instance.DeleteListener(EEventType.JoyStickDown, OnJoyStickDown);
      // EventManager.Instance.DeleteListener(EEventType.JoyStickDrag, OnJoyStickDrag);
      // EventManager.Instance.DeleteListener(EEventType.JoyStickUp, OnJoyStickUp);
        mMaster.GetComponent<CapsuleCollider>().radius = 0.25f;
    }



    override public void OnAnimationStart()
    {

    }



    override public void OnAnimationHit(int i)
    {
        if (isCollider == true && hitinfo.collider.tag == "Map")
        {   
            AreaManger.Instance.HitTerrain(hitinfo.point + mMaster.transform.forward * 0.1f);
        }
    }



    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.Idle);
    }

    override public void AnimationEventOne()
    {
        currentAnimationAbleMove = true;
    }

    override public void AnimationEventTwo()
    {
        isCanRun = true;
    }


    override public void OnTriggerEnter(Collider other)
    {
        if (mMaster.GetComponent<BackBengMeiData>().backBengMeiTimeScaleIsColling == false &&
            other.tag == "天殛之境_裁决" /*|| other.tag == "QQRWeapon"*/)
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
