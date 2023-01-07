using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



/*
C:/Users/16179/AppData/Local/Temp/DefaultCompany/ToCastATreacherousVerdictUponFate1_2
          配置Excels表格数据 通过 自定义编辑器扩展基于动态链接库 将Excels数据转换成bite数据 通过对象逆向序列化 转换成代码对应数据类型
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
    //使用按钮
    public Button UseBtn;

    //scroll content
    public GameObject Content;

    //ItemSelectCtrl
    public GameObject outfitUiObj;

    //当前选中的物品
    int mSelectItemId;


    //装备物品选择 监听者 事件数据为 选中物品的Id 与 Id对应物品的数量
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

    //按钮函数 此函数由使用按钮触发
    public void OnUseBtn()
    {
        if (mSelectItemId > 0)
        {
            //使用物品
            BagSystem.Instance.AddItem(mSelectItemId, -1);

            if (mSelectItemId == 1001)//蒂蒂的虎鲸抱枕
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
            else if (mSelectItemId == 1002)//雾切之回光
            {
                Destroy(BackBengMei.weaPonLeftHand.gameObject);
                Destroy(BackBengMei.weaPonRightHand.gameObject);

                BackBengMei.weaPonLeftHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/雾切之回光"));
                BackBengMei.weaPonLeftHand.parent = BackBengMei.heroActor.GetComponent<BackBengMei>().ledtHand.transform;
                BackBengMei.weaPonLeftHand.transform.localPosition =
                    BackBengMei.heroActor.GetComponent<BackBengMei>().ledtHand.transform.localPosition;
                BackBengMei.weaPonLeftHand.localPosition = new Vector3(0.024f, 0.013f, -0.018f);
                BackBengMei.weaPonLeftHand.localRotation = Quaternion.Euler(181.176f, 306.569f, 168.572f);

                BackBengMei.weaPonRightHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/雾切之回光"));
                BackBengMei.weaPonRightHand.parent = BackBengMei.heroActor.GetComponent<BackBengMei>().rightHand.transform;
                BackBengMei.weaPonRightHand.transform.localPosition =
                    BackBengMei.heroActor.GetComponent<BackBengMei>().rightHand.transform.localPosition;
                BackBengMei.weaPonRightHand.localPosition = new Vector3(0f, 0f, 0f);
                BackBengMei.weaPonRightHand.localRotation = Quaternion.Euler(0f, 0f, 0f);
                BackBengMei.weaPonRightHand.gameObject.SetActive(true);
            }
            else if (mSelectItemId == 1003)//魂妖刀_血樱寂灭
            {
                Destroy(BackBengMei.weaPonLeftHand.gameObject);
                Destroy(BackBengMei.weaPonRightHand.gameObject);

                BackBengMei.weaPonLeftHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/魂妖刀_血樱寂灭"));
                BackBengMei.weaPonLeftHand.parent = BackBengMei.heroActor.GetComponent<BackBengMei>().ledtHand.transform;
                BackBengMei.weaPonLeftHand.transform.localPosition =
                    BackBengMei.heroActor.GetComponent<BackBengMei>().ledtHand.transform.localPosition;
                BackBengMei.weaPonLeftHand.localPosition = new Vector3(0.024f, 0.013f, -0.018f);
                BackBengMei.weaPonLeftHand.localRotation = Quaternion.Euler(181.176f, 306.569f, 168.572f);

                BackBengMei.weaPonRightHand = GameObject.Instantiate(Resources.Load<Transform>("Prefab/魂妖刀_血樱寂灭"));
                BackBengMei.weaPonRightHand.parent = BackBengMei.heroActor.GetComponent<BackBengMei>().rightHand.transform;
                BackBengMei.weaPonRightHand.transform.localPosition =
                    BackBengMei.heroActor.GetComponent<BackBengMei>().rightHand.transform.localPosition;
                BackBengMei.weaPonRightHand.localPosition = new Vector3(0f, 0f, 0f);
                BackBengMei.weaPonRightHand.localRotation = Quaternion.Euler(0f, 0f, 0f);
                BackBengMei.weaPonRightHand.gameObject.SetActive(true);
            }
            //刷新背包
            RefreshView();
        }
    }

    //背包背景 背景按钮
    public void OnBgBtn()
    {
        mSelectItemId = -1;
        RefreshUseBtn();
    }

    //按钮 函数 由关闭按钮触发
    public void OnCloseBtn()
    {
        GameManager.Instance.RemoveUI(gameObject);
    }


    void RefreshUseBtn()
    {
        //interactable 是否可交互
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
        //获取背包的数据
        Dictionary<int, int> bagOutfitIdAndCount = BagSystem.Instance.dicOutfitIdAndCounts;
        if (!bagOutfitIdAndCount.ContainsKey(mSelectItemId))
        {
            mSelectItemId = -1;
            RefreshUseBtn();
        }

        //刷新界面
        RectTransform rt = Content.GetComponent<RectTransform>();
        for (int i = 0; i < rt.childCount; i++)
        {
            GameObject.Destroy(rt.GetChild(i).gameObject);
        }

        //背包物品 物品显示数量控制
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
