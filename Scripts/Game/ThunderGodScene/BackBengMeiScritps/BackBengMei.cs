using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*     
 
     C:/Users/16179/AppData/Local/Temp/DefaultCompany/ToCastATreacherousVerdictUponFate1_2

 */
public class BackBengMei : MonoBehaviour
{
    public Animator At;
    public Rigidbody Rb;
    protected ActionMachine mAm;

    public GameObject ledtHand;
    public GameObject rightHand;
    static public Transform weaPonLeftHand { get; set; }
    static public Transform weaPonRightHand { get; set; }
    static public Transform heroActor { get; set; }
    void Awake()
    {
        //雷电芽衣不受时空断裂影响
        At.updateMode = AnimatorUpdateMode.UnscaledTime;
        heroActor = this.gameObject.transform;
        mAm = new ActionMachine(this.gameObject);
        //   魂妖刀_血樱寂灭           雾切之回光               DeDeIsOrcaPillow
        weaPonLeftHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/雾切之回光"));
        weaPonLeftHand.parent = ledtHand.transform;
        weaPonLeftHand.transform.localPosition = ledtHand.transform.localPosition;
        weaPonLeftHand.localPosition = new Vector3(0.024f, 0.013f, -0.018f);
        weaPonLeftHand.localRotation = Quaternion.Euler(181.176f, 306.569f, 168.572f);

        weaPonRightHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/雾切之回光"));
        weaPonRightHand.parent = rightHand.transform;
        weaPonRightHand.transform.localPosition = rightHand.transform.localPosition;
        weaPonRightHand.localPosition = new Vector3(0f, 0f, 0f);
        weaPonRightHand.localRotation = Quaternion.Euler(0f, 0f, 0f);
        weaPonRightHand.gameObject.SetActive(true);

        EventManager.Instance.AddListener(EEventType.BackBengMeiLeftHandAtkAt, BackBengMeiLeftHandAtkFeel);
        EventManager.Instance.AddListener(EEventType.BackBengMeiRightHandAtkAt, BackBengMeiRightHandAtkFeel);
        EventManager.Instance.AddListener(EEventType.HerrscherOfThunderMeiRightHandWeaPonBeHit, BackBengMeiBeBeatBack);
    }

    void Start()
    {
        mAm.AddAction(new BackBengMeiIdle(EActionType.Idle, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiRun(EActionType.Run, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiJump(EActionType.Jump, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiAttackOne(EActionType.AttackOne, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiAttackTwo(EActionType.AttackTwo, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiAttackThree(EActionType.AttackThree, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiAttackFour(EActionType.AttackFour, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiAttackFive(EActionType.AttackFive, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiEvadeForwardOne(EActionType.EvadeForwardOne, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiEvadeForwardTwo(EActionType.EvadeForwardTwo, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiEvadeBackForwardOne(EActionType.EvadeBackwardOne, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiEvadeBackForwardTwo(EActionType.EvadeBackwardTwo, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiEvadeEndToAttack(EActionType.EvadeEndToAttack, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiBeHitRetreat(EActionType.BeHitRetreat, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiBeHitOne(EActionType.BeHitOne, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiBeHitTwo(EActionType.BeHitTwo, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiParry(EActionType.Parry, mAm, this.gameObject));
        mAm.AddAction(new BackBengMeiJumpTurnSword(EActionType.JumpTurnSword, mAm, this.gameObject));

    }

    void Update()
    {
        //  Debug.Log(transform.forward);
       // Time.timeScale = 0.1f;
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

    //此攻击手感函数事件 仅仅调用 BackBengMeiLeftHandAtkFeel
    public void BackBengMeiLeftHandAtkFeel(MyEvent evt)
    {
        At.speed = 0.1f;
        TimerMgr.Instance.OneShot("BackBengMeiAtkFeelReset", 0.06f, BackBengMeiAtkFeelReset);
    }

    public void BackBengMeiRightHandAtkFeel(MyEvent evt)
    {
        At.speed = 0.1f;
        TimerMgr.Instance.OneShot("BackBengMeiAtkFeelReset", 1f, BackBengMeiAtkFeelReset);
    }

    public void BackBengMeiBeBeatBack(MyEvent evt)
    {
        //SoundMgr.Instance.mEffect.Pause();
        At.speed = 0.0f;
        TimerMgr.Instance.OneShot("BackBengMeiAtkFeelReset", 0.5f, BackBengMeiAtkFeelReset);
    }
    public void BackBengMeiAtkFeelReset()
    {
        At.speed = 1;
    }

    void OnDestroy()
    {
        EventManager.Instance.DeleteListener(EEventType.BackBengMeiLeftHandAtkAt, BackBengMeiLeftHandAtkFeel);
        EventManager.Instance.DeleteListener(EEventType.BackBengMeiRightHandAtkAt, BackBengMeiRightHandAtkFeel);
        EventManager.Instance.DeleteListener(EEventType.HerrscherOfThunderMeiRightHandWeaPonBeHit, BackBengMeiBeBeatBack);
    }

}
