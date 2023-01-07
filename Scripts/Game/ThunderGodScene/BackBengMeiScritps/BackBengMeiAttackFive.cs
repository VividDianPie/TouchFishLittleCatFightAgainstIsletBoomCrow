using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiAttackFive : ActionState
{
    private bool currentAnimationAbleMove;
    private bool isCanAttack;
    Vector2 joyPos;
    public BackBengMeiAttackFive(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        isCanAttack = false;
        currentAnimationAbleMove = false;

        //避免重复代码
        EventManager.Instance.AddListener(EEventType.JoyStickDown, OnJoyStickDown);
        EventManager.Instance.AddListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        EventManager.Instance.AddListener(EEventType.JoyStickUp, OnJoyStickUp);
        EventManager.Instance.AddListener(EEventType.ScreenFingerZero, TouchReset);

    }



    override public void Enter()
    {

       
        mAt.Play("AttackFive");
        isCanAttack = false;
        currentAnimationAbleMove = false;
        int isPlay = Random.Range(0, 2);
        if (isPlay == 0)
        {
            //SoundMgr.Instance.PlayEffect("Sound/Effects/XiangLing/香菱_打扁咯");
        }


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
            if (Input.GetKeyDown(KeyCode.Q))
            {
                mAm.ChangeAction(EActionType.EvadeEndToAttack);
            }
            if (isCanAttack == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mAm.ChangeAction(EActionType.AttackOne);
                    BackBengMei.weaPonLeftHand.gameObject.SetActive(false);
                }
            }
            if (currentAnimationAbleMove == true)
            {
                float ph = Input.GetAxis("Horizontal");
                float pv = Input.GetAxis("Vertical");
                if (pv != 0.0f || ph != 0.0f)
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
            if (EasyTouchEvent.buttonQState == "QDown")
            {
                mAm.ChangeAction(EActionType.EvadeEndToAttack);
            }
            if (isCanAttack == true)
            {
                if (EasyTouchEvent.buttonAState == "ADown")
                {
                    mAm.ChangeAction(EActionType.AttackOne);
                    BackBengMei.weaPonLeftHand.gameObject.SetActive(false);
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
                else if (EasyTouchEvent.buttonEState == "EDown" || EasyTouchEvent.buttonEvadeState == "EvadePressed")
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
        ////避免重复代码
        //EventManager.Instance.DeleteListener(EEventType.JoyStickDown, OnJoyStickDown);
        //EventManager.Instance.DeleteListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        //EventManager.Instance.DeleteListener(EEventType.JoyStickUp, OnJoyStickUp);
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
        SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengAtk5");
        BackBengMei.weaPonLeftHand.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
        BackBengMei.weaPonRightHand.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
        TimerMgr.Instance.OneShot("BackBengMeiAttackFiveWeaPonRightHandUnActive", 0.6f, BackBengMeiAttackFiveWeaPonRightHandUnActive);
    }
    void BackBengMeiAttackFiveWeaPonRightHandUnActive()
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
        isCanAttack = true;
    }


    override public void AnimationEventTwo()
    {
        BackBengMei.weaPonLeftHand.gameObject.SetActive(false);
    }

    override public void AnimationEventThree()
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



