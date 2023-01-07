using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Config;


public class ItemSelectCtrl : MonoBehaviour
{

    //图标
    public Image Icon;

    //名字
    public Text Name;

    //数量
    public Text Count;

    //物品配置信息
    protected ExcelsToOutFitInformation excelsToOutFitInformation;
    protected int mOutfitCount;

    //是否已经拾取
    protected bool mFlag;


    protected void Awake()
    {
        excelsToOutFitInformation = null;
        mOutfitCount = -1;
        mFlag = false;
    }


    protected void Start()
    {

    }


    protected void Update()
    {
        
    }


    //选择按钮的点击响应
    public void OnSelectBtn()
    {
        if (mOutfitCount > 0 && !mFlag)
        {
            BagSystem.Instance.AddItem(excelsToOutFitInformation.Id, mOutfitCount);
            mFlag = true;
        }
    }


    public void SetData(int id, int count)
    {
        excelsToOutFitInformation = ExcelsToItemHelpClass.Instance.IdToFindOutfitInformation(id);
        mOutfitCount = count;
        Refresh();
    }


    public void Refresh()
    {
        if (excelsToOutFitInformation != null)
        {
            //显示物品的名字
            Name.text = excelsToOutFitInformation.Name;

            //显示物品的数量
            Count.text = mOutfitCount.ToString();

            //显示物品的图标
            Icon.sprite = Resources.Load<Sprite>("Pics/" + excelsToOutFitInformation.Icon);
        }
    }
}
