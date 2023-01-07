using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    //要爆的物品ID
    [Header("要爆的物品ID")]
    public int[] ItemIds;

    //要爆的物品的数量
    [Header("要爆的物品的数量")]
    public int[] ItemNum;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }


    //被碰到的时候
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<BackBengMei>() != null)
        {
            //Debug.Log("Box OnTriggerEnter");
            ItemSelectPopup isp = null;
            GameObject ui = null;

            //if (GameManager.Instance.CheckIsTop("ItemSelectPopup"))
            //{
            //    ui = GameManager.Instance.GetTopUI();
            //    isp = ui.GetComponent<ItemSelectPopup>();

            //    //清除所有的物品
            //    isp.ClearView();
            //}
            //else
            //{
                GameManager.Instance.LoadUI("Uis/ItemSelectPopup");
                ui = GameManager.Instance.GetTopUI();
                isp = ui.GetComponent<ItemSelectPopup>();
            //}

            //宝箱中物品设置到选择界面
            for (int i = 0; i < ItemIds.Length; i++)
            {
                isp.AddItem(ItemIds[i], ItemNum[i]);
            }
        }
    }
}
