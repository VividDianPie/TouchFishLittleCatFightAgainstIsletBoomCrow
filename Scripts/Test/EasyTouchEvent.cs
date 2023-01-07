using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyTouchEvent : MonoBehaviour
{
    public static string buttonAState;//Ui���� A ״̬ -  APressed  AUp
    public static string buttonEvadeState;//Ui���� Evade ״̬ EvadeDown  EvadePressed  EvadeUp
    public static string buttonJumpState;//Ui���� Jump ״̬ JumpDown  JumpPressed  JumpUp
    public static string buttonEState;//Ui���� E ״̬ EDown  EPressed  EUp
    public static string buttonQState;//Ui���� Q ״̬ QDown  QPressed  QUp
    // ���ⰴ�� A
    void Update()
    {

        if (Time.timeScale <= 0.01f)
        {
            if (GameManager.Instance.CheckIsTop("ExplanationPopUi") == true) { return; }
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else
        {
            if (transform.GetChild(0).gameObject.activeInHierarchy == true) { return; }

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void ButtonADown()
    {
        buttonAState = "ADown";
    }
    public void ButtonAPressed()
    {
        buttonAState = "APressed";
    }
    public void ButtonAUp()
    {
        buttonAState = "AUp";
    }

    // ���ⰴ�� Evade
    public void ButtonEvadeDown()
    {
        buttonEvadeState = "EvadeDown";
    }
    public void ButtonEvadePressed()
    {
        buttonEvadeState = "EvadePressed";
    }
    public void ButtonEvadeUp()
    {
        buttonEvadeState = "EvadeUp";
    }

    // ���ⰴ�� Jump
    public void ButtonJumpDown()
    {
        buttonJumpState = "JumpDown";
    }
    public void ButtonJumpPressed()
    {
        buttonJumpState = "JumpPressed";
    }
    public void ButtonJumpUp()
    {
        buttonJumpState = "JumpUp";
    }


    // ���ⰴ�� E
    public void ButtonEDown()
    {
        buttonEState = "EDown";
    }
    public void ButtonEPressed()
    {
        buttonEState = "EPressed";
    }
    public void ButtonEUp()
    {
        buttonEState = "EUp";
    }


    // ���ⰴ�� Q
    public void ButtonQDown()
    {
        buttonQState = "QDown";
    }
    public void ButtonQPressed()
    {
        buttonQState = "QPressed";
    }
    public void ButtonQUp()
    {
        buttonQState = "QUp";
    }

}
