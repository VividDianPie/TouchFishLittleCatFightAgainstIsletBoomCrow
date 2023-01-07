using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillBig : MonoBehaviour
{
    public CinemachinePathBase path0;
    public CinemachinePathBase path1;
    public CinemachinePathBase path2;
    public CinemachinePathBase path3;
    public CinemachinePathBase path4;
    int pathChange;
    CinemachinePathBase tempPaht;

    void Start()
    {
        pathChange = 0;
        TimerMgr.Instance.Repeated("LodSlefTurn天殛之境_裁决", 0.05f, LodSlefTurn天殛之境_裁决);

       // Invoke("LodSlefTurn天殛之境_裁决End", 9);

        TimerMgr.Instance.OneShot("LodSlefTurn天殛之境_裁决End", 9.0f, LodSlefTurn天殛之境_裁决End);

    }

    void Update()
    {
        this.transform.Rotate(Vector3.up, 0.1f);
    }

    void LodSlefTurn天殛之境_裁决()
    {
       // if (SceneManager.GetActiveScene().name != "ThunderGodScene") { return; }
        int pathCtrl = pathChange++;
        if (pathCtrl % 5 == 0)
            tempPaht = path0;
        else if (pathCtrl % 5 == 1)
            tempPaht = path1;
        else if (pathCtrl % 5 == 2)
            tempPaht = path2;
        else if (pathCtrl % 5 == 3)
            tempPaht = path3;
        else if (pathCtrl % 5 == 4)
            tempPaht = path4;
        GameObject lodSlefTurn天殛之境_裁决 = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/SelfTurn天殛之境_裁决"));
        lodSlefTurn天殛之境_裁决.transform.SetParent(gameObject.transform);
        CinemachineDollyCart cinemachineDollyCartScripts = lodSlefTurn天殛之境_裁决.GetComponent<CinemachineDollyCart>();
        cinemachineDollyCartScripts.m_Path = tempPaht;
        cinemachineDollyCartScripts.m_Speed = 6;
    }

    void LodSlefTurn天殛之境_裁决End()
    {
        TimerMgr.Instance.DeleteTimer("LodSlefTurn天殛之境_裁决");
        Destroy(gameObject);
    }


}


