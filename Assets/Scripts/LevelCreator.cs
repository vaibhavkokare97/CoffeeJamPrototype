using System;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private Transform _baseMapTransform;
    [SerializeField] private GameObject _lightCheckered, _darkCheckered;

    [Range(4, 7)] [SerializeField] private int lengthX, lengthY;

    private void Start()
    {
        CreateBaseMap(lengthX, lengthY);
    }

    private Tile[][] CreateBaseMap(int x, int y)
    {
        if (lengthX <= 0 || lengthY <= 0)
        {
            Debug.LogError("Invalid map size");
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
