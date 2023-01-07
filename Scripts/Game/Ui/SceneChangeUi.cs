using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChangeUi : MonoBehaviour
{
    string buttonState;//BackGrundButton   MeiSceneButton   WorldButton
    string changeScene;//MyWorldScene    ThunderGodScene
    public Button goodButton;
    GameObject image;   //加载界面
    public GameObject meiSceneImage;
    public GameObject worldSceneImage;
    public GameObject slider;   //进度条
    public Text text;      //加载进度文本
    private void Awake()
    {
        EventManager.Instance.DispatchEvent(new MyEvent(EEventType.GameStop, null));
    }

    void OnDestroy()
    {
        EventManager.Instance.DispatchEvent(new MyEvent(EEventType.GameRun, null));
    }

    public void OnClothBtn()
    {
        GameManager.Instance.RemoveUI(gameObject);
    }

    private void Start()
    {
        buttonState = "";
        changeScene = "";
        goodButton.interactable = false;
    }
    public void BackGrundButton()
    {
        buttonState = "BackGrundButton";
        goodButton.interactable = false;
    }

    public void MeiSceneButton()
    {
        buttonState = "MeiSceneButton";
        goodButton.interactable = true;
    }


    public void WorldButton()
    {
        buttonState = "WorldButton";
        goodButton.interactable = true;
    }

    public void GoodButton()
    {
        if (buttonState == "MeiSceneButton")
        {
            image = meiSceneImage;
            changeScene = "ThunderGodScene";
            if (changeScene == SceneManager.GetActiveScene().name) { return; }
            StartCoroutine(ChangeScene());
        }
        else if (buttonState == "WorldButton")
        {
            image = worldSceneImage;
            changeScene = "MyWorldScene";
            if (changeScene == SceneManager.GetActiveScene().name) { return; }
            StartCoroutine(ChangeScene());
        }
    }
    IEnumerator ChangeScene()
    {
        image.SetActive(true);
        slider.SetActive(true);
        yield return new WaitForSeconds(0.0f);
        StartCoroutine(LoadLeaver());
    }
    IEnumerator LoadLeaver()
    {


        AsyncOperation operation = SceneManager.LoadSceneAsync(changeScene);
        TimerMgr.Instance.DeleteAllTimer();
        while (operation.isDone == false)
        {
            slider.GetComponent<Slider>().value = operation.progress;  //进度条与场景加载进度对应
            text.text = (operation.progress * 110) + "%";
            yield return null;
        }
        if (operation.isDone == true)
        {
            GameManager.Instance.RemoveUI(gameObject);
        }
    }
}
