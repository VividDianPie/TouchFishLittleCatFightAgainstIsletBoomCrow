using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyTouchEvent : MonoBehaviour
{
    public static string buttonAState;//Ui°´¼ü A ×´Ì¬ -  APressed  AUp
    public static string buttonEvadeState;//Ui°´¼ü Evade ×´Ì¬ EvadeDown  EvadePressed  EvadeUp
    public static string buttonJumpState;//Ui°´¼ü Jump ×´Ì¬ JumpDown  JumpPressed  JumpUp
    public static string buttonEState;//Ui°´¼ü E ×´Ì¬ EDown  EPressed  EUp
    public static string buttonQState;//Ui°´¼ü Q ×´Ì¬ QDown  QPressed  QUp
    // ÐéÄâ°´¼ü A
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

    // ÐéÄâ°´¼ü Evade
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

    // ÐéÄâ°´¼ü Jump
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


    // ÐéÄâ°´¼ü E
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


    // ÐéÄâ°´¼ü Q
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
