using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QiuQiuPoppleDie : ActionState
{
    int randCound;

    public QiuQiuPoppleDie(EActionType type, ActionMachine am, GameObject master, ActionState pnt = null, ActionState cld = null)
        : base(type, am, master, pnt, cld)
    {
        randCound = -1;

    }


    override public void Enter()
    {
        randCound = Random.Range(0, 3);

        mAt.CrossFade("QQRDie", 0.05f);
        // mAt.Play("Idle");

        mMaster.transform.forward = (BackBengMei.heroActor.position - mMaster.transform.position).normalized;

        mRb.velocity = mRb.velocity + new Vector3(0f, 3.5f, 3.5f);

        if (randCound == 0) //µÙµÙµÄ»¢¾¨±§Õí
        {
            for (int i = 0; i < 5; i++) ;
            {
                GameObject pOpObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/PopWeapon/DeDeIsOrcaPillowPop"));
                pOpObj.transform.position = mMaster.transform.position + Vector3.up * 2;
            }
        }
        else if (randCound == 1) //Ñýµ¶
        {
            for (int i = 0; i < 5; i++) ;
            {
                GameObject pOpObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/PopWeapon/ÎíÇÐÖ®»Ø¹âPop"));
                pOpObj.transform.position = mMaster.transform.position + Vector3.up * 2;
            }
        }
        else if (randCound == 2) //ÎíÇÐ
        {
            for (int i = 0; i < 5; i++) ;
            {
                GameObject pOpObj = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/PopWeapon/»êÑýµ¶_ÑªÓ£¼ÅÃðPop"));
                pOpObj.transform.position = mMaster.transform.position + Vector3.up * 2;
            }
        }




        //mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.parent = mMaster.GetComponent<QiuQiuPopple>().QQRActor;
        //mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.transform.localPosition = mMaster.GetComponent<QiuQiuPopple>().QQRActor.localPosition;
        //mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.localPosition = new Vector3(0.1987187f, 1.399445f, -0.1305424f);
        //mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.localRotation = Quaternion.Euler(-239.239f, 49.55299f, 45.229f);
        //mMaster.GetComponent<QiuQiuPopple>().weaPonRightHand.localScale = new Vector3(1.0f, 1.0f, 1.0f);

    }


    override public void Update()
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
        GameObject.Destroy(mMaster.transform.gameObject);
    }

}

