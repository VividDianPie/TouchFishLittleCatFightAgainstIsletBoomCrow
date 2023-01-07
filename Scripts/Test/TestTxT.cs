using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchRotateScale : MonoBehaviour
{

    // 本案例主要是unity中touch事件的应用，如左右滑动旋转角色，放大缩小角色，判断左滑还是右滑

    public GameObject Player;

    Touch oldTouch1, oldTouch2;

    Vector2 beginPos;
    public Text Tips;
    void Start()
    {

    }


    void Update()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }

        //单点触摸
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = touch.deltaPosition;
            //左右旋转：
            Player.transform.Rotate(Vector3.down * pos.x, Space.World);
            //上下旋转
            Player.transform.Rotate(Vector3.right * pos.y, Space.World);



        }



        //两点触摸
        Touch newTouch1 = Input.GetTouch(0);
        Touch newTouch2 = Input.GetTouch(1);

        //第二点开始进行触摸,但是只做记录，不进行处理
        if (newTouch2.phase == TouchPhase.Began)
        {
            oldTouch1 = newTouch1;
            oldTouch2 = newTouch2;
            return;
        }

        //此时两点已经开始有移动

        //计算老的两点距离和新的两点间距离，变大要放大模型，变小要缩放模型  
        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);
        //两个距离之差，为正表示放大手势， 为负表示缩小手势  
        float offset = newDistance - oldDistance;
        //放大因子， 一个像素按 0.01倍来算(100可调整)  
        float scaleFactor = offset / 100;

        Vector3 scale = new Vector3(Player.transform.localScale.x + scaleFactor,
            Player.transform.localScale.y + scaleFactor,
            Player.transform.localScale.z + scaleFactor);

        //我这里设置的最小缩放到 0.3 倍 （根据个人项目调整） 
        if (scale.x > 0.3f && scale.y > 0.3f && scale.z > 0.3f)
        {
            Player.transform.localScale = scale;
        }

        //记住最新的触摸点，下次使用  
        oldTouch1 = newTouch1;
        oldTouch2 = newTouch2;

        //判断滑动方向
        TouchDirection();

    }
    //---------------判断是左滑右滑等----------------------
    void TouchDirection()
    {
        if (Input.touchCount <= 0)
        {
            return;
        }

        if (Input.touchCount == 1)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                beginPos = Input.GetTouch(0).deltaPosition;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended && Input.GetTouch(0).phase != TouchPhase.Canceled)
            {
                Vector2 pos = Input.GetTouch(0).deltaPosition;
                //判断滑动的方向(左右滑动还是上下滑动)

                if (Mathf.Abs(beginPos.x = pos.x) > Mathf.Abs(beginPos.y = pos.y))//左右滑动
                {
                    if (beginPos.x > pos.x)
                    {
                        Debug.Log("向左滑动");
                        Tips.text = "向左滑动";
                    }
                    else
                    {
                        Debug.Log("向右滑动");
                        Tips.text = "向右滑动";

                    }
                }
                else  //上下滑动
                {
                    if (beginPos.y > pos.y)
                    {
                        Debug.Log("向下滑动");
                        Tips.text = "向下滑动";

                    }
                    else
                    {
                        Debug.Log("向上滑动");
                        Tips.text = "向上滑动";

                    }
                }
            }
        }

    }
}

