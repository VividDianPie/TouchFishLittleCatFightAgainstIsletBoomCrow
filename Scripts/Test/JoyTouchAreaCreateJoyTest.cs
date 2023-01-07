using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//继承接口实现接口
public class JoyTouchAreaCreateJoyTest : MonoBehaviour//, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    void Start()
    {

    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Debug.Log(" TouchPhase.Began");
            }
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                Debug.Log(" TouchPhase.Moved");
            }
            if (Input.touches[0].phase == TouchPhase.Stationary)
            {
                Debug.Log(" TouchPhase.Stationary");
            }
            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                Debug.Log(" TouchPhase.Ended");
            }
            if (Input.touches[0].phase == TouchPhase.Canceled)
            {
                Debug.Log(" TouchPhase.Canceled");
            }
        }

    }




    ////鼠标拖动
    //public void OnDrag(PointerEventData eventData)
    //{
    //    Debug.Log("OnDrag");

    //}

    ////鼠标按下
    //public void OnPointerDown(PointerEventData eventData)
    //{

    //    Debug.Log("OnPointerDown");


    //}

    ////鼠标抬起
    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    Debug.Log("OnPointerUp");


    //}

















    ///// 锁定手指触摸的区域 UI对象作为区域判断条件   （*上图说的红框）
    //[Header("锁定手指触摸的区域 UI对象作为区域判断条件")]
    //public Transform _map;
    //float _mapWidth;
    //float _mapHight;
    ///// 获取ui的屏幕坐标
    ///// <param name="trans">UI物体</param>
    //private Vector2 GetUiToScreenPos(Transform trans)
    //{
    //    _mapWidth = trans.GetComponent<RectTransform>().rect.width;//获取ui的实际宽度
    //    _mapHight = trans.GetComponent<RectTransform>().rect.height;//长度
    //    Vector2 pos2D = trans.position;
    //    return pos2D;
    //}

    ///// 判断是否在ui上
    ///// <param name="pos">输入的坐标信息(触摸点数据)</param>
    //public bool IsTouchInUi(Vector3 pos)
    //{
    //    bool isInRect = false;
    //    Vector3 newPos = GetUiToScreenPos(_map);
    //    if (pos.x < (newPos.x + _mapWidth) && pos.x > newPos.x &&
    //        pos.y < (newPos.y + _mapHight) && pos.y > newPos.y)
    //    {
    //        isInRect = true;
    //    }
    //    return isInRect;
    //}


    ////调用 rawPosition这个很关键，用于触摸的原始位置。如果使用的是position，那么意味着触摸区域一旦离开指定区域将会无效；
    //IsTouchInUi(Input.GetTouch(0).rawPosition)


     















}








