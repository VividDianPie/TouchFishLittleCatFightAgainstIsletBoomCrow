using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//二维空间索引树
public class FourXTree : MonoBehaviour
{
    public static List<FourXTreeNode> fourXTreeNodeList = new List<FourXTreeNode>();
    private FourXTreeNode rootNode;
    private int maxHeight;
    public int chunkWidth;
    public int treeHeight;

    public FourXTree()
    {
        rootNode = null;
        maxHeight = 32;
        chunkWidth = 16;
        treeHeight = 0;
    }

    public void InitTree(int tree_weight, int chunk_width)
    {
        chunkWidth = chunk_width;
        treeHeight = Mathf.Clamp(tree_weight, 1, maxHeight);
        FourXTreeNode node = new FourXTreeNode();
        node.starEndIndex = new Vector2Int(0, 1);
        int widthCount = 1;
        for (int i = 1; i < treeHeight; i++)
        {
            node.starEndIndex.y <<= 2;
            widthCount <<= 1;
        }
        // 根节点矩形宽
        node.nodeRect.Set(0, 0, chunkWidth * widthCount, chunkWidth * widthCount);  
        rootNode = node;
        GenerateTree(rootNode, node.starEndIndex.y);
    }
    private void GenerateTree(FourXTreeNode fatherNode, int len)
    {
        fatherNode.Index = fourXTreeNodeList.Count;
        fourXTreeNodeList.Add(fatherNode);
        if (len <= 1)
        {
            return; 
        }
        len >>= 2;
        FourXTreeNode[] corners = new FourXTreeNode[4] { new FourXTreeNode(), new FourXTreeNode(), 
                                                                                              new FourXTreeNode(), new FourXTreeNode() };
        corners[0].nodeRect.Set(fatherNode.nodeRect.x, fatherNode.nodeRect.y, fatherNode.nodeRect.width / 2, fatherNode.nodeRect.height / 2);
        corners[0].starEndIndex.x = fatherNode.starEndIndex.x;
        corners[0].starEndIndex.y = fatherNode.starEndIndex.x + len;
        corners[0].parentNode = fatherNode;
        GenerateTree(corners[0], len);
        for (int i = 1; i < 4; i++)
        {
            corners[i].starEndIndex.x = corners[i - 1].starEndIndex.y;
            corners[i].starEndIndex.y = corners[i].starEndIndex.x + len;
            corners[i].parentNode = fatherNode;
            switch (i)
            {
                case 1:
                    corners[i].nodeRect.Set(corners[0].nodeRect.x, corners[0].nodeRect.yMax, corners[0].nodeRect.width, corners[0].nodeRect.height);
                    break;
                case 2:
                    corners[i].nodeRect.Set(corners[0].nodeRect.xMax, corners[0].nodeRect.yMax, corners[0].nodeRect.width, corners[0].nodeRect.height);
                    break;
                case 3:
                    corners[i].nodeRect.Set(corners[0].nodeRect.xMax, corners[0].nodeRect.y, corners[0].nodeRect.width, corners[0].nodeRect.height);
                    break;
                default:
                    break;
            }
            GenerateTree(corners[i], len);
        }
        fatherNode.childrenList.AddRange(corners);
    }




    //遍历节点
    public FourXTreeNode FindLastNode(Vector2 pos)
    {
        if (rootNode.nodeRect.Contains(pos) == false) { return null; }
        //从根节点遍历
        FourXTreeNode result = rootNode;
        if (result.hasChild == true)
        {
            for (int i = 1; i < treeHeight; i++)
            {
                if (result.leftDown != null && result.leftDown.nodeRect.Contains(pos))
                {
                    result = result.leftDown;
                    continue;
                }
                if (result.leftUp != null && result.leftUp.nodeRect.Contains(pos))
                {
                    result = result.leftUp;
                    continue;
                }
                if (result.rightUp != null && result.rightUp.nodeRect.Contains(pos))
                {
                    result = result.rightUp;
                    continue;
                }
                if (result.rightDown != null && result.rightDown.nodeRect.Contains(pos))
                {
                    result = result.rightDown;
                    continue;
                }
            }
        }
        return result;
    }



}








public class FourXTreeNode
{
    //树高
    public int fourXTreeHigh;
    //下标
    public Vector2Int starEndIndex;
    public int Index;
    public FourXTreeNode parentNode { get; set; }
    //子集链表
    public List<FourXTreeNode> childrenList { get; set; }
    /***************************************************************************************************************************************/
    public Vector3 nodePosition;
    public Rect nodeRect;
    public Chunk nodeChunkScripts { get; set; }
    public List<GameObject> nodeObjList{ get; set; }
    public bool isActive { get; set; }
    public FourXTreeNode()
    {
        fourXTreeHigh = 1;
        starEndIndex = new Vector2Int(0, 1);
        parentNode = null;
        childrenList = new List<FourXTreeNode>();
        nodeObjList = new List<GameObject>();
    }


    public FourXTreeNode leftDown 
    {
        get
        {
            if (childrenList.Count == 0)
            {
                return null;
            }
            return childrenList[0];
        }
        set
        {
            if (childrenList.Count == 0)
            {
                childrenList.Add(value);
            }
            else
            {
                childrenList[0] = value;
            }
            Debug.LogError("error");
            //nodeRect.x = value.nodeRect.x - value.nodeRect.width;
            //nodeRect.y = value.nodeRect.y - value.nodeRect.height;
            //nodeRect.width = 2 * value.nodeRect.width;
            //nodeRect.height = 2 * value.nodeRect.height;
            //nodePosition = new Vector3(value.nodeRect.x, 0f, value.nodeRect.y);
            //value.parentNode = this;
        }
    }



    public FourXTreeNode leftUp
    {
        get
        {
            if (childrenList.Count < 2)
            {
                return null;
            }
            return childrenList[1];
        }
        set
        {
            if (childrenList.Count < 2)
            {
                childrenList.Add(value);
            }
            else
            {
                childrenList[1] = value;
            }
            value.parentNode = this;
        }
    }




    public FourXTreeNode rightUp
    {
        get
        {
            if (childrenList.Count < 3)
            {
                return null;
            }
            return childrenList[2];
        }
        set
        {
            if (childrenList.Count < 3)
            {
                childrenList.Add(value);
            }
            else
            {
                childrenList[2] = value;
            }
            value.parentNode = this;
        }
    }




    public FourXTreeNode rightDown
    {
        get
        {
            if (childrenList.Count < 4)
            {
                return null;
            }
            return childrenList[3];
        }
        set
        {
            if (childrenList.Count < 4)
            {
                childrenList.Add(value);
            }
            else
            {
                childrenList[3] = value;
            }
            value.parentNode = this;
        }
    }




    public bool hasChild
    {
        get
        {
            return childrenList != null && childrenList.Count > 0;
        }
    }

    //计算树高
    public int GetTreeHigh()
    {
        if (hasChild == true)
        {
            return 1;
        }
        int high = childrenList[0].GetTreeHigh();
        for (int i = 1; i < childrenList.Count; i++)
        {
            int temp = childrenList[i].GetTreeHigh();
            if (high < temp)
            {
                high = temp;
            }
        }
        return fourXTreeHigh + high;
    }

}