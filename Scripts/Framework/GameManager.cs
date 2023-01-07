using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//游戏管理者
public class GameManager
{
    static GameManager sInstance = null;
    public static GameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                sInstance = new GameManager();
            }
            return sInstance;
        }
    }
    //UI根节点
    GameObject mUiRoot;
    //游戏根节点
    GameObject mGameRoot;
    //ui层链表
    List<GameObject> mUis;
    //当前游戏层
    GameObject mCurLevel;


    GameManager()
    {
        mUis = new List<GameObject>();
        mCurLevel = null;
    }


    public GameObject UIRoot
    {
        get
        {
            return mUiRoot;
        }
    }


    public GameObject GameRoot
    {
        get
        {
            return mGameRoot;
        }
    }


    public void init(GameObject uiRoot, GameObject gameRoot)
    {
        mUiRoot = uiRoot;
        mGameRoot = gameRoot;

        if (uiRoot == null || gameRoot == null)
        {
            throw new System.Exception("GameManager不能为空!");
        }
    }


    //检测此UI是否为顶部UI
    public bool CheckIsTop(string uiName)
    {
        if (mUis.Count > 0 && mUis[mUis.Count - 1].name.StartsWith(uiName))
        {
            return true;
        }

        return false;
    }

    //获取顶部Ui
    public GameObject GetTopUI()
    {
        if (mUis.Count == 0)
        {
            return null;
        }

        return mUis[mUis.Count - 1];
    }


    //加载Ui
    public bool LoadUI(string uiPath)
    {
        int idx = uiPath.LastIndexOf('/');
        string uiName = uiPath.Substring(idx + 1, uiPath.Length - 1 - idx);

        if (CheckIsTop(uiName))
        {
            return false;
        }

        GameObject obj = Resources.Load<GameObject>(uiPath);
        obj = GameObject.Instantiate<GameObject>(obj);

        obj.GetComponent<RectTransform>().SetParent(mUiRoot.transform);
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);

        mUis.Add(obj);
        obj.GetComponent<Canvas>().sortingOrder = mUis.Count;

        return true;
    }


    //删除顶层UI
    public bool RemoveUI(GameObject obj)
    {
        //定位UI在链表中的位置
        int idx = mUis.IndexOf(obj);

        //判断此Ui是否处于顶层
        if (idx < mUis.Count - 1)
        {
            return false;
        }

        //根据下标删除链表中的UI
        mUis.RemoveAt(idx);

        //删除游戏对象
        GameObject.Destroy(obj);

        return true;
    }


    //加载游戏关卡
    public bool LoadLevel(string levelPath)
    {
        int idx = levelPath.LastIndexOf('/');
        string levelName = levelPath.Substring(idx + 1, levelPath.Length - 1 - idx);

        if (mCurLevel != null && mCurLevel.name.StartsWith(levelName))
        {
            return false;
        }

        RemoveLevel();

        GameObject obj = Resources.Load<GameObject>(levelPath);
        obj = GameObject.Instantiate<GameObject>(obj);

        obj.transform.parent = mGameRoot.transform;
        mCurLevel = obj;

        return true;
    }


    //销毁当前场景
    public void RemoveLevel()
    {
        if (mCurLevel != null)
        {
            GameObject.Destroy(mCurLevel);
            mCurLevel = null;
        }
    }


    public int GetThisUiIsDontDestroyUiCount()
    {
        return mUis.Count;
    }

}

