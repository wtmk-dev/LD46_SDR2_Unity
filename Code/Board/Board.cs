using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private int xMax = 2, xMin = -1,yMax = 3, yMin = -2;
    private Dictionary<Vector2, Tile> board;

    public void Init()
    {
        board = new Dictionary<Vector2, Tile>();

        for(int x = xMin; x < xMax; x++)
        {
            for(int y = yMin; y < yMax; y++)
            {
                GameObject clone = Instantiate(tilePrefab);
                Vector2 pos = new Vector2(x, y);
                Tile tile = clone.GetComponent<Tile>();
                clone.transform.position = pos;
                gameObject.transform.SetParent(clone.transform, gameObject.transform);
                board.Add(pos, tile);
            }
        }
    }

}
