using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
public class Chunk : MonoBehaviour
{
    //����ڵ��±�
    public int Index { get; set; }

    [Header("���鳤��")]
    public int chunkWidth;

    [Header("�������߶�")]
    public int chunkHeight;

    //���Σ���ȣ��Լ����氼͹�̶ȣ����ͣ�
    [Header("Ƶ��")]
    public float frequency;

    //���Σ���ȣ��Լ����氼͹�̶� �����ͣ�
    [Header("���")]
    public float amplitude;

    [Header("����")]
    [Range(0.1f, 1.0f)]
    public float persistence;

    [Header("����ƽ��")]
    [Range(1, 30)]
    public uint octaves;

    [Header("�����߶�")]
    [Range(2, 255)]
    public uint baseHeight;

    [Header("ֲ�����ֲ��ܶ�")]
    [Range(0f, 1f)]
    public float treeDensity = 0.01f;

    [Header("���˷ֲ��ܶ�")]
    [Range(0f, 1f)]
    public float MonsterDensity = 0.001f;

    //���Σ���ȣ��Լ����氼͹�̶� ������
    [Header("����")]
    [Range(64, 10000)]
    public float relief;

    public static Vector2 randOff;

    MeshRenderer mMeshRender;
    MeshFilter mMeshFilter;
    Mesh mChunkMesh;
    MeshCollider mMeshCollider;

    BlockType[,,] map;
    int unityMaxVertices = 60000;


    void Start()
    {
        mMeshRender = GetComponent<MeshRenderer>();
        mMeshCollider = GetComponent<MeshCollider>();
        mMeshFilter = GetComponent<MeshFilter>();
        mMeshRender.materials = Block.blockMap.Values.Select(s => s.Material).ToArray();
        map = new BlockType[chunkWidth, chunkHeight, chunkWidth];
        BuildTerrain();
        BuildMesh();
    }
    void BuildMesh()
    {
        //ģ�Ͷ���
        List<Vector3> vertList = new List<Vector3>();
        //����������˳��
        Dictionary<BlockType, List<int>> triangleDic = new Dictionary<BlockType, List<int>>();
        //ģ������ uv ����
        List<Vector2> uvList = new List<Vector2>();
        int meshNum = 0;
        for (int x = 0; x < chunkWidth; x++)
        {
            for (int y = 0; y < chunkHeight; y++)
            {
                for (int z = 0; z < chunkWidth; z++)
                {
                    if (map[x, y, z] == BlockType.Air) { continue; }

                    Vector3 blockPos = new Vector3(x, y, z);
                    if (y + 1 >= chunkHeight || map[x, y + 1, z] == BlockType.Air)//��
                    {
                        AddMeshFace(vertList.Count, blockPos + new Vector3(0, 1, 0), new Vector3(0, 0, 1),
                            new Vector3(1, 0, 0), vertList, triangleDic, map[x, y, z]);
                        uvList.AddRange(map[x, y, z].GetTopUVs());
                    }

                    if (y - 1 < 0 || map[x, y - 1, z] == BlockType.Air)                             // ��
                    {
                        AddMeshFace(vertList.Count, blockPos + new Vector3(0, 0, 1), new Vector3(0, 0, -1),
                            new Vector3(1, 0, 0), vertList, triangleDic, map[x, y, z]);
                        uvList.AddRange(map[x, y, z].GetBottomUVs());
                    }

                    if (x - 1 < 0 || map[x - 1, y, z] == BlockType.Air)                      // ��
                    {
                        AddMeshFace(vertList.Count, blockPos + new Vector3(0, 0, 1), new Vector3(0, 1, 0),
                            new Vector3(0, 0, -1), vertList, triangleDic, map[x, y, z]);
                        uvList.AddRange(map[x, y, z].GetLeftUVs());
                    }

                    if (x + 1 >= chunkWidth || map[x + 1, y, z] == BlockType.Air)         // ��
                    {
                        AddMeshFace(vertList.Count, blockPos + new Vector3(1, 0, 0), new Vector3(0, 1, 0),
                            new Vector3(0, 0, 1), vertList, triangleDic, map[x, y, z]);
                        uvList.AddRange(map[x, y, z].GetRightUVs());
                    }

                    if (z - 1 < 0 || map[x, y, z - 1] == BlockType.Air)                     // ǰ
                    {
                        AddMeshFace(vertList.Count, blockPos, new Vector3(0, 1, 0),
                            new Vector3(1, 0, 0), vertList, triangleDic, map[x, y, z]);
                        uvList.AddRange(map[x, y, z].GetForwardUVs());
                    }

                    if (z + 1 >= chunkWidth || map[x, y, z + 1] == BlockType.Air)          // ��
                    {
                        AddMeshFace(vertList.Count, blockPos + new Vector3(1, 0, 1), new Vector3(0, 1, 0),
                            new Vector3(-1, 0, 0), vertList, triangleDic, map[x, y, z]);
                        uvList.AddRange(map[x, y, z].GetBackUVs());
                    }


                    if (vertList.Count > unityMaxVertices)
                    {
                        //����������ͼ
                        GameObject tempObject = new GameObject();
                        tempObject.name = gameObject.name + "_" + meshNum;
                        tempObject.transform.position = transform.position;
                        tempObject.transform.SetParent(transform);
                        Mesh mesh = tempObject.AddComponent<MeshFilter>().mesh;
                        MeshRenderer meshRenderer = tempObject.AddComponent<MeshRenderer>();
                        MeshCollider meshCollider = tempObject.AddComponent<MeshCollider>();
                        mesh.vertices = vertList.ToArray();
                        //**********************************?**************************************
                        mesh.subMeshCount = mMeshRender.materials.Length;
                        foreach (var triangLink in triangleDic)
                        {
                            //���÷����Ӧ����������������˳��
                            mesh.SetTriangles(triangLink.Value.ToArray(), triangLink.Key.GetMatId());
                        }
                        //�������� Uv ����
                        mesh.uv = uvList.ToArray();
                        //�ػ�������
                        mesh.RecalculateNormals();
                        meshCollider.sharedMesh = mesh;
                        meshRenderer.materials = mMeshRender.materials;
                        vertList.Clear();
                        triangleDic.Clear();
                        uvList.Clear();
                    }
                }
            }
        }
        if (vertList.Count > 0)
        {
            mChunkMesh = new Mesh();
            //***************************************?********************************************
            //�� ���ʸ���
            mChunkMesh.subMeshCount = mMeshRender.materials.Length;
            //�� ��������
            mChunkMesh.vertices = vertList.ToArray();
            foreach (var triangLink in triangleDic)
            {
                mChunkMesh.SetTriangles(triangLink.Value.ToArray(), triangLink.Key.GetMatId());
            }
            //���÷������� UV ����
            mChunkMesh.uv = uvList.ToArray();
            //�ػ�������
            //*********************************���������֮ǰ��Ҫ�ػ�������*****************************
            mChunkMesh.RecalculateNormals();
            mMeshFilter.mesh = mChunkMesh;
            //***************************�����ײ�����ж�������Ե�������**********************************
            mMeshCollider.sharedMesh = mChunkMesh;
        }
    }
    void AddMeshFace(int vertListCount, Vector3 starPoint, Vector3 up, Vector3 right, List<Vector3> vertList,
        Dictionary<BlockType, List<int>> triangleDict, BlockType type)
    {
        vertList.Add(starPoint);
        vertList.Add(starPoint + up);
        vertList.Add(starPoint + up + right);
        vertList.Add(starPoint + right);
        /*
         
         1    2
         0    3

         */
        //ȷ��������������˳��
        if (triangleDict.ContainsKey(type))
        {
            triangleDict[type].Add(vertListCount + 0);
            triangleDict[type].Add(vertListCount + 2);
            triangleDict[type].Add(vertListCount + 3);
            triangleDict[type].Add(vertListCount + 0);
            triangleDict[type].Add(vertListCount + 1);
            triangleDict[type].Add(vertListCount + 2);
        }
        else
        {
            //����������˷���������ô������µķ�������
            //������������������˳��
            triangleDict.Add(type, new List<int>() {
                    vertListCount + 0, vertListCount + 2, vertListCount + 3, vertListCount + 0, vertListCount + 1, vertListCount + 2
            });
        }
    }

    //���ڰ������ɵ���߶�
    void BuildTerrain()
    {
        for (int x = 0; x < chunkWidth; x++)
        {
            for (int z = 0; z < chunkWidth; z++)
            {
                long berlinNoise = PerlinNoise(x + (int)transform.position.x, z + (int)transform.position.z);
                for (int y = 0; y < chunkHeight; y++)
                {
                    if (y == berlinNoise)
                    {
                        map[x, y, z] = BlockType.DirtGrass;
                        //������
                        if ((BuildTree(x, z, y) == false) && (Random.value < MonsterDensity))
                        {
                            GameObject.Instantiate(Resources.Load<Transform>("Prefab/QiuQiuPopple"), new Vector3(x, y + 1, z) + transform.position, new Quaternion());
                        }
                    }
                    else if (y < berlinNoise)
                    {
                        if (berlinNoise - y > 10)
                        {
                            map[x, y, z] = BlockType.Stone;
                        }
                        else
                        {
                            map[x, y, z] = BlockType.Dirt;
                        }
                    }
                }
            }
        }
    }




    //�������㷨
    bool BuildTree(int x, int z, int y)
    {
        if (Random.value < treeDensity)
        {
            if (x <= 1 || z <= 1)
            {
                return false;
            }
            if (x >= chunkWidth - 1 || z >= chunkWidth - 1)
            {
                return false;
            }
            int height = Random.Range(3, 10);
            for (int i = 0; i < height; i++)
            {
                if (y + i >= chunkHeight)
                {
                    break;
                }
                map[x, y++, z] = BlockType.Wood;
            }
            //��ȥ����߶�
            --y;
            // �����Ҷ����
            height = Random.Range(2, 5);
            for (int i = 1; i <= height; i++)
            {
                if (++y >= chunkHeight)
                    break;
                map[x, y, z] = BlockType.Wood;
                // ÿ��Ȧ��
                int circle = height - i;
                if (circle == 0)
                {
                    map[x, y, z] = BlockType.Leaves;
                }
                else
                {
                    for (int j = 1; j <= circle; j++)
                    {
                        // ������ ��  45��б��
                        if (x + j < chunkWidth)
                        {
                            map[x + j, y, z] = BlockType.Leaves;
                            if (z + j < chunkWidth)
                                map[x + j, y, z + j] = BlockType.Leaves;
                            if (z - j >= 0)
                                map[x + j, y, z - j] = BlockType.Leaves;
                        }
                        if (x - j >= 0)
                        {
                            map[x - j, y, z] = BlockType.Leaves;
                            if (z + j < chunkWidth)
                                map[x - j, y, z + j] = BlockType.Leaves;
                            if (z - j >= 0)
                                map[x - j, y, z - j] = BlockType.Leaves;
                        }
                        if (z + j < chunkWidth)
                            map[x, y, z + j] = BlockType.Leaves;
                        if (z - j >= 0)
                            map[x, y, z - j] = BlockType.Leaves;

                        // ��45��б��
                        for (int k = 1; k <= circle; k++)
                        {
                            if (x + j < chunkWidth)
                            {
                                // 45��б��
                                if (z + k < chunkWidth)
                                    map[x + j, y, z + k] = BlockType.Leaves;
                                if (z - k >= 0)
                                    map[x + j, y, z - k] = BlockType.Leaves;
                            }
                            if (x - j >= 0)
                            {
                                if (z + k < chunkWidth)
                                    map[x - j, y, z + k] = BlockType.Leaves;
                                if (z - k >= 0)
                                    map[x - j, y, z - k] = BlockType.Leaves;
                            }
                        }
                    }
                }
            }
            return true;
        }
        return false;
    }

    long PerlinNoise(int x, int z)
    {
        Vector2 wPos = new Vector2((x + randOff.x + relief) / relief, (z + randOff.y + relief) / relief);
        float perlinNoise = 0;
        float _amplitude = amplitude;
        float _frequency = frequency;
        for (int i = 0; i < octaves; i++)
        {
            perlinNoise += Mathf.PerlinNoise(wPos.x * _frequency, wPos.y * _frequency) * _amplitude;
            _amplitude *= persistence;
            _frequency *= 2;
        }
        return baseHeight + (int)perlinNoise;
    }







    //��������
    public void DestroyBlock(Vector3 pos)
    {
        //��ȡ�������
        Vector3 relativePos= pos - transform.position;
        //Debug.Log($"���ƻ���chunk��㣺{transform.position},���飺{pos}���±꣺{relativePos}");

        map[(int)relativePos.x, (int)relativePos.y, (int)relativePos.z] = BlockType.Air;
        BuildMesh();

        ////����ȡ��
        //Vector3Int intRelativePos = new Vector3Int((int)relativePos.x, (int)relativePos.y, (int)relativePos.z);
        ////�����᷽���ж�
        //if (relativePos.x - intRelativePos.x < 0.00001f)
        //{
        //    Debug.Log("��ײ����������");

        //    map[intRelativePos.x, intRelativePos.y, intRelativePos.z] = BlockType.Air;
        //    if (intRelativePos.x > 0)
        //    { 
        //        map[intRelativePos.x - 1, intRelativePos.y, intRelativePos.z] = BlockType.Air;
        //    }
        //}
        //else if (relativePos.z - intRelativePos.z < 0.00001f)
        //{
        //    Debug.Log("��ײ����ǰ����");

        //    map[intRelativePos.x, intRelativePos.y, intRelativePos.z] = BlockType.Air;
        //    if (intRelativePos.z > 0)
        //    { 
        //        map[intRelativePos.x, intRelativePos.y, intRelativePos.z - 1] = BlockType.Air;
        //    }
        //}
        //else// if(index.y - intRelativePos.y < 0.00001f)
        //{
        //    Debug.Log("��ײ����������");

        //    map[intRelativePos.x, intRelativePos.y, intRelativePos.z] = BlockType.Air;
        //    if (intRelativePos.y > 0)
        //    { 
        //        map[intRelativePos.x, intRelativePos.y - 1, intRelativePos.z] = BlockType.Air;
        //    }
        //}
        ////�ػ�����
        //BuildMesh();
    }

}
