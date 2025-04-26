using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private Transform _baseMapTransform;
    [SerializeField] private GameObject _trayBlockPrefab;
    [SerializeField] private GameObject _lightCheckered, _darkCheckered;
    [SerializeField] private LevelData levelData;


    private void Start()
    {
        if (CreateBaseMap(levelData.lengthX, levelData.lengthY) != null)
        {
            CreateTrayBlocks();
            CreateWalls(levelData.lengthX, levelData.lengthY);
        }

    }

    private void CreateWalls(int x, int y)
    {
        GameObject[] walls = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            walls[i] = new GameObject("wall");
            walls[i].transform.parent = _baseMapTransform;
            walls[i].AddComponent<BoxCollider>();

        }

        //left
        walls[0].GetComponent<BoxCollider>().size = new Vector3(2f, 1f, y * 1f);
        walls[0].transform.position = new Vector3(-1.5f, 0f, y / 2 - 0.5f);

        //right
        walls[1].GetComponent<BoxCollider>().size = new Vector3(2f, 1f, y * 1f);
        walls[1].transform.position = new Vector3(x + 0.5f, 0f, y / 2 - 0.5f);

        //down
        walls[2].GetComponent<BoxCollider>().size = new Vector3(x * 1f, 1f, 2f);
        walls[2].transform.position = new Vector3(x / 2, 0f, -1.5f);

        //up
        walls[3].GetComponent<BoxCollider>().size = new Vector3(x * 1f, 1f, 2f);
        walls[3].transform.position = new Vector3(x / 2, 0f, y + 0.5f);

    }

    private Tile[][] CreateBaseMap(int x, int y)
    {
        if (x <= 0 || y <= 0)
        {
            Debug.LogError("invalid map size");
            return null;
        }

        Tile[][] map = new Tile[x][];
        for (int i = 0; i < x; i++)
        {
            map[i] = new Tile[y];
            for (int j = 0; j < y; j++)
            {
                map[i][j] = new Tile(new Vector2(i, j));
                Instantiate(((i + j) % 2 == 0) ? _lightCheckered : _darkCheckered, new Vector3(i, 0, j), Quaternion.Euler(-90, 0, 0), _baseMapTransform);
            }
        }
        return map;
    }

    private void CreateTrayBlocks()
    {
        foreach (var trayData in levelData.trayDatas)
        {
            GameObject trayBlock = new GameObject();
            trayBlock.name = trayData.color.ToString();
            trayBlock.transform.parent = _baseMapTransform;
            Rigidbody rb = trayBlock.AddComponent<Rigidbody>();
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.isKinematic = true;

            List<Vector2Int> placementPoints = Element.BlockTiles(trayData.elementBlockStr);
            foreach (var placementPoint in placementPoints)
            {
                GameObject g = Instantiate(_trayBlockPrefab, new Vector3(placementPoint.x, 0, placementPoint.y), Quaternion.identity, trayBlock.transform);
                g.GetComponent<Renderer>().materials[0].color = trayData.color;
                g.GetComponent<Renderer>().materials[1].color = trayData.color;
            }
        }
    }
}

public class Tile
{
    public Vector2 tileLocID;
    public Vector2 tileGlobalPosition;

    public Tile(Vector2 _tileLocID)
    {
        tileLocID = new Vector2(_tileLocID.x, _tileLocID.y);
        tileGlobalPosition = new Vector2(tileLocID.x, tileLocID.y);
    }
}
