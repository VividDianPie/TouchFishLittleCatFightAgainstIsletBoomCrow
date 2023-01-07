using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopWeaponUi : MonoBehaviour
{
    public RectTransform ºì¾¨Button;
    public RectTransform Ñýµ¶Button;
    public RectTransform ÎíÇÐButton;
    public RectTransform content;
    Vector3 oldPos;
    void Start()
    {

    }

    void Update()
    {
        if (Mathf.Abs((oldPos - BackBengMei.heroActor.position).magnitude) > 0.1f)
        {
            oldPos = BackBengMei.heroActor.position;
            RefreshInterface();
        }
        if (Input.GetKeyDown(KeyCode.F) && (content.childCount > 0))
        {
            //Debug.Log("KeyCode.F");
            Collider[] colliders = Physics.OverlapSphere(BackBengMei.heroActor.position, 1);
            float maxDis = 3f;
            Collider colliderObj = null;
            for (int i = 0; i < colliders.Length; ++i)
            {
                if (colliders[i].tag == "PopWeapon")
                {
                    float tempDis = Vector3.Distance(BackBengMei.heroActor.position, colliders[i].transform.position);
                    if (tempDis < maxDis)
                    {
                        colliderObj = colliders[i];
                        maxDis = tempDis;
                    }
                }
            }

            if (colliderObj.name == "DeDeIsOrcaPillowPop")
            {
                BagSystem.Instance.AddItem(1001, 1);
            }
            else if (colliderObj.name == "»êÑýµ¶_ÑªÓ£¼ÅÃðPop")
            {
                BagSystem.Instance.AddItem(1003, 1);
            }
            else if (colliderObj.name == "ÎíÇÐÖ®»Ø¹âPop")
            {
                BagSystem.Instance.AddItem(1002, 1);
            }
            DestroyImmediate(content.GetChild(0).gameObject);
            DestroyImmediate(colliderObj.GetComponentInParent<Transform>().gameObject);
            RefreshInterface();
        }
    }

    public void ºì¾¨ButtonHitEvent()
    {
        Debug.Log("ºì¾¨ButtonHitEvent");
        Collider[] colliders = Physics.OverlapSphere(BackBengMei.heroActor.position, 1);
        float maxDis = 3f;
        Collider colliderObj = null;
        for (int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i].name == "DeDeIsOrcaPillowPop")
            {
                float tempDis = Vector3.Distance(BackBengMei.heroActor.position, colliders[i].transform.position);
                if (tempDis < maxDis)
                {
                    colliderObj = colliders[i];
                    maxDis = tempDis;
                }
            }
        }
        DestroyImmediate(colliderObj.GetComponentInParent<Transform>().gameObject);
        RefreshInterface();
        BagSystem.Instance.AddItem(1001, 1);
    }

    public void Ñýµ¶ButtonHitEvent()
    {
        Debug.Log("Ñýµ¶ButtonHitEvent");
        Collider[] colliders = Physics.OverlapSphere(BackBengMei.heroActor.position, 1);
        float maxDis = 3f;
        Collider colliderObj = null;
        for (int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i].name == "»êÑýµ¶_ÑªÓ£¼ÅÃðPop")
            {
                float tempDis = Vector3.Distance(BackBengMei.heroActor.position, colliders[i].transform.position);
                if (tempDis < maxDis)
                {
                    colliderObj = colliders[i];
                    maxDis = tempDis;
                }
            }
        }
        DestroyImmediate(colliderObj.GetComponentInParent<Transform>().gameObject);
        RefreshInterface();
        BagSystem.Instance.AddItem(1003, 1);
    }

    public void ÎíÇÐButtonHitEvent()
    {
        Debug.Log("ÎíÇÐButtonHitEvent");
        Collider[] colliders = Physics.OverlapSphere(BackBengMei.heroActor.position, 1);
        float maxDis = 3f;
        Collider colliderObj = null;
        for (int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i].name == "ÎíÇÐÖ®»Ø¹âPop")
            {
                float tempDis = Vector3.Distance(BackBengMei.heroActor.position, colliders[i].transform.position);
                if (tempDis < maxDis)
                {
                    colliderObj = colliders[i];
                    maxDis = tempDis;
                }
            }
        }
        DestroyImmediate(colliderObj.GetComponentInParent<Transform>().gameObject);
        RefreshInterface();
        BagSystem.Instance.AddItem(1002, 1);
    }

    void RefreshInterface()
    {
        int contentCount = content.childCount;
        for (int i = 0; i < contentCount; i++)
        {
            DestroyImmediate(content.GetChild(0).gameObject);
        }
        Collider[] colliders = Physics.OverlapSphere(BackBengMei.heroActor.position, 1);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].name == "DeDeIsOrcaPillowPop")
            {
                RectTransform button = Instantiate<RectTransform>(ºì¾¨Button);
                button.gameObject.SetActive(true);
                button.SetParent(content);
            }
            else if (colliders[i].name == "ÎíÇÐÖ®»Ø¹âPop")
            {
                RectTransform button = Instantiate<RectTransform>(ÎíÇÐButton);
                button.gameObject.SetActive(true);
                button.SetParent(content);
            }
            else if (colliders[i].name == "»êÑýµ¶_ÑªÓ£¼ÅÃðPop")
            {
                RectTransform button = Instantiate<RectTransform>(Ñýµ¶Button);
                button.gameObject.SetActive(true);
                button.SetParent(content);
            }
        }
    }
}
