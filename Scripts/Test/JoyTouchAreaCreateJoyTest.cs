using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//�̳нӿ�ʵ�ֽӿ�
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




    ////����϶�
    //public void OnDrag(PointerEventData eventData)
    //{
    //    Debug.Log("OnDrag");

    //}

    ////��갴��
    //public void OnPointerDown(PointerEventData eventData)
    //{

    //    Debug.Log("OnPointerDown");


    //}

    ////���̧��
    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    Debug.Log("OnPointerUp");


    //}

















    ///// ������ָ���������� UI������Ϊ�����ж�����   ��*��ͼ˵�ĺ��
    //[Header("������ָ���������� UI������Ϊ�����ж�����")]
    //public Transform _map;
    //float _mapWidth;
    //float _mapHight;
    ///// ��ȡui����Ļ����
    ///// <param name="trans">UI����</param>
    //private Vector2 GetUiToScreenPos(Transform trans)
    //{
    //    _mapWidth = trans.GetComponent<RectTransform>().rect.width;//��ȡui��ʵ�ʿ��
    //    _mapHight = trans.GetComponent<RectTransform>().rect.height;//����
    //    Vector2 pos2D = trans.position;
    //    return pos2D;
    //}

    ///// �ж��Ƿ���ui��
    ///// <param name="pos">�����������Ϣ(����������)</param>
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


    ////���� rawPosition����ܹؼ������ڴ�����ԭʼλ�á����ʹ�õ���position����ô��ζ�Ŵ�������һ���뿪ָ�����򽫻���Ч��
    //IsTouchInUi(Input.GetTouch(0).rawPosition)


     















}








