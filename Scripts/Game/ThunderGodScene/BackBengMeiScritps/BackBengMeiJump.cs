using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackBengMeiJump : ActionState
{
    private Vector3 startFrontObjForward;
    private Vector3 laterObjForward;
    private float frontLaterTurnIncludeAngle;
    private float lerpTurn;
    RaycastHit hitinfo;
    RaycastHit centerDownRaycastHitinfo;
    float startSpeed;
    float gravityAddSpeed;
    Vector2 joyPos;


    public BackBengMeiJump(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
    
    }


    override public void Enter()
    {
        //�����ظ�����
        EventManager.Instance.AddListener(EEventType.JoyStickDown, OnJoyStickDown);
        EventManager.Instance.AddListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        EventManager.Instance.AddListener(EEventType.JoyStickUp, OnJoyStickUp);
        EventManager.Instance.AddListener(EEventType.ScreenFingerZero, TouchReset);


        mAt.CrossFade("Jump", 0.05f);
        mMaster.transform.position += Vector3.up * 0.1f;
        startSpeed = mMaster.GetComponent<BackBengMeiData>().backBengMeiJumpPower;//  7
        gravityAddSpeed = -10f;
        joyPos = Vector2.zero;

        //mMaster.GetComponent<Rigidbody>().useGravity = false;
        //mRb.velocity = mRb.velocity + new Vector3(0f, mMaster.GetComponent<BackBengMeiData>().backBengMeiJumpPower, 0f);
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
            //�����ɫ����ǰ�ĳ���
            startFrontObjForward = mRb.transform.forward;
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
            //��ȡ��ɫ����֮��ĳ���
            laterObjForward = mRb.transform.forward;
            //����ǰ����ĽǶȲ�
            frontLaterTurnIncludeAngle = AngleSigned(startFrontObjForward, laterObjForward, Vector3.up);
            //��ɫ���������ת���������
            mRb.transform.forward = startFrontObjForward;
            lerpTurn = Mathf.Lerp(0.0f, frontLaterTurnIncludeAngle, 0.05f);
            mRb.transform.Rotate(Vector3.up, lerpTurn);

            //���߼��
            bool isCollider = Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, mMaster.transform.forward, out hitinfo, 0.5f);
            if (isCollider == false)
            {
                mMaster.transform.position +=
                    mMaster.transform.forward * mMaster.GetComponent<BackBengMeiData>().backBengMeiJumpMoveSpeed * Time.unscaledDeltaTime;
            }
        }
        if (Physics.Raycast(mMaster.transform.position, -Vector3.up, out centerDownRaycastHitinfo, 0.1f) == false)
        {
            float _t = startSpeed * Time.unscaledDeltaTime + gravityAddSpeed * Time.unscaledDeltaTime * Time.unscaledDeltaTime / 2;
            startSpeed += gravityAddSpeed * Time.unscaledDeltaTime;
            mMaster.transform.position += Vector3.up * _t;
        }
        else  
        {
            mAm.ChangeAction(EActionType.Idle);
        }
    }

    override public void Exit()
    {
        //�����ظ�����
        EventManager.Instance.DeleteListener(EEventType.JoyStickDown, OnJoyStickDown);
        EventManager.Instance.DeleteListener(EEventType.JoyStickDrag, OnJoyStickDrag);
        EventManager.Instance.DeleteListener(EEventType.JoyStickUp, OnJoyStickUp);
        //mMaster.GetComponent<Rigidbody>().useGravity = true;
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
     
    //��ȡ����֮��������Ƕ�   ����n��ֱ��v1��v2�γɵ�ƽ��
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
