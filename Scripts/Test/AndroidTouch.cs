using UnityEngine;
using System.Collections;


//������ű�������������ϣ����ֵ��������������������ƶ��������˫���������������š�
public class AndroidTouch : MonoBehaviour
{

    private int isforward;//�����������ƶ�����
    //��¼������ָ�ľ�λ��
    private Vector2 oposition1 = new Vector2();
    private Vector2 oposition2 = new Vector2();

    Vector2 m_screenPos = new Vector2(); //��¼��ָ������λ��

    //�����ж��Ƿ�Ŵ�
    bool isEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        //����������һ�δ��������λ���뱾�δ��������λ�ü�����û�������
        float leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
        float leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
        if (leng1 < leng2)
        {
            //�Ŵ�����
            return true;
        }
        else
        {
            //��С����
            return false;
        }
    }

    void Start()
    {
        Input.multiTouchEnabled = true;//������㴥��
    }

    void Update()
    {
        if (Input.touchCount <= 0)
            return;
        if (Input.touchCount == 1) //���㴥���ƶ������
        {
            if (Input.touches[0].phase == TouchPhase.Began)
                m_screenPos = Input.touches[0].position;   //��¼��ָ�մ�����λ��
            if (Input.touches[0].phase == TouchPhase.Moved) //��ָ����Ļ���ƶ����ƶ������
            {
                transform.Translate(new Vector3(Input.touches[0].deltaPosition.x * Time.deltaTime, Input.touches[0].deltaPosition.y * Time.deltaTime, 0));
            }
        }

        else if (Input.touchCount > 1)//��㴥��
        {
            //��¼������ָ��λ��
            Vector2 nposition1 = new Vector2();
            Vector2 nposition2 = new Vector2();

            //��¼��ָ��ÿ֡�ƶ�����
            Vector2 deltaDis1 = new Vector2();
            Vector2 deltaDis2 = new Vector2();

            for (int i = 0; i < 2; i++)
            {
                Touch touch = Input.touches[i];
                if (touch.phase == TouchPhase.Ended)
                    break;
                if (touch.phase == TouchPhase.Moved) //��ָ���ƶ�
                {

                    if (i == 0)
                    {
                        nposition1 = touch.position;
                        deltaDis1 = touch.deltaPosition;
                    }
                    else
                    {
                        nposition2 = touch.position;
                        deltaDis2 = touch.deltaPosition;

                        if (isEnlarge(oposition1, oposition2, nposition1, nposition2)) //�ж����������Ӷ����������ǰ���ƶ���������Ч��
                            isforward = 1;
                        else
                            isforward = -1;
                    }
                    //��¼�ɵĴ���λ��
                    oposition1 = nposition1;
                    oposition2 = nposition2;
                }
                //�ƶ������
                Camera.main.transform.Translate(isforward * Vector3.forward * Time.deltaTime * (Mathf.Abs(deltaDis2.x + deltaDis1.x) + Mathf.Abs(deltaDis1.y + deltaDis2.y)));
            }
        }
    }
}