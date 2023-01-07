using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiuQiuPopple : MonoBehaviour
{
    public Animator At;
    public Rigidbody Rb;
    protected ActionMachine mAm;

    public int chunkId;

    public GameObject ledtHand;
    public GameObject rightHand;
     public Transform weaPonLeftHand { get; set; }
     public Transform weaPonRightHand { get; set; }
     public Transform QQRActor { get; set; }
    void Awake()
    {
        mAm = new ActionMachine(this.gameObject);
        QQRActor = this.transform;
        //weaPonLeftHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/»êÑýµ¶_ÑªÓ£¼ÅÃð"));
        //weaPonLeftHand.parent = ledtHand.transform;
        //weaPonLeftHand.transform.localPosition = ledtHand.transform.localPosition;
        //weaPonLeftHand.localPosition = new Vector3(0.03250026f, 0.06395942f, -0.02115426f);
        //weaPonLeftHand.localRotation = Quaternion.Euler(85.402f, 64.752f, 12.064f);
        //weaPonLeftHand.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        weaPonRightHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/QQRWeapon"));
        weaPonRightHand.parent = this.gameObject.transform;
        weaPonRightHand.transform.localPosition = this.gameObject.transform.localPosition;
        weaPonRightHand.localPosition = new Vector3(0.1987187f, 1.399445f, -0.1305424f);
        weaPonRightHand.localRotation = Quaternion.Euler(-239.239f, 49.55299f, 45.229f);
        weaPonRightHand.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        //weaPonRightHand.localPosition = new Vector3(0.61f, 0.058f, -0.465f);
        //weaPonRightHand.localRotation = Quaternion.Euler(-177.113f, 125.868f, -273.452f);
        //weaPonRightHand.localScale = new Vector3(1.0f, 1.0f, 1.0f);

    }

    void Start()
    {
        mAm.AddAction(new QiuQiuPoppleWalk(EActionType.QiuQiuPoppleWalk, mAm, this.gameObject));
        mAm.AddAction(new QiuQiuPopplejump(EActionType.QiuQiuPoppleJump, mAm, this.gameObject));
        mAm.AddAction(new QiuQiuPoppleRun(EActionType.QiuQiuPoppleRun, mAm, this.gameObject));
        mAm.AddAction(new QiuQiuPoppleBeHitOne(EActionType.QiuQiuPoppleBeHitOne, mAm, this.gameObject));
        mAm.AddAction(new QiuQiuPoppleBeHitTwo(EActionType.QiuQiuPoppleBeHitTwo, mAm, this.gameObject));
        mAm.AddAction(new QiuQiuPoppleAttackOne(EActionType.QiuQiuPoppleAttackOne, mAm, this.gameObject));
        mAm.AddAction(new QiuQiuPoppleAttackTwo(EActionType.QiuQiuPoppleAttackTwo, mAm, this.gameObject));
        mAm.AddAction(new QiuQiuPoppleDie(EActionType.QiuQiuPoppleDie, mAm, this.gameObject));


    }

    void Update()
    {
        if (mAm != null)
        { 
           mAm.Update();
        }
    }

    public void OnAnimationStart()
    {
        mAm.OnAnimationStart();
    }

    public void OnAnimationHit(int i)
    {
        mAm.OnAnimationHit(i);
    }
    public void OnAnimationEnd()
    {
        mAm.OnAnimationEnd();
    }

    public void AnimationEventOne()
    {
        mAm.AnimationEventOne();
    }

    public void AnimationEventTwo()
    {
        mAm.AnimationEventTwo();
    }

    public void AnimationEventThree()
    {
        mAm.AnimationEventThree();
    }


    public void OnCollisionEnter(Collision collision)
    {
        mAm.OnCollisionEnter(collision);
    }

    public void OnCollisionStay(Collision collision)
    {
        mAm.OnCollisionStay(collision);
    }

    public void OnCollisionExit(Collision collision)
    {
        mAm.OnCollisionExit(collision);
    }


    //´¥·¢Æ÷
    public void OnTriggerEnter(Collider other)
    {
        mAm.OnTriggerEnter(other);
    }

    public void OnTriggerStay(Collider other)
    {
        mAm.OnTriggerStay(other);
    }

    public void OnTriggerExit(Collider other)
    {
        mAm.OnTriggerExit(other);
    }





    void OnDestroy()
    {

    }
}
