using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiIdle : ActionState
{
    Vector2 joyPos;
    public BackBengMeiIdle(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
        : base(type, am, master, pnt, cld)
    {
        //避免重复代码
        EventManager.Instance.AddListener(EEventType.JoyStickDown, OnJoyStickDown);
        EventManager.Instance.AddListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        EventManager.Instance.AddListener(EEventType.JoyStickUp, OnJoyStickUp);
        EventManager.Instance.AddListener(EEventType.ScreenFingerZero, TouchReset);
    }


    override public void Enter()
    {



        mAt.CrossFade("Idle", 0.05f);
        // mAt.Play("Idle");

        joyPos = Vector2.zero;

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


        // Pc 端输入操作
        if (Application.platform != RuntimePlatform.Android)
        {
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

        // Android 端输入操作
        if (Application.platform == RuntimePlatform.Android)
        {
            if (AxisX < -0.1 || AxisX > 0.1 || AxisY < -0.1 || AxisY > 0.1)
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

    override public void Exit()
    {
        //  //避免重复代码
        //  EventManager.Instance.DeleteListener(EEventType.JoyStickDown, OnJoyStickDown);
        //  EventManager.Instance.DeleteListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        //  EventManager.Instance.DeleteListener(EEventType.JoyStickUp, OnJoyStickUp);
        joyPos = Vector2.zero;

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



    override public void OnTriggerEnter(Collider other)
    {
        //避免重复代码
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







    public void OnJoyStickDown(MyEvent evt)
    {
        MyVec2 pos = evt.data as MyVec2;
        if (pos != null)
        {
            //Debug.Log("Down "+"Pos.x = "+pos.x+"     Pos.y = "+pos.y);
            joyPos.x = pos.x;
            joyPos.y = pos.y;
        }
    }


    public void OnJoyStickDrag(MyEvent evt)
    {
        MyVec2 pos = evt.data as MyVec2;
        if (pos != null)
        {
            // Debug.Log("Drag " + "Pos.x = " + pos.x + "     Pos.y = " + pos.y);
            joyPos.x = pos.x;
            joyPos.y = pos.y;
        }
    }

    public void OnJoyStickUp(MyEvent evt)
    {
        MyVec2 pos = evt.data as MyVec2;
        // Debug.Log("Drag " + "Pos.x = " + pos.x + "     Pos.y = " + pos.y);
        joyPos.x = pos.x;
        joyPos.y = pos.y;
    }
    public void TouchReset(MyEvent evt)
    {
        joyPos = Vector2.zero;
    }
}
