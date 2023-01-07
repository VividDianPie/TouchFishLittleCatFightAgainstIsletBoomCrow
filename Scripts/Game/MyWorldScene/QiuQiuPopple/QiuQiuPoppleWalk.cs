using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiuQiuPoppleWalk : ActionState
{
    string qQDirCtrl;
    int randCound;
    float IncludeAngle;
    float lerpTurn;
    Vector3 qiuQiuPoppleBackFrward;
    public QiuQiuPoppleWalk(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
        : base(type, am, master, pnt, cld)
    {
        qQDirCtrl = "left";
    }


    override public void Enter()
    {
        mAt.CrossFade("QQR_Walk", 0.05f);
        // mAt.Play("Idle");
        randCound = -1;
    }


    override public void Update()
    {
        if (GameManager.Instance.GetThisUiIsDontDestroyUiCount() > 0) { return; };
        if (mMaster.transform.position.y < -10)
        {
            GameObject.Destroy(mMaster.transform.gameObject);
        }
        //如果没有碰撞那么可走
        RaycastHit hitinfo;
        if (Physics.Raycast(mMaster.transform.position + Vector3.up * 0.5f, mMaster.transform.forward, out hitinfo, 0.5f) == false &&
               (randCound == -1))
        {
            if (qQDirCtrl == "left")
            {
                mMaster.transform.Rotate(Vector3.up, 0.5f);
            }
            else if (qQDirCtrl == "right")
            {
                mMaster.transform.Rotate(Vector3.up, -0.5f);
            }

            mMaster.transform.position +=
                mMaster.transform.forward * mMaster.GetComponent<QiuQiuPoppleData>().qiuQiuPoppleWalkSpeed * Time.deltaTime;
        }
        //如果qiuqiu人前进碰撞有几率 跳跃 转向
        else if (((randCound == -1) && (hitinfo.transform.gameObject.name == "Chunk(Clone)")))
        {
            randCound = Random.Range(0, 2);
            qiuQiuPoppleBackFrward = -mMaster.transform.forward;
        }
        if (randCound == 0)
        {
            mAm.ChangeAction(EActionType.QiuQiuPoppleJump);
            return;
        }
        else if (randCound == 1)
        {
            IncludeAngle = AngleSigned(mMaster.transform.forward, qiuQiuPoppleBackFrward, Vector3.up);
            //丘丘人朝向过渡旋转至计算后方向
            lerpTurn = Mathf.Lerp(0.0f, IncludeAngle, 0.07f);
            mRb.transform.Rotate(Vector3.up, lerpTurn);
            if (IncludeAngle <= 0.35f)
            {
                randCound = -1;
            }
        }
        if (Physics.Raycast(mMaster.transform.position + Vector3.up * 1.5f, mMaster.transform.forward, out hitinfo, 0.5f) == true &&
        hitinfo.transform.gameObject.name == "QiuQiuPopple")
        {
            mMaster.transform.forward = (randCound == 1) ? (mMaster.transform.right) : (-mMaster.transform.right);
            mAm.ChangeAction(EActionType.QiuQiuPoppleJump);
            return;
        }
        //丘丘人的扇形索敌范围限制
        if ((BackBengMei.heroActor.position - mMaster.transform.position).sqrMagnitude <= 5 &&
                  Vector3.Angle(mMaster.transform.forward, (BackBengMei.heroActor.position - mMaster.transform.position).normalized) <= 35)
        {
            mMaster.GetComponent<QiuQiuPoppleData>().qiuQiuPoppIsAnger = true;
            mAm.ChangeAction(EActionType.QiuQiuPoppleRun);
            return;
        }



        if (mMaster.GetComponent<QiuQiuPoppleData>().qiuQiuPoppIsAnger == true)
        {
            mAm.ChangeAction(EActionType.QiuQiuPoppleRun);
        }

    }
    override public void Exit()
    {
    }

    override public void OnAnimationStart()
    {

    }



    override public void OnAnimationHit(int i)
    {

    }



    override public void OnAnimationEnd()
    {
        if (Random.Range(0, 2) == 0)
        {
            qQDirCtrl = "left";
        }
        else
        {
            qQDirCtrl = "right";
        }
    }

    override public void AnimationEventOne()
    {

    }

    override public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "雾切之回光" || other.tag == "魂妖刀_血樱寂灭")
        {
            mAm.ChangeAction(EActionType.QiuQiuPoppleBeHitOne);
            return;
        }
    }


    //获取向量之间的正负角度   其中n垂直与v1与v2形成的平面
    public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }
}

