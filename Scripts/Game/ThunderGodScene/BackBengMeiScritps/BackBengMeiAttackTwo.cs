using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiAttackTwo : ActionState
{
    private bool whetherTheCombo;
    private bool isFollowUpAttackOpen;
    private bool currentAnimationAbleMove;
    float mouseLongHitTime;
    Vector2 joyPos;
    public BackBengMeiAttackTwo(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        whetherTheCombo = false;
        isFollowUpAttackOpen = false;
        currentAnimationAbleMove = false;

        //�����ظ�����
        EventManager.Instance.AddListener(EEventType.JoyStickDown, OnJoyStickDown);
        EventManager.Instance.AddListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        EventManager.Instance.AddListener(EEventType.JoyStickUp, OnJoyStickUp);
        EventManager.Instance.AddListener(EEventType.ScreenFingerZero, TouchReset);

    }

    override public void Enter()
    {
      
        mAt.Play("AttackTwo");
        whetherTheCombo = false;
        isFollowUpAttackOpen = false;
        currentAnimationAbleMove = false;
        mouseLongHitTime = 0;
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
            //��ɫ����Ϊ�����ᳯ��
            mRb.transform.forward = (new Vector3(AxisX, 0f, AxisY)).normalized;
            //��ȡ�������ƽ�泯��
            Vector3 CamForward = Camera.main.transform.forward;
            CamForward.y = 0;
            //��ȡ���ƽ�泯����������ǰ������ļнǶ�
            float angle = Vector3.Angle(CamForward, Vector3.forward);
            //�ж����x�ᳯ������
            if (CamForward.x < 0)
            {
                angle = -angle;
            }
            //��ɫ��ת����ȷ��λ��
            mRb.transform.Rotate(Vector3.up, angle);
        }
    }


    override public void Update()
    {
        // Pc ��������� 
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
                    mAm.ChangeAction(EActionType.AttackThree);
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
                if (Input.GetMouseButton(0))
                {
                    if ((mouseLongHitTime += Time.unscaledDeltaTime) >= mMaster.GetComponent<BackBengMeiData>().JumpTurnSwordMousLongHitTime)
                    {
                        mAm.ChangeAction(EActionType.JumpTurnSword);
                    }
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
            }
        }

        // Android ���������
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
                    mAm.ChangeAction(EActionType.AttackThree);
                    return;
                }
                float AxisX = Input.GetAxis("Horizontal");
                float AxisY = Input.GetAxis("Vertical");
                if (joyPos.x != 0.0f || joyPos.y != 0.0f)
                {
                    AxisX = joyPos.x;
                    AxisY = joyPos.y;
                }
                if ((AxisX != 0.0f || AxisY != 0.0f) && EasyTouchEvent.buttonEvadeState == "EvadeDown")
                {
                    mAm.ChangeAction(EActionType.EvadeForwardOne);
                    return;
                }
                else if (EasyTouchEvent.buttonEvadeState == "EvadeDown")
                {
                    mAm.ChangeAction(EActionType.EvadeBackwardOne);
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
            }
        }
    }

    override public void Exit()
    {
        ////�����ظ�����
        //EventManager.Instance.DeleteListener(EEventType.JoyStickDown, OnJoyStickDown);
        //EventManager.Instance.DeleteListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        //EventManager.Instance.DeleteListener(EEventType.JoyStickUp, OnJoyStickUp);
    }


    override public void OnAnimationStart()
    {

    }



    override public void OnAnimationHit(int i)
    {
        SoundMgr.Instance.PlayEffect("Sound/Effects/BackBengAtk2");
        BackBengMei.weaPonRightHand.GetChild(0).GetComponent<CapsuleCollider>().enabled = true; 
        TimerMgr.Instance.OneShot("BackBengMeiAttackTwoWeaPonRightHandUnActive", 0.5f, BackBengMeiAttackTwoWeaPonRightHandUnActive);
    }
    void BackBengMeiAttackTwoWeaPonRightHandUnActive()
    {
        BackBengMei.weaPonRightHand.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;
    }


    override public void OnAnimationEnd()
    {
        mAm.ChangeAction(EActionType.Idle);
    }

    override public void AnimationEventOne()
    {
        if(whetherTheCombo==true)
        {
            mAm.ChangeAction(EActionType.AttackThree);
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
        if (other.tag == "����֮��_�þ�")
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
