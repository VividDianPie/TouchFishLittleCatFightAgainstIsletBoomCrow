using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiRun : ActionState
{
    private Vector3 startFrontObjForward;
    private Vector3 laterObjForward;
    private float frontLaterTurnIncludeAngle;
    private float lerpTurn;
    //private Vector3 mVelocity;

    Vector2 joyPos;




    public BackBengMeiRun(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
        : base(type, am, master, pnt, cld)
    {
        //  mVelocity = Vector3.zero;

    }


    override public void Enter()
    {
        //避免重复代码
        EventManager.Instance.AddListener(EEventType.JoyStickDown, OnJoyStickDown);
        EventManager.Instance.AddListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        EventManager.Instance.AddListener(EEventType.JoyStickUp, OnJoyStickUp);
        EventManager.Instance.AddListener(EEventType.ScreenFingerZero, TouchReset);

        mAt.CrossFade("Run", 0.05f);
        //进入此状态时 剑跟随于后背

        //调整 坐标 与 方向
        BackBengMei.weaPonRightHand.transform.SetParent(mMaster.transform);
        BackBengMei.weaPonRightHand.localPosition = new Vector3(0.03670211f, 1.559347f, -0.2970055f);
        BackBengMei.weaPonRightHand.localRotation = Quaternion.Euler(-20.045f, 31.418f, 250.753f);




    }


    override public void Update()
    {
        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() > 0) { return; };
        float AxisX = Input.GetAxis("Horizontal");
        float AxisY = Input.GetAxis("Vertical");
        if (joyPos.x != 0.0f || joyPos.y != 0.0f)
        {
            AxisX = joyPos.x;
            AxisY = joyPos.y;

        }

        if (AxisX != 0.0f || AxisY != 0.0f)
        {
            //保存角色计算前的朝向
            startFrontObjForward = mRb.transform.forward;
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
            //获取角色计算之后的朝向
            laterObjForward = mRb.transform.forward;
            //计算前后朝向的角度差
            frontLaterTurnIncludeAngle = AngleSigned(startFrontObjForward, laterObjForward, Vector3.up);
            //角色朝向过渡旋转至计算后方向
            mRb.transform.forward = startFrontObjForward;

            //Pc端转速
            //  lerpTurn = Mathf.Lerp(0.0f, frontLaterTurnIncludeAngle, 0.07f);
            //Android 端 转速
            lerpTurn = Mathf.Lerp(0.0f, frontLaterTurnIncludeAngle, 0.28f);

            mRb.transform.Rotate(Vector3.up, lerpTurn);







            // Pc 端输入操作 
            if (Application.platform != RuntimePlatform.Android)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mAm.ChangeAction(EActionType.AttackOne);
                    return;
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    mAm.ChangeAction(EActionType.Jump);
                    return;
                }
                else if (Input.GetMouseButtonDown(1) &&
                       mMaster.GetComponent<BackBengMeiData>().backBengMeiEvadeForwardTwoCooling == false)
                {
                    mAm.ChangeAction(EActionType.EvadeForwardOne);
                    return;
                }
                else if (Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E))
                {
                    mAm.ChangeAction(EActionType.Parry);
                    return;
                }
            }



            // Android 端输入操作
            if (Application.platform == RuntimePlatform.Android)
            {
                if (EasyTouchEvent.buttonAState == "ADown")
                {
                    mAm.ChangeAction(EActionType.AttackOne);
                    return;
                }
                else if (EasyTouchEvent.buttonJumpState == "JumpDown")
                {
                    mAm.ChangeAction(EActionType.Jump);
                    return;
                }
                else if (EasyTouchEvent.buttonEvadeState == "EvadeDown" &&
                       mMaster.GetComponent<BackBengMeiData>().backBengMeiEvadeForwardTwoCooling == false)
                {
                    mAm.ChangeAction(EActionType.EvadeForwardOne);
                    return;
                }
                else if (EasyTouchEvent.buttonEState == "EDown" || EasyTouchEvent.buttonEState == "EPressed")
                {
                    mAm.ChangeAction(EActionType.Parry);
                    return;
                }
            }

            //射线检测
            RaycastHit hitinfo;
            if (Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, mMaster.transform.forward, out hitinfo, 0.5f) == false &&
                Mathf.Abs(frontLaterTurnIncludeAngle) < 50.0f)
            {
                mMaster.transform.position +=
                    mMaster.transform.forward * mMaster.GetComponent<BackBengMeiData>().backBengMeiRunSpeed * Time.unscaledDeltaTime;
            }
        }
        else
        {
            mAm.ChangeAction(EActionType.Idle);
        }
    }
    override public void Exit()
    {
        //避免重复代码
        EventManager.Instance.DeleteListener(EEventType.JoyStickDown, OnJoyStickDown);
        EventManager.Instance.DeleteListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        EventManager.Instance.DeleteListener(EEventType.JoyStickUp, OnJoyStickUp);
        //跑路状态退出时 剑持于手
        BackBengMei.weaPonRightHand.parent = BackBengMei.heroActor.gameObject.GetComponent<BackBengMei>().rightHand.transform;
        BackBengMei.weaPonRightHand.transform.localPosition =
            BackBengMei.heroActor.gameObject.GetComponent<BackBengMei>().rightHand.transform.localPosition;
        BackBengMei.weaPonRightHand.localPosition = new Vector3(0f, 0f, 0f);
        BackBengMei.weaPonRightHand.localRotation = Quaternion.Euler(0f, 0f, 0f);

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
        if (other.tag == "天殛之境_裁决")
        {
            if (HerrscherOfThunderMei.herrscherOfThunderMeiActor.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DoublieKnifeAttack") ||
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


    //获取向量之间的正负角度   其中n垂直与v1与v2形成的平面
    public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
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
