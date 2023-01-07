using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectPopup : MonoBehaviour
{

    //用来挂在选择小物件
    public RectTransform Content;

    //选择物品空间的模版
    public GameObject ItemSelectCtrlTmp;

    //key:物品id value:数量
    Dictionary<int, int> mItems;


    void Awake()
    {
        mItems = new Dictionary<int, int>();
    }


    void Start()
    {
        
    }


    void Update()
    {
        
    }


    //添加物品
    public void AddItem(int id, int count)
    {
        if (mItems.ContainsKey(id))
        {
            return;
        }

        mItems.Add(id, count);

        Refresh();
    }


    public void ClearView()
    {
        int c = Content.childCount;
        for (int i = 0; i < c; i++)
        {
            GameObject.Destroy(Content.GetChild(i).gameObject);
        }

        mItems.Clear();
    }


    //刷新界面
    public void Refresh()
    {
        //清空Content
        int c = Content.childCount;
        for (int i = 0; i < c; i++)
        {
            GameObject.Destroy(Content.GetChild(i).gameObject);
        }

        //克隆我们上面ItemSelectCtrlTmp挂在到Content
        foreach (var kv in mItems)
        {
            //复制游戏物体
            GameObject isc = GameObject.Instantiate<GameObject>(ItemSelectCtrlTmp);

            //把游戏物体设置激活状态
            isc.SetActive(true);

            //挂在到content
            isc.GetComponent<RectTransform>().SetParent(Content);

            //取下ItemSelectCtrl设置id和数量
            ItemSelectCtrl isccs = isc.GetComponent<ItemSelectCtrl>();
            isccs.SetData(kv.Key, kv.Value);
        }
    }


    //关闭按钮的响应
    public void OnCloseBtn()
    {
        GameManager.Instance.RemoveUI(this.gameObject);
    }
}
