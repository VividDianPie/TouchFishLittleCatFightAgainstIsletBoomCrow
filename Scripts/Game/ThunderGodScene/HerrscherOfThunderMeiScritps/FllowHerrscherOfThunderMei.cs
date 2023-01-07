using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FllowHerrscherOfThunderMei : MonoBehaviour
{
    public GameObject fllowHerrscherOfThunderMeiObj;
    public GameObject fllowBackBengMeiObj;
    private Vector3 mVelocity;
    string herrscherRingState;
    private void Awake()
    {
        mVelocity = Vector3.zero;
        herrscherRingState = "FlowHerrscherOfThunderMeiState";


        EventManager.Instance.AddListener(EEventType.HerrscherOfThunderMeiEventSkillBig, EventSkillIsHappen);
    }
    void Start()
    {

    }

    void Update()
    {
        //在雷律释放此技能时 雷神环停止跟随 
        if ((herrscherRingState == "FlowBackBengMeiState" || herrscherRingState == "FlowHerrscherOfThunderMeiState") &&
            HerrscherOfThunderMei.herrscherOfThunderMeiActor.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("SkillTwoSwordTrack"))
        {
            herrscherRingState = "SkillTwoSwordTrackState";
            //雷神环9秒之后重新跟随
            TimerMgr.Instance.OneShot("HerrscherRingCanFlowIsOk", 9f, HerrscherRingCanFlowIsOk);
            return;
        }









        if (herrscherRingState == "FlowHerrscherOfThunderMeiState")
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                herrscherRingState = "FlowBackBengMeiState";
            }
            this.transform.forward = HerrscherOfThunderMei.herrscherOfThunderMeiActor.forward;
            this.transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(
                fllowHerrscherOfThunderMeiObj.transform.position.x, fllowHerrscherOfThunderMeiObj.transform.position.y - 0f, fllowHerrscherOfThunderMeiObj.transform.position.z - 0.1f),
                ref mVelocity, 0.05f);
        }
        else if (herrscherRingState == "FlowBackBengMeiState")
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                herrscherRingState = "FlowHerrscherOfThunderMeiState";
            }

            this.transform.forward = BackBengMei.heroActor.forward;
            this.transform.position = Vector3.SmoothDamp(this.transform.position, new Vector3(
                fllowBackBengMeiObj.transform.position.x, fllowBackBengMeiObj.transform.position.y - 0f, fllowBackBengMeiObj.transform.position.z - 0.1f),
                ref mVelocity, 0.05f);

        }
        else if (herrscherRingState == "SkillTwoSwordTrackState")
        {
            //雷神环停止跟随时 同时统一与雷律的朝向
            this.transform.forward = (new Vector3(BackBengMei.heroActor.position.x, transform.position.y,
            BackBengMei.heroActor.position.z) - transform.position).normalized; ;
        }
        else if (herrscherRingState == "RingSkillState")
        {
            this.transform.Rotate(Vector3.up, 1);

            //雷神环9秒之后重新跟随
            TimerMgr.Instance.OneShot("HerrscherRingCanFlowIsOk", 9f, HerrscherRingCanFlowIsOk);
        }
    }

    void HerrscherRingCanFlowIsOk()
    {
        herrscherRingState = "FlowHerrscherOfThunderMeiState";
    }



    void EventSkillIsHappen(MyEvent evt)
    {
        herrscherRingState = "RingSkillState";
    }



    public void ChangeRingUi()
    {
        if (herrscherRingState == "FlowBackBengMeiState")
        {
            herrscherRingState = "FlowHerrscherOfThunderMeiState";
        }
        else if (herrscherRingState == "FlowHerrscherOfThunderMeiState")
        {
            herrscherRingState = "FlowBackBengMeiState";
        }

    }
}
