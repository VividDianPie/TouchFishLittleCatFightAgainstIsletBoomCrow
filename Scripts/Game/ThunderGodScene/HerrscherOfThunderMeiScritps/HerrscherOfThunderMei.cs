using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerrscherOfThunderMei : MonoBehaviour
{
    public Animator At;
    public Rigidbody Rb;
    protected ActionMachine mAm;
    public GameObject leftHand;
    public GameObject rightHand;

    static public Transform leftHandWeaPon { get; set; }
    static public Transform rightHandWeaPon { get; set; }
    static public Transform herrscherOfThunderMeiActor { get; set; }

    void Awake()
    {
       herrscherOfThunderMeiActor = this.gameObject.transform;
        mAm = new ActionMachine(this.gameObject);

        leftHandWeaPon = GameObject.Instantiate(Resources.Load<Transform>("Prefab/天殛之境_裁决"));
        leftHandWeaPon.parent = leftHand.transform;
        leftHandWeaPon.transform.localPosition = leftHand.transform.localPosition;
        leftHandWeaPon.localPosition = new Vector3(0.032f, 0.069f, -0.014f);
        leftHandWeaPon.localRotation = Quaternion.Euler(80.737f, 84.722f, 32.592f);
        leftHandWeaPon.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        leftHandWeaPon.gameObject.SetActive(false);

        rightHandWeaPon = GameObject.Instantiate(Resources.Load<Transform>("Prefab/天殛之境_裁决"));
        rightHandWeaPon.parent = rightHand.transform;
        rightHandWeaPon.transform.localPosition = rightHand.transform.localPosition;
        rightHandWeaPon.localPosition = new Vector3(-0.035f, 0.067f, -0.015f);
        rightHandWeaPon.localRotation = Quaternion.Euler(76.699f, -122.294f, -62.854f);
        rightHandWeaPon.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        EventManager.Instance.AddListener(EEventType.HerrscherOfThunderMeiDoubleHandAtkAtEvent, HerrscherOfThunderMeiAttackFeel);

    }

    void Start()
    {
        mAm.AddAction(new Walk(EActionType.Walk, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderMeiIdle(EActionType.HerrscherOfThunderMeiIdle, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderMeiWeaPonTurn(EActionType.HerrscherOfThunderWeaPonTurn, mAm, this.gameObject, At));
        mAm.AddAction(new HerrscherOfThunderMeiDoublieKnifeAttack(EActionType.DoublieKnifeAttack, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderMeiEvadeForwardOne(
            EActionType.HerrscherOfThunderEvadeForwardOne, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderMeiEvadeForwardTwo(
            EActionType.HerrscherOfThunderEvadeForwardTwo, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderMeiSkillOneLandThorn(EActionType.SkillOneLandThorn, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderMeiBehitOne(EActionType.HerrscherOfThunderBeHitOne, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderMeiBehitTwo(EActionType.HerrscherOfThunderBeHitTwo, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderMeiSkillTwoSwordTrack(
            EActionType.HerrscherOfThunderMeiSkillTwoSwordTrack, mAm, this.gameObject));

        mAm.AddAction(new HerrscherOfThunderAtkOne(EActionType.HerrscherOfThunderAtkOne, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderAtkTwo(EActionType.HerrscherOfThunderAtkTwo, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderAtkThree(EActionType.HerrscherOfThunderAtkThree, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderAtkFour(EActionType.HerrscherOfThunderAtkFour, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderAtkFive(EActionType.HerrscherOfThunderAtkFive, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderMeiSkillThreeJumpTurnSword(
            EActionType.SkillThreeJumpTurnSword, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderEvadeBackwardOne(EActionType.HerrscherOfThunderEvadeBackwardOne, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderEvadeBackwardTwo(EActionType.HerrscherOfThunderEvadeBackwardTwo, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderMeiSkillBezierCuves(EActionType.SkillBezierCuves, mAm, this.gameObject));
        mAm.AddAction(new HerrscherOfThunderMeiSkillBig(EActionType.SkillBig, mAm, this.gameObject));
        //加载对话Ui
        StartCoroutine(LodTalkUi());

    }
    IEnumerator LodTalkUi()
    {
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.LoadUI("Prefab/Uis/TalkUi");
    }

    void Update()
    {
        //Debug.Log(Vector3.Distance(this.transform.position, BackBengM ei.heroActor.position));
        mAm.Update();
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

    //对撞机
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


    //触发器
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
    void HerrscherOfThunderMeiAttackFeel(MyEvent evt)
    {
        At.speed = 0.1f; 
        TimerMgr.Instance.OneShot("HerrscherOfThunderMeiAttackFeelReset", 0.09f, HerrscherOfThunderMeiAttackFeelReset);
    }
    void HerrscherOfThunderMeiAttackFeelReset()
    {
        At.speed = 1.0f;
    }
    void OnDestroy()
    {
        EventManager.Instance.DeleteListener(EEventType.HerrscherOfThunderMeiDoubleHandAtkAtEvent, HerrscherOfThunderMeiAttackFeel);
    }
}

