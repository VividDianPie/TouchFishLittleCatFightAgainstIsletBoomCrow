using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiAttackFour : ActionState
{
    private bool whetherTheCombo;
    private bool isFollowUpAttackOpen;
    private bool currentAnimationAbleMove;
    Vector2 joyPos;
    public BackBengMeiAttackFour(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        whetherTheCombo = false;
        isFollowUpAttackOpen = false;
        currentAnimationAbleMove = false;

        //避免重复代码
        EventManager.Instance.AddListener(EEventType.JoyStickDown, OnJoyStickDown);
        EventManager.Instance.AddListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        EventManager.Instance.AddListener(EEventType.JoyStickUp, OnJoyStickUp);
        EventManager.Instance.AddListener(EEventType.ScreenFingerZero, TouchReset);

    }

    override public void Enter()
    {
      
        mAt.Play("AttackFour");
        whetherTheCombo = false;
        isFollowUpAttackOpen = false;
        currentAnimationAbleMove = false;
        SoundMgr.Instance.RandomPlayXianglingAAttackSound();


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
    }


    override public void Update()
    {
        // Pc 端输入操作 
        if (Application.platform != RuntimePlatform.Android)
        {
            if (Input.GetMouseButtonDown(0))
            {
                whetherTheCombo = true;
            }

            if (isFollowUpAttackOpen == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mAm.ChangeAction(EActionType.AttackFive);
                    return;
                }
                if ((Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f) && Input.GetMouseButtonDown(1))
                {
                    mAm.ChangeAction(EActionType.EvadeForwardOne);
                    return;
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    mAm.ChangeAction(EActionType.EvadeBackwardOne);
                    return;
                }
            }
            if (currentAnimationAbleMove == true)
            {
                float AxisX = Input.GetAxis("Horizontal");
                float AxisY = Input.GetAxis("Vertical");
                if (joyPos.x != 0.0f || joyPos.y != 0.0f)
                {
                    AxisX = joyPos.x;
                    AxisY = joyPos.y;
                }
                if (AxisY != 0.0f || AxisX != 0.0f)
                {
                    mAm.ChangeAction(EActionType.Run);
                    return;
                }
            }
        }

        // Android 端输入操作
        if (Application.platform == RuntimePlatform.Android)
        {
            if (EasyTouchEvent.buttonAState == "ADown")
            {
                whetherTheCombo = true;
            }

            if (isFollowUpAttackOpen == true)
            {
                if (EasyTouchEvent.buttonAState == "ADown")
                {
                    mAm.ChangeAction(EActionType.AttackFive);
                    return;
                }
                float AxisX = Input.GetAxis("Horizontal");
                float AxisY = Input.GetAxis("Vertical");
                if (joyPos.x != 0.0f || joyPos.y != 0.0f)
                {
                    AxisX = joyPos.x;
                    AxisY = joyPos.y;
                }
                if ((AxisY != 0.0f || AxisX != 0.0f) && EasyTouchEvent.buttonEvadeState == "EvadeDown")
                {
                    mAm.ChangeAction(EActionType.EvadeForwardOne);
                    return;
                }
                else if (EasyTouchEvent.buttonEvadeState == "EvadeDown")
                {
                    mAm.ChangeAction(EActionType.EvadeBackwardOne);
                    return;
                }
            }
            if (currentAnimationAbleMove == true)
            {
                float AxisX = Input.GetAxis("Horizontal");
                float AxisY = Input.GetAxis("Vertical");
                if (joyPos.x != 0.0f || joyPos.y != 0.0f)
                {
                    AxisX = joyPos.x;
                    AxisY = joyPos.y;
                }
                if (AxisY != 0.0f || AxisX != 0.0f)
                {
                    mAm.ChangeAction(EActionType.Run);
                    return;
                }
            }
        }

    }

    override public void Exit()
    {
      //  //避免重复代码
      //  EventManager.Instance.DeleteListener(EEventType.JoyStickDown, OnJoyStickDown);
      //  EventManager.Instance.DeleteListener(EEventType.JoyStickDrag, OnJoyStickDrag);
      //  EventManager.Instance.DeleteListener(EEventType.JoyStickUp, OnJoyStickUp);
    }


    override public void OnAnimationStart()
    {

    }



    override public void OnAnimationHit(int i)
    {
        SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengAtk4");
        BackBengMei.weaPonRightHand.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
        TimerMgr.Instance.OneShot("BackBengMeiAttackFourWeaPonRightHandUnActive", 0.6f, BackBengMeiAttackFourWeaPonRightHandUnActive);
    }
    void BackBengMeiAttackFourWeaPonRightHandUnActive()
    {
        BackBengMei.weaPonRightHand.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
    }


    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.Idle);
    }


    override public void AnimationEventOne()
    {
        if (whetherTheCombo == true)
        {
            mAm.ChangeAction(EActionType.AttackFive);
            return;
        }
        isFollowUpAttackOpen = true;
    }

    override public void AnimationEventTwo()
    {
        currentAnimationAbleMove = true;
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
