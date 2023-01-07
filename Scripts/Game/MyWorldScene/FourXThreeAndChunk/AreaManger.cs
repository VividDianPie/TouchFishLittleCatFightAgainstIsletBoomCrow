using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManger : MonoBehaviourSingleton<AreaManger>
{
    FourXTree m4XTree;
    GameObject mChunk;
    GameObject mMapRoot;

    [Header("�����")]
    public int chunk_Width;
    [Header("������Χ���ɲ���")]
    public int activeLevel;
    [Header("�Ĳ�����")]
    public int fourXTreeHeight;
    public int RandSeed;


    private Vector3 focusOldPos { get; set; }
    public List<FourXTreeNode> activeAreaList { get; set; }
    public List<FourXTreeNode> inactiveAreaList { get; set; }
    public GameObject focusObject;
    private Coroutine mCurCoroutine;
    private bool Inited = false;


    public void Awake()
    {
        //�������
        Random.InitState(RandSeed);
        Chunk.randOff = new Vector2(Random.value, Random.value);
        //����Block Awake
        Destroy(Instantiate(Resources.Load<GameObject>("Prefab/Block")));
        //ħ�ȵ���
        AreaManger tempInstance = AreaManger.Instance;
        m4XTree = new FourXTree();
        m4XTree.InitTree(fourXTreeHeight, chunk_Width);
        activeAreaList = new List<FourXTreeNode>();
        inactiveAreaList = new List<FourXTreeNode>();
        mChunk = Resources.Load<GameObject>("Prefab/Chunk");
        mChunk.GetComponent<Chunk>().chunkWidth = chunk_Width;
        mMapRoot = GameObject.Find("Map");
        mCurCoroutine = null;
        Inited = true;
    }

    public void Update()
    {
        if (focusObject == null || Inited == false) { return; }
        if (Mathf.Abs(focusObject.transform.position.x - focusOldPos.x) > 20f ||
            Mathf.Abs(focusObject.transform.position.z - focusOldPos.z) > 20f)
        {
            focusOldPos = focusObject.transform.position;
            if (mCurCoroutine != null)
            {
                StopCoroutine(mCurCoroutine);
            }
            mCurCoroutine = StartCoroutine(CheckAround(focusOldPos));
        }
    }



    public IEnumerator CheckAround(Vector3 pos)
    {
        FourXTreeNode centerNode = m4XTree.FindLastNode(new Vector2(pos.x, pos.z));
        if (centerNode == null) { yield break; }
        if (activeAreaList.Count > 0)
        {
            foreach (FourXTreeNode treeNode in activeAreaList)
            {
                FourXTree.fourXTreeNodeList[treeNode.Index].isActive = false;
                if (inactiveAreaList.Contains(FourXTree.fourXTreeNodeList[treeNode.Index]) == false)
                {
                    inactiveAreaList.Add(FourXTree.fourXTreeNodeList[treeNode.Index]);
                }
            }
        }
        activeAreaList.Clear();
        CreateNodeChunk(centerNode);
        FourXTreeNode around;
        for (int i = 1; i <= activeLevel; i++)
        {
            //�������
            around = m4XTree.FindLastNode(centerNode.nodeRect.position + i * Vector2.up * chunk_Width);//��
            CreateNodeChunk(around);
            around = m4XTree.FindLastNode(centerNode.nodeRect.position + i * Vector2.down * chunk_Width);//��
            CreateNodeChunk(around);
            around = m4XTree.FindLastNode(centerNode.nodeRect.position + i * Vector2.left * chunk_Width);//��
            CreateNodeChunk(around);
            around = m4XTree.FindLastNode(centerNode.nodeRect.position + i * Vector2.right * chunk_Width);//��
            CreateNodeChunk(around);
            //б�����
            for (int j = 1; j <= activeLevel; j++)
            {
                around = m4XTree.FindLastNode(centerNode.nodeRect.position +          //����
                    i * Vector2.down * chunk_Width + j * Vector2.left * chunk_Width);
                CreateNodeChunk(around);

                around = m4XTree.FindLastNode(centerNode.nodeRect.position +          //����
                    i * Vector2.up * chunk_Width + j * Vector2.left * chunk_Width);
                CreateNodeChunk(around);

                around = m4XTree.FindLastNode(centerNode.nodeRect.position +          //����
                    i * Vector2.up * chunk_Width + j * Vector2.right * chunk_Width);
                CreateNodeChunk(around);

                around = m4XTree.FindLastNode(centerNode.nodeRect.position +          //����
                    i * Vector2.down * chunk_Width + j * Vector2.right * chunk_Width);
                CreateNodeChunk(around);
                yield return null;
            }
        }
        // ������� ��������ǻ�Ծ��
        StopCoroutine(InactiveFade());
        StartCoroutine(InactiveFade());
    }



    public IEnumerator InactiveFade()
    {
        while (inactiveAreaList.Count > 0)
        {
            if (inactiveAreaList[0].isActive == true)
            {
                inactiveAreaList.RemoveAt(0);
                continue;
            }

            foreach (var item in inactiveAreaList[0].nodeObjList)
            {
                item.SetActive(false);
            }
            inactiveAreaList[0].nodeObjList.Clear();
            inactiveAreaList[0].nodeChunkScripts.gameObject.SetActive(false);
            inactiveAreaList.RemoveAt(0);
            yield return null;
        }
    }



    public void CreateNodeChunk(FourXTreeNode centreNode)
    {
        if (centreNode == null) { return; }
        if (centreNode.nodeChunkScripts == null)
        {
            GameObject chunkObj = UnityEngine.Object.Instantiate(mChunk, mMapRoot.transform);
            chunkObj.tag = mMapRoot.tag;
            chunkObj.transform.position = new Vector3(centreNode.nodeRect.x, 0f, centreNode.nodeRect.y);
            Chunk chunkScripts = chunkObj.GetComponent<Chunk>();
            chunkScripts.chunkWidth = chunk_Width;

            centreNode.nodeChunkScripts = chunkScripts;
            centreNode.nodeChunkScripts.Index = centreNode.Index;
        }
        if (centreNode.isActive == false)
        {
            centreNode.isActive = true;
            centreNode.nodeChunkScripts.gameObject.SetActive(true);
        }
        activeAreaList.Add(centreNode);
    }


    public void HitTerrain(Vector3 pos)
    {
        FourXTreeNode curNode = m4XTree.FindLastNode(new Vector2(pos.x, pos.z));
        if (curNode.nodeChunkScripts != null)
        {
            curNode.nodeChunkScripts.DestroyBlock(pos);
        }
    }


}
