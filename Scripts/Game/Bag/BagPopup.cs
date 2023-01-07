using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/*
C:/Users/16179/AppData/Local/Temp/DefaultCompany/ToCastATreacherousVerdictUponFate1_2
          ����Excels������� ͨ�� �Զ���༭����չ���ڶ�̬���ӿ� ��Excels����ת����bite���� ͨ�������������л� ת���ɴ����Ӧ��������
*/
class BagEvtData
{
    public int id;
    public int count;

    public BagEvtData(int id, int count)
    {
        this.id = id;
        this.count = count;
    }
}


public class BagPopup : MonoBehaviour
{
    //ʹ�ð�ť
    public Button UseBtn;

    //scroll content
    public GameObject Content;

    //ItemSelectCtrl
    public GameObject outfitUiObj;

    //��ǰѡ�е���Ʒ
    int mSelectItemId;


    //װ����Ʒѡ�� ������ �¼�����Ϊ ѡ����Ʒ��Id �� Id��Ӧ��Ʒ������
    void Awake()
    {
        EventManager.Instance.AddListener(EEventType.BagItemSelect, OnBagItemSelect);
        EventManager.Instance.DispatchEvent(new MyEvent(EEventType.GameStop, null));


    }


    void OnDestroy()
    {
        EventManager.Instance.DeleteListener(EEventType.BagItemSelect, OnBagItemSelect);
        EventManager.Instance.DispatchEvent(new MyEvent(EEventType.GameRun, null));

    }


    void Start()
    {
        mSelectItemId = -1;
        RefreshUseBtn();
        RefreshView();
    }


    void Update()
    {

    }

    //��ť���� �˺�����ʹ�ð�ť����
    public void OnUseBtn()
    {
        if (mSelectItemId > 0)
        {
            //ʹ����Ʒ
            BagSystem.Instance.AddItem(mSelectItemId, -1);

            if (mSelectItemId == 1001)//�ٵٵĻ�������
            {
                Destroy(BackBengMei.weaPonLeftHand.gameObject);
                Destroy(BackBengMei.weaPonRightHand.gameObject);

                BackBengMei.weaPonLeftHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/DeDeIsOrcaPillow"));
                BackBengMei.weaPonLeftHand.parent = BackBengMei.heroActor.GetComponent<BackBengMei>().ledtHand.transform;
                BackBengMei.weaPonLeftHand.transform.localPosition = 
                    BackBengMei.heroActor.GetComponent<BackBengMei>().ledtHand.transform.localPosition;
                BackBengMei.weaPonLeftHand.localPosition = new Vector3(0.024f, 0.013f, -0.018f);
                BackBengMei.weaPonLeftHand.localRotation = Quaternion.Euler(181.176f, 306.569f, 168.572f);

                BackBengMei.weaPonRightHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/DeDeIsOrcaPillow"));
                BackBengMei.weaPonRightHand.parent = BackBengMei.heroActor.GetComponent<BackBengMei>().rightHand.transform;
                BackBengMei.weaPonRightHand.transform.localPosition = 
                    BackBengMei.heroActor.GetComponent<BackBengMei>().rightHand.transform.localPosition;
                BackBengMei.weaPonRightHand.localPosition = new Vector3(0f, 0f, 0f);
                BackBengMei.weaPonRightHand.localRotation = Quaternion.Euler(0f, 0f, 0f);
                BackBengMei.weaPonRightHand.gameObject.SetActive(true);
            }
            else if (mSelectItemId == 1002)//����֮�ع�
            {
                Destroy(BackBengMei.weaPonLeftHand.gameObject);
                Destroy(BackBengMei.weaPonRightHand.gameObject);

                BackBengMei.weaPonLeftHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/����֮�ع�"));
                BackBengMei.weaPonLeftHand.parent = BackBengMei.heroActor.GetComponent<BackBengMei>().ledtHand.transform;
                BackBengMei.weaPonLeftHand.transform.localPosition =
                    BackBengMei.heroActor.GetComponent<BackBengMei>().ledtHand.transform.localPosition;
                BackBengMei.weaPonLeftHand.localPosition = new Vector3(0.024f, 0.013f, -0.018f);
                BackBengMei.weaPonLeftHand.localRotation = Quaternion.Euler(181.176f, 306.569f, 168.572f);

                BackBengMei.weaPonRightHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/����֮�ع�"));
                BackBengMei.weaPonRightHand.parent = BackBengMei.heroActor.GetComponent<BackBengMei>().rightHand.transform;
                BackBengMei.weaPonRightHand.transform.localPosition =
                    BackBengMei.heroActor.GetComponent<BackBengMei>().rightHand.transform.localPosition;
                BackBengMei.weaPonRightHand.localPosition = new Vector3(0f, 0f, 0f);
                BackBengMei.weaPonRightHand.localRotation = Quaternion.Euler(0f, 0f, 0f);
                BackBengMei.weaPonRightHand.gameObject.SetActive(true);
            }
            else if (mSelectItemId == 1003)//������_Ѫӣ����
            {
                Destroy(BackBengMei.weaPonLeftHand.gameObject);
                Destroy(BackBengMei.weaPonRightHand.gameObject);

                BackBengMei.weaPonLeftHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/������_Ѫӣ����"));
                BackBengMei.weaPonLeftHand.parent = BackBengMei.heroActor.GetComponent<BackBengMei>().ledtHand.transform;
                BackBengMei.weaPonLeftHand.transform.localPosition =
                    BackBengMei.heroActor.GetComponent<BackBengMei>().ledtHand.transform.localPosition;
                BackBengMei.weaPonLeftHand.localPosition = new Vector3(0.024f, 0.013f, -0.018f);
                BackBengMei.weaPonLeftHand.localRotation = Quaternion.Euler(181.176f, 306.569f, 168.572f);

                BackBengMei.weaPonRightHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/������_Ѫӣ����"));
                BackBengMei.weaPonRightHand.parent = BackBengMei.heroActor.GetComponent<BackBengMei>().rightHand.transform;
                BackBengMei.weaPonRightHand.transform.localPosition =
                    BackBengMei.heroActor.GetComponent<BackBengMei>().rightHand.transform.localPosition;
                BackBengMei.weaPonRightHand.localPosition = new Vector3(0f, 0f, 0f);
                BackBengMei.weaPonRightHand.localRotation = Quaternion.Euler(0f, 0f, 0f);
                BackBengMei.weaPonRightHand.gameObject.SetActive(true);
            }
            //ˢ�±���
            RefreshView();
        }
    }

    //�������� ������ť
    public void OnBgBtn()
    {
        mSelectItemId = -1;
        RefreshUseBtn();
    }

    //��ť ���� �ɹرհ�ť����
    public void OnCloseBtn()
    {
        GameManager.Instance.RemoveUI(gameObject);
    }


    void RefreshUseBtn()
    {
        //interactable �Ƿ�ɽ���
        if (mSelectItemId >= 0)
        {
            UseBtn.interactable = true;
        }
        else
        {
            UseBtn.interactable = false;
        }
    }


    void RefreshView()
    {
        //��ȡ����������
        Dictionary<int, int> bagOutfitIdAndCount = BagSystem.Instance.dicOutfitIdAndCounts;
        if (!bagOutfitIdAndCount.ContainsKey(mSelectItemId))
        {
            mSelectItemId = -1;
            RefreshUseBtn();
        }

        //ˢ�½���
        RectTransform rt = Content.GetComponent<RectTransform>();
        for (int i = 0; i < rt.childCount; i++)
        {
            GameObject.Destroy(rt.GetChild(i).gameObject);
        }

        //������Ʒ ��Ʒ��ʾ��������
        foreach (KeyValuePair<int, int> outfitIdAndCount in bagOutfitIdAndCount)
        {
            for (int i = 0; i < outfitIdAndCount.Value; i++)
            {
                GameObject outfitUiObjCopy = GameObject.Instantiate<GameObject>(outfitUiObj);
                outfitUiObjCopy.SetActive(true);
                outfitUiObjCopy.GetComponent<RectTransform>().SetParent(rt);
                ItemSelectCtrl ItemSelectCtrlScripts = outfitUiObjCopy.GetComponent<ItemSelectCtrl>();
                ItemSelectCtrlScripts.SetData(outfitIdAndCount.Key, 1 /*outfitIdAndCount.Value*/);
            }
        }
    }


    public void OnBagItemSelect(MyEvent evt)
    {
        BagEvtData data = evt.data as BagEvtData;
        if (data != null)
        {
            mSelectItemId = data.id;
            RefreshUseBtn();
        }
    }
}
