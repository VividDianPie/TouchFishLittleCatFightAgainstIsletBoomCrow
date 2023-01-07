using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BlockType
{
    Air = 0,
    Grass,
    Wood,
    Leaves,
    Dirt,
    DirtGrass,
    Stone
}
[System.Serializable]
public class BlockInfo
{
    protected Vector2[] mForwardUVs;
    protected Vector2[] mBackUVs;
    protected Vector2[] mLeftUVs;
    protected Vector2[] mRightUVs;
    protected Vector2[] mTopUVs;
    protected Vector2[] mBottomUVs;

    [Header("BlockType")]
    public BlockType Type;
    [Header("BlockMaterial")]
    public Material Material;

    public int materialIndex { get; set; } = 0;
    public Vector2[] topUVs { get { return mTopUVs; } }
    public Vector2[] forwardUVs { get { return mForwardUVs; } }
    public Vector2[] backUVs { get { return mBackUVs; } }
    public Vector2[] leftUVs { get { return mLeftUVs; } }
    public Vector2[] rightUVs { get { return mRightUVs; } }
    public Vector2[] bottomUVs { get { return mBottomUVs; } }

    public bool Init()
    {
        switch (Type)
        {
            case BlockType.Dirt:
            case BlockType.Leaves:
            case BlockType.Grass:
            case BlockType.Stone:
                return InitCubeGrass();
            case BlockType.DirtGrass:
                return InitDirtGrass();
            case BlockType.Wood:
                return InitCubeWood();
            case BlockType.Air:
            default:
                break;
        }
        return false;
    }


    private bool InitCubeWood()
    {
        // 正方体6个面 每个面4个uv
        float unitPos = 0.333333f;
        float x = 0, y = unitPos;
        mTopUVs = new Vector2[4]
        {
            new Vector2(x, y),
            new Vector2(x, y+unitPos),
            new Vector2(x+unitPos, y+unitPos),
            new Vector2(x+unitPos, y)
        };

        x = unitPos; y = 0;
        mBottomUVs = new Vector2[4]
        {
            new Vector2(x, y),
            new Vector2(x, y+unitPos),
            new Vector2(x+unitPos, y+unitPos),
            new Vector2(x+unitPos, y)
        };

        x = 0; y = 0;
        mForwardUVs = new Vector2[4]
        {
            new Vector2(x, y),
            new Vector2(x, y+unitPos),
            new Vector2(x+unitPos, y+unitPos),
            new Vector2(x+unitPos, y)
        };
        x = unitPos * 2; y = 0;
        mBackUVs = new Vector2[4]
        {
            new Vector2(x+unitPos, y+unitPos),
            new Vector2(x+unitPos, y),
            new Vector2(x, y),
            new Vector2(x, y+unitPos)
        };
        x = unitPos; y = unitPos;
        mLeftUVs = new Vector2[4]
        {
            new Vector2(x, y),
            new Vector2(x, y+unitPos),
            new Vector2(x+unitPos, y+unitPos),
            new Vector2(x+unitPos, y)
        };
        x = unitPos * 2; y = unitPos;
        mRightUVs = new Vector2[4]
        {
            new Vector2(x, y),
            new Vector2(x, y+unitPos),
            new Vector2(x+unitPos, y+unitPos),
            new Vector2(x+unitPos, y)
        };
        return true;
    }

    private bool InitDirtGrass()
    {
        float unitPos = 0.333333f;
        float x = 0, y = unitPos;
        mBottomUVs = new Vector2[4]
        {
            new Vector2(x, y),
            new Vector2(x, y+unitPos),
            new Vector2(x+unitPos, y+unitPos),
            new Vector2(x+unitPos, y)
        };

        x = unitPos; y = 0;
        mTopUVs = new Vector2[4]
        {
            new Vector2(x, y),
            new Vector2(x, y+unitPos),
            new Vector2(x+unitPos, y+unitPos),
            new Vector2(x+unitPos, y)
        };

        x = 0; y = 0;
        mForwardUVs = new Vector2[4]
        {
            new Vector2(x, y),
            new Vector2(x, y+unitPos),
            new Vector2(x+unitPos, y+unitPos),
            new Vector2(x+unitPos, y)
        };
        x = unitPos * 2; y = 0;
        mBackUVs = new Vector2[4]
        {
            new Vector2(x+unitPos, y+unitPos),
            new Vector2(x+unitPos, y),
            new Vector2(x, y),
            new Vector2(x, y+unitPos)
        };
        x = unitPos; y = unitPos;
        mLeftUVs = new Vector2[4]
        {
            new Vector2(x, y),
            new Vector2(x, y+unitPos),
            new Vector2(x+unitPos, y+unitPos),
            new Vector2(x+unitPos, y)
        };
        x = unitPos * 2; y = unitPos;
        mRightUVs = new Vector2[4]
        {
            new Vector2(x, y),
            new Vector2(x, y+unitPos),
            new Vector2(x+unitPos, y+unitPos),
            new Vector2(x+unitPos, y)
        };
        return true;
    }

    private bool InitCubeGrass()
    {
        float unitPos = 1;
        float x = 0, y = 0;
        mTopUVs = mBottomUVs =
        mForwardUVs = mBackUVs =
        mLeftUVs = mRightUVs = new Vector2[4]
        {
            new Vector2(x, y),
            new Vector2(x, y+unitPos),
            new Vector2(x+unitPos, y+unitPos),
            new Vector2(x+unitPos, y)
        };
        return true;
    }
}





/// <summary>
/// 初始化对应方块材质信息
/// </summary>
public class Block : MonoBehaviour
{
    public static Dictionary<BlockType, BlockInfo> blockMap = new Dictionary<BlockType, BlockInfo>();
    [Header("BlockInfo")]
    [SerializeField]
    public List<BlockInfo> blockInfoList;

    private void Awake()
    {

        for (int i = 0; i < blockInfoList.Count; i++)
        {
            BlockInfo blockInfo = blockInfoList[i];
            if (blockInfo.Init() == true)
            {
                if (blockMap.ContainsKey(blockInfo.Type) == false)
                {
                    blockInfo.materialIndex = i;
                    blockMap.Add(blockInfo.Type, blockInfo);
                }
            }//Grass        materialIndex       0
        }    //Wood       materialIndex       1
    }        //Leaves      materialIndex       2
             //Dirt           materialIndex       3
}            //DritGrass  materialIndex       4
             //Stone       materialIndex        5




/// <summary>
/// 抛出方块对应材质Id
/// </summary>
public static class BlockExtension
{
    public static int GetMatId(this BlockType type)
    {
        if (Block.blockMap.ContainsKey(type))
            return Block.blockMap[type].materialIndex;
        return 0;
    }
    public static Vector2[] GetTopUVs(this BlockType type)
    {
        if (Block.blockMap.ContainsKey(type))
            return Block.blockMap[type].topUVs;
        return null;
    }
    public static Vector2[] GetLeftUVs(this BlockType type)
    {
        if (Block.blockMap.ContainsKey(type))
            return Block.blockMap[type].leftUVs;
        return null;
    }
    public static Vector2[] GetRightUVs(this BlockType type)
    {
        if (Block.blockMap.ContainsKey(type))
            return Block.blockMap[type].rightUVs;
        return null;
    }
    public static Vector2[] GetForwardUVs(this BlockType type)
    {
        if (Block.blockMap.ContainsKey(type))
            return Block.blockMap[type].forwardUVs;
        return null;
    }
    public static Vector2[] GetBackUVs(this BlockType type)
    {
        if (Block.blockMap.ContainsKey(type))
            return Block.blockMap[type].backUVs;
        return null;
    }
    public static Vector2[] GetBottomUVs(this BlockType type)
    {
        if (Block.blockMap.ContainsKey(type))
            return Block.blockMap[type].bottomUVs;
        return null;
    }

}
