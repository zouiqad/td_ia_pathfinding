using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public Tile[][] grid;

    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private int sizeX = 13;
    private Dictionary<Tile, List<Tile>> neighborDictionary;



    private void Start()
    {
        grid = new Tile[sizeX][];

        int sizeY = (sizeX + 1) / 2;
        bool isIncreasing = true; // Flag to track whether sizeY is increasing or decreasing
        // Initialize each sub-array
        for (int i = 0; i < sizeX; i++)
        {
            grid[i] = new Tile[sizeY];

            // Determine the step direction based on whether sizeY has reached sizeX
            if (sizeY >= sizeX && isIncreasing)
            {
                isIncreasing = false; // Switch to decreasing once sizeY reaches sizeX
            }

            int sizeYDirection = isIncreasing ? 1 : -1;
            sizeY += sizeYDirection;
        }


        float strideZ = Mathf.Cos(Mathf.PI / 6);
        float offSetX = 0.0f;
        isIncreasing = true;
        // Generate the map (gameobjects)
        for (int x = 0; x < sizeX; x++)
        {
            
            sizeY = grid[x].Length;
            for (int y = 0; y < sizeY; y++)
            {
                int type = 0;

                Tile tile = Instantiate(_tilePrefab, new Vector3(y + offSetX, 0, x * strideZ), Quaternion.identity).GetComponent<Tile>();
                grid[x][y] = tile;
                tile.gameObject.transform.parent = transform;

                grid[x][y].Init(x, y, type);
            }

            // Determine the step direction based on whether sizeY has reached sizeX
            if (sizeY >= sizeX && isIncreasing)
            {
                isIncreasing = false; // Switch to decreasing once sizeY reaches sizeX
            }

            int sizeYDirection = isIncreasing ? -1 : 1;
            offSetX += 0.5f * sizeYDirection;
        }

        BuildGraph();
    }

    // Build the graph
    private void BuildGraph()
    {

        neighborDictionary = new Dictionary<Tile, List<Tile>>();

        for (int x = 0; x < sizeX; x++)
        {
            int sizeY = grid[x].Length;

            Tile[] upperRow = x < sizeX - 1 ? grid[x + 1] : null;
            Tile[] lowerRow = x > 0 ? grid[x - 1] : null;

            for (int y = 0; y < sizeY; y++)
            {
                List<Tile> neighbors = new List<Tile>();
                // TO DO FIX NEIGHBORS IMPLEMENT DIRECTION
                if (y < sizeY - 1)
                    neighbors.Add(grid[x][y + 1]);
                if (y > 0)
                    neighbors.Add(grid[x][y - 1]);
                if (upperRow != null && y < upperRow.Length)
                    neighbors.Add(upperRow[y]);
                if (upperRow != null && y < upperRow.Length - 1)
                    neighbors.Add(upperRow[y + 1]);
                if (lowerRow != null && y < lowerRow.Length)
                    neighbors.Add(lowerRow[y]);
                if (lowerRow != null && y > 0)
                    neighbors.Add(lowerRow[y - 1]);

                neighborDictionary.Add(grid[x][y], neighbors);
                grid[x][y].neighbors = neighbors;
            }
        }
    }
    public List<Tile> Neighbors(Tile tile)
    {
        return neighborDictionary[tile];
    }

    public void ResetAllTiles()
    {
        if (grid != null)
        {
            foreach (Tile[] row in grid)
            {
                foreach (Tile t in row)
                {
                    t.predecessor = null;
                    t.CostToReach = Mathf.Infinity;
                    t.ColorTile(Color.white);
                }
            }
        }
    }
}