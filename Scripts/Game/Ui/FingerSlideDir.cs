using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FingerSlideDir : MonoBehaviour
{
    static FingerSlideDir sInstance = null;
    public static FingerSlideDir Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new FingerSlideDir();
            }
            return sInstance;
        }
    }

    Vector2 beginPos;











    public float TouchDirectionX1()
    {
        if (Input.touchCount <= 0)
        {
            return 0;
        }

        if (Input.touchCount == 1/*|| Input.touchCount == 2*/)
        {
            //检测当前手指之下是否有Ui
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return 0;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                beginPos = Input.GetTouch(0).deltaPosition;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).phase != TouchPhase.Canceled)
            {
                Vector2 pos = Input.GetTouch(0).deltaPosition;
                //判断滑动的方向(左右滑动还是上下滑动)

                if (Mathf.Abs(beginPos.x = pos.x) > Mathf.Abs(beginPos.y = pos.y))//左右滑动
                {
                    if (beginPos.x > pos.x)
                    {
                        return -pos.x;
                    }
                    else
                    {
                        return pos.x;
                    }
                }
            }
        }

        return 0;
    }



    public float TouchDirectionX2()
    {
        if (Input.touchCount <= 0)
        {
            return 0;
        }

        if (Input.touchCount == 2)
        {
            List<RaycastResult> raycastResults0 = IsPointerOverGameObject(Input.GetTouch(0).position);

            for (int i = 0; i < raycastResults0.Count; i++)
            {
                //Debug.Log("0  " + raycastResults0[i].gameObject.name);
            }


            bool isJoy0 = false;
            for (int i = 0; i < raycastResults0.Count; i++)
            {
                if (raycastResults0[i].gameObject.name == "JoyStickBg" || raycastResults0[i].gameObject.name == "Stick")
                {
                    isJoy0 = true;
                    break;
                }
            }
            if (isJoy0 == false)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    beginPos = Input.GetTouch(0).deltaPosition;
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).phase != TouchPhase.Canceled)
                {
                    Vector2 pos = Input.GetTouch(0).deltaPosition;
                    //判断滑动的方向(左右滑动还是上下滑动)
                    if (Mathf.Abs(beginPos.x = pos.x) > Mathf.Abs(beginPos.y = pos.y))//左右滑动
                    {
                        if (beginPos.x > pos.x)
                        {
                            return -pos.x;
                        }
                        else
                        {
                            return pos.x;
                        }
                    }
                }

            }





            List<RaycastResult> raycastResults1 = IsPointerOverGameObject(Input.GetTouch(1).position);

            for (int i = 0; i < raycastResults1.Count; i++)
            {
                //Debug.Log("0  " + raycastResults1[i].gameObject.name);
            }


            bool isJoy1 = false;
            for (int i = 0; i < raycastResults1.Count; i++)
            {
                if (raycastResults1[i].gameObject.name == "JoyStickBg" || raycastResults1[i].gameObject.name == "Stick")
                {
                    isJoy1 = true;
                    break;
                }
            }
            if (isJoy1 == false)
            {
                if (Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    beginPos = Input.GetTouch(1).deltaPosition;
                }
                if (Input.GetTouch(1).phase == TouchPhase.Moved && Input.GetTouch(1).phase != TouchPhase.Canceled)
                {
                    Vector2 pos = Input.GetTouch(1).deltaPosition;
                    //判断滑动的方向(左右滑动还是上下滑动)
                    if (Mathf.Abs(beginPos.x = pos.x) > Mathf.Abs(beginPos.y = pos.y))//左右滑动
                    {
                        if (beginPos.x > pos.x)
                        {
                            return -pos.x;
                        }
                        else
                        {
                            return pos.x;
                        }
                    }
                }
            }



        }
        return 0;
    }











    public float TouchDirectionY1()
    {
        if (Input.touchCount <= 0)
        {
            return 0;
        }

        if (Input.touchCount == 1)
        {
            //检测当前手指之下是否有Ui
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return 0;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                beginPos = Input.GetTouch(0).deltaPosition;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(0).phase != TouchPhase.Canceled)
            {
                Vector2 pos = Input.GetTouch(0).deltaPosition;
                //判断滑动的方向(左右滑动还是上下滑动)

                if (Mathf.Abs(beginPos.x = pos.x) < Mathf.Abs(beginPos.y = pos.y))//上下滑动
                {

                    if (beginPos.y > pos.y)
                    {
                        return -pos.y;
                    }
                    else
                    {
                        return pos.y;
                    }
                }
            }
        }
        return 0;
    }




    public float TouchDirectionY2()
    {
        if (Input.touchCount <= 0)
        {
            return 0;
        }

        if (Input.touchCount == 2)
        {
            //检测当前手指之下是否有Ui
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId))
            {
                return 0;
            }

            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                beginPos = Input.GetTouch(1).deltaPosition;
            }
            if (Input.GetTouch(1).phase == TouchPhase.Moved && Input.GetTouch(1).phase != TouchPhase.Canceled)
            {
                Vector2 pos = Input.GetTouch(1).deltaPosition;
                //判断滑动的方向(左右滑动还是上下滑动)

                if (Mathf.Abs(beginPos.x = pos.x) < Mathf.Abs(beginPos.y = pos.y))//上下滑动
                {

                    if (beginPos.y > pos.y)
                    {
                        return -pos.y;
                    }
                    else
                    {
                        return pos.y;
                    }
                }
            }
        }
        return 0;
    }



    private List<RaycastResult> IsPointerOverGameObject(Vector2 mousePosition)
    {
        //创建一个点击事件
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = mousePosition;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        //向点击位置发射一条射线，检测是否点击UI
        EventSystem.current.RaycastAll(eventData, raycastResults);




        if (raycastResults.Count > 0)
        {
            return raycastResults;
        }
        else
        {
            return new List<RaycastResult>();
        }
    }


}
