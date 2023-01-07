using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiJumpTurnSword : ActionState
{
    bool canMove;
    bool isCombo;
    RaycastHit hitinfo;
    Vector2 joyPos;
    public BackBengMeiJumpTurnSword(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
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

        mAt.CrossFade("JumpTurnSword", 0.05f);
        // mAt.Play("Idle");
        canMove = false;
        isCombo = false;
        joyPos = Vector2.zero;

        // mRb.velocity = mRb.velocity +
        //new Vector3(0f, mMaster.GetComponent<BackBengMeiData>().backBengMeiJumpTurnSwordJump, 0f);

    }


    override public void Update()
    {
        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() > 0) { return; };

        // Pc 端输入操作 
        if (Application.platform != RuntimePlatform.Android)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //旋转追击在动画事件One中进行追击
                isCombo = true;
            }
            if (canMove == true)
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
            else if ((Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, mMaster.transform.forward, out hitinfo, 0.5f) == false))
            {
                mMaster.transform.position += mMaster.transform.forward *
                mMaster.GetComponent<BackBengMeiData>().backBengMeiJumpTurnSwordFword * Time.unscaledDeltaTime;
            }
        }


        // Android 端输入操作
        if (Application.platform == RuntimePlatform.Android)
        {
            if (EasyTouchEvent.buttonAState == "ADown")
            {
                //旋转追击在动画事件One中进行追击
                isCombo = true;
            }
            if (canMove == true)
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
            if ((Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, mMaster.transform.forward, out hitinfo, 0.5f) == false))
            {
                mMaster.transform.position += mMaster.transform.forward *
                mMaster.GetComponent<BackBengMeiData>().backBengMeiJumpTurnSwordFword * Time.unscaledDeltaTime;
            }
        }
    }
    override public void Exit()
    {
        ////避免重复代码
        //EventManager.Instance.DeleteListener(EEventType.JoyStickDown, OnJoyStickDown);
        //EventManager.Instance.DeleteListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        //EventManager.Instance.DeleteListener(EEventType.JoyStickUp, OnJoyStickUp);
        //BackBengMei.weaPonLeftHand.gameObject.SetActive(false);

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
        SoundMgr.Instance.PlayEffect("Sound/Effects/JumpTurnSword");
        BackBengMei.weaPonRightHand.GetChild(0).GetComponent<CapsuleCollider>().enabled = true;
        TimerMgr.Instance.OneShot("BackBengMeiWeaPonRightUnActive", 0.8f,
            BackBengMeiWeaPonRightUnActive);
    }
    void BackBengMeiWeaPonRightUnActive()
    {
        BackBengMei.weaPonRightHand.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
    }


    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.Idle);
    }

    override public void AnimationEventOne()
    {
        BackBengMei.weaPonLeftHand.gameObject.SetActive(false);
        canMove = true;
        if (isCombo == true)
        {
            mAm.ChangeAction(EActionType.AttackFive);
        }
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
