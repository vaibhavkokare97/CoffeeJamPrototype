using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData")]
public class LevelData : ScriptableObject
{
    [Range(4, 7)] [SerializeField] public int lengthX, lengthY;
    public List<Element> trayDatas;


}

[Serializable]
public class Element
{
    public string elementBlockStr; //00,01,02
    public Color color;
    [HideInInspector] public List<Vector2Int> blockedTiles;

    public static List<Vector2Int> BlockTiles(string _elementBlockStr)
    {
        List<Vector2Int> tiles = new List<Vector2Int>();

        if (string.IsNullOrEmpty(_elementBlockStr))
            return tiles;

        string[] coords = _elementBlockStr.Split(',');

        foreach (string coord in coords)
        {
            if (coord.Length >= 2)
            {
                int x = int.Parse(coord[0].ToString());
                int y = int.Parse(coord[1].ToString());
                tiles.Add(new Vector2Int(x, y));
            }
        }

        return tiles;
    }

    public Element(string _elementBlockStr)
    {
        elementBlockStr = _elementBlockStr;
        blockedTiles = BlockTiles(elementBlockStr);
    }
}
