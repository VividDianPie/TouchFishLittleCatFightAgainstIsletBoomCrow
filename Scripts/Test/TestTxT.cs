using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchRotateScale : MonoBehaviour
{

    // ��������Ҫ��unity��touch�¼���Ӧ�ã������һ�����ת��ɫ���Ŵ���С��ɫ���ж��󻬻����һ�

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

        //���㴥��
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = touch.deltaPosition;
            //������ת��
            Player.transform.Rotate(Vector3.down * pos.x, Space.World);
            //������ת
            Player.transform.Rotate(Vector3.right * pos.y, Space.World);



        }



        //���㴥��
        Touch newTouch1 = Input.GetTouch(0);
        Touch newTouch2 = Input.GetTouch(1);

        //�ڶ��㿪ʼ���д���,����ֻ����¼�������д���
        if (newTouch2.phase == TouchPhase.Began)
        {
            oldTouch1 = newTouch1;
            oldTouch2 = newTouch2;
            return;
        }

        //��ʱ�����Ѿ���ʼ���ƶ�

        //�����ϵ����������µ��������룬���Ҫ�Ŵ�ģ�ͣ���СҪ����ģ��  
        float oldDistance = Vector2.Distance(oldTouch1.position, oldTouch2.position);
        float newDistance = Vector2.Distance(newTouch1.position, newTouch2.position);
        //��������֮�Ϊ����ʾ�Ŵ����ƣ� Ϊ����ʾ��С����  
        float offset = newDistance - oldDistance;
        //�Ŵ����ӣ� һ�����ذ� 0.01������(100�ɵ���)  
        float scaleFactor = offset / 100;

        Vector3 scale = new Vector3(Player.transform.localScale.x + scaleFactor,
            Player.transform.localScale.y + scaleFactor,
            Player.transform.localScale.z + scaleFactor);

        //���������õ���С���ŵ� 0.3 �� �����ݸ�����Ŀ������ 
        if (scale.x > 0.3f && scale.y > 0.3f && scale.z > 0.3f)
        {
            Player.transform.localScale = scale;
        }

        //��ס���µĴ����㣬�´�ʹ��  
        oldTouch1 = newTouch1;
        oldTouch2 = newTouch2;

        //�жϻ�������
        TouchDirection();

    }
    //---------------�ж������һ���----------------------
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
                //�жϻ����ķ���(���һ����������»���)

                if (Mathf.Abs(beginPos.x = pos.x) > Mathf.Abs(beginPos.y = pos.y))//���һ���
                {
                    if (beginPos.x > pos.x)
                    {
                        Debug.Log("���󻬶�");
                        Tips.text = "���󻬶�";
                    }
                    else
                    {
                        Debug.Log("���һ���");
                        Tips.text = "���һ���";

                    }
                }
                else  //���»���
                {
                    if (beginPos.y > pos.y)
                    {
                        Debug.Log("���»���");
                        Tips.text = "���»���";

                    }
                    else
                    {
                        Debug.Log("���ϻ���");
                        Tips.text = "���ϻ���";

                    }
                }
            }
        }

    }
}

