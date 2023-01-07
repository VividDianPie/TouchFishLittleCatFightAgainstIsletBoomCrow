using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTwoSwordTrack : MonoBehaviour
{
    public float generateSwordTime;//0.1
    public float stopGenerateTime;//3
    public float SwordMoveSpeed;//5
    public CinemachinePathBase pathOne;
    public CinemachinePathBase pathTwo;
    public CinemachinePathBase pathThree;
    public CinemachinePathBase pathFour;
    public CinemachinePathBase pathFive;
    CinemachinePathBase pathRund;

    void Start()
    {
        TimerMgr.Instance.Repeated("SkillTwoSwordTrackRun", generateSwordTime, SkillTwoSwordTrackRun);

       // Invoke("SkillTwoSwordTrackStop", stopGenerateTime);

        TimerMgr.Instance.OneShot("SkillTwoSwordTrackStop", stopGenerateTime, SkillTwoSwordTrackStop);


    }

    void SkillTwoSwordTrackRun()
    {
        if (pathOne == null || pathTwo == null || pathThree == null || pathFour == null || pathFive == null) {
            return;
        };
        //Ëæ»úÑ¡ÔñÂ·¾¶
        int range = Random.Range(1, 6);
        if (range == 1)
            pathRund = pathOne;
        else if (range == 2)
            pathRund = pathTwo;
        else if (range == 3)
            pathRund = pathThree;
        else if (range == 4)
            pathRund = pathFour;
        else if (range == 5)
            pathRund = pathFive;

        GameObject cinemachineDollyCartObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/SkillTwoSwordTrack"),this.gameObject.transform);
        /*CinemachineDollyCart cinemachineDollyCartScripts =*/ cinemachineDollyCartObj.GetComponent<CinemachineDollyCart>().m_Path = pathRund;
        cinemachineDollyCartObj.GetComponent<CinemachineDollyCart>().m_Speed = SwordMoveSpeed;
        //cinemachineDollyCartScripts.m_Path = pathRund;
        //cinemachineDollyCartScripts.m_Speed = SwordMoveSpeed;

        this.gameObject.transform.forward = (new Vector3(BackBengMei.heroActor.position.x, this.gameObject.transform.position.y,
            BackBengMei.heroActor.position.z) - this.gameObject.transform.position).normalized;
    }

    void SkillTwoSwordTrackStop()
    {
        TimerMgr.Instance.DeleteTimer("SkillTwoSwordTrackRun");
    }

}
