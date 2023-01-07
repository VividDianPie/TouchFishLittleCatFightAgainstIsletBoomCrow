using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderMeiWeaPonTurn : ActionState
{
    private bool rightHandWeaPonIsCanTurn;
    public HerrscherOfThunderMeiWeaPonTurn(EActionType type, ActionMachine am, GameObject master, Animator at, ActionState pnt = null,
        ActionState cld = null)
    : base(type, am, master, pnt, cld)
    {
        rightHandWeaPonIsCanTurn = false;
    }

    override public void Enter()
    {
        mAt.Play("WeaPonTurn");
        rightHandWeaPonIsCanTurn = false;
    }


    override public void Update()
    {
        if (rightHandWeaPonIsCanTurn == true)
        {


            // Pc 端转速
           // HerrscherOfThunderMei.rightHandWeaPon.Rotate(-mMaster.transform.forward, Time.deltaTime * 2500, Space.World);
            // Android 端转速
            HerrscherOfThunderMei.rightHandWeaPon.Rotate(-mMaster.transform.forward, Time.deltaTime * 6000, Space.World);



            //计算夹角平滑朝向
            float meiIncludeAngle = AngleSigned(new Vector3(mMaster.transform.forward.x, 0, mMaster.transform.forward.z).normalized,
                (BackBengMei.heroActor.position - mMaster.transform.position).normalized, Vector3.up);
            // Pc 端转速
         //   float lerpTurn = Mathf.Lerp(0.0f, meiIncludeAngle, 0.01f);
            // Android 端转速
            float lerpTurn = Mathf.Lerp(0.0f, meiIncludeAngle, 0.05f);

            mRb.transform.Rotate(Vector3.up, lerpTurn);
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
        mAm.ChangeAction(EActionType.HerrscherOfThunderMeiIdle);
    }

    override public void AnimationEventOne()
    {
        mAt.speed = 0.15f;
        rightHandWeaPonIsCanTurn = true;
        HerrscherOfThunderMei.herrscherOfThunderMeiActor.GetComponent<Rigidbody>().isKinematic = true;
        HerrscherOfThunderMei.rightHandWeaPon.localPosition = new Vector3(-0.091f, 0.099f, 0.005f);
        HerrscherOfThunderMei.rightHandWeaPon.localRotation = Quaternion.Euler(67.493f, -136.337f, -105.047f);
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().enabled = true;
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().isTrigger = false;
        HerrscherOfThunderMei.rightHandWeaPon.gameObject.AddComponent<Rigidbody>().useGravity = false;
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<Rigidbody>().constraints =
            RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | 
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        EventManager.Instance.AddListener(EEventType.HerrscherOfThunderMeiRightHandWeaPonBeHit, HerrscherOfThunderMeiBeatBack);

    }

    override public void AnimationEventTwo()
    {
        if (mAt == null) { return; }
        mAt.speed = 1.0f;
        rightHandWeaPonIsCanTurn = false;
        HerrscherOfThunderMei.herrscherOfThunderMeiActor.GetComponent<Rigidbody>().isKinematic = false;
        HerrscherOfThunderMei.rightHandWeaPon.localPosition = new Vector3(-0.035f, 0.067f, -0.015f);
        HerrscherOfThunderMei.rightHandWeaPon.localRotation = Quaternion.Euler(76.699f, -122.294f, -62.854f);
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().enabled = false;
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().isTrigger = true;
        DelayDestory.Instance.SetDestroy(HerrscherOfThunderMei.rightHandWeaPon.GetComponent<Rigidbody>());
        EventManager.Instance.DeleteListener(EEventType.BackBengMeiLeftHandAtkAt, HerrscherOfThunderMeiBeatBack);

    }

    public void HerrscherOfThunderMeiBeatBack(MyEvent evt)
    {
        if (mAt == null) { return; }
        mAt.speed = 1.0f;
        rightHandWeaPonIsCanTurn = false;
        HerrscherOfThunderMei.rightHandWeaPon.localPosition = new Vector3(-0.035f, 0.067f, -0.015f);
        HerrscherOfThunderMei.rightHandWeaPon.localRotation = Quaternion.Euler(76.699f, -122.294f, -62.854f);
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().enabled = false;
        HerrscherOfThunderMei.rightHandWeaPon.GetComponent<CapsuleCollider>().isTrigger = true;
        DelayDestory.Instance.SetDestroy(HerrscherOfThunderMei.rightHandWeaPon.GetComponent<Rigidbody>());
        EventManager.Instance.DeleteListener(EEventType.BackBengMeiLeftHandAtkAt, HerrscherOfThunderMeiBeatBack);
        mAm.ChangeAction(EActionType.DoublieKnifeAttack);
    }

    //获取向量之间的正负角度   其中n垂直与v1与v2形成的平面
    public static float AngleSigned(Vector3 v1, Vector3 v2, Vector3 n)
    {
        return Mathf.Atan2(Vector3.Dot(n, Vector3.Cross(v1, v2)), Vector3.Dot(v1, v2)) * Mathf.Rad2Deg;
    }
 

}
