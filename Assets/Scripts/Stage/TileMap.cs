using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public Tile[,] grid;

    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private int sizeX = 10;
    [SerializeField] private int sizeY = 10;
    private Dictionary<Tile, List<Tile>> neighborDictionary;

    float strideZ = Mathf.Cos(Mathf.PI / 6);

    private void Start()
    {
        grid = new Tile[sizeX, sizeY];
    
        // Generer la map (gameobjects)
        for (int y = 0; y < sizeY; y++)
        {
            float offSetX = (y % 2 == 0) ? 0 : 0.5f;
            for (int x = 0; x < sizeX; x++)
            {
                Tile tile = Instantiate(_tilePrefab, new Vector3(x + offSetX, 0, y * strideZ), Quaternion.Euler(0.0f, 90.0f, 0.0f)).GetComponent<Tile>();
                grid[x, y] = tile;
                tile.gameObject.transform.parent = transform;
                grid[x, y].Init(x, y);
            }
        }


        // Construire le graphe
        neighborDictionary = new Dictionary<Tile, List<Tile>>();

        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                List<Tile> neighbors = new List<Tile>();

                if (y < sizeY - 1)
                    neighbors.Add(grid[x, y + 1]);
                if (x < sizeX - 1)
                    neighbors.Add(grid[x + 1, y]);
                if (y > 0)
                    neighbors.Add(grid[x, y - 1]);
                if (x > 0)
                    neighbors.Add(grid[x - 1, y]);
                if (x < sizeX - 1 && y < sizeY - 1)
                    neighbors.Add(grid[x + 1, y + 1]);
                if (x > 0 && y < sizeY - 1)
                    neighbors.Add(grid[x - 1, y + 1]);

                neighborDictionary.Add(grid[x, y], neighbors);
                grid[x, y].neighbors = neighbors;
            }
        }

        


    }

    public List<Tile> Neighbors(Tile tile)
    {
        return neighborDictionary[tile];
    }

    public void ResetAllTiles()
    {
        if(grid != null)
        {
            foreach (Tile t in grid)
            {
                t.predecessor = null;
                t.Cost = Mathf.Infinity;
                t._Color = Color.white;
            }
        }

    }
}