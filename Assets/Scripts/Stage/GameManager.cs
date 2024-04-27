using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Pathfinding pathfinding;

    public GameObject TileMap;
    public GameObject player;

    private PlayerController playerController;
    private Tile[][] grid;

    private Tile current_tile;
    private Tile goal_tile;

    // Start is called before the first frame update
    void Start()
    {
        pathfinding = gameObject.GetComponent<Pathfinding>();
        grid = TileMap.GetComponent<TileMap>().grid;

        playerController = player.GetComponent<PlayerController>();
        SpawnPlayer();

        playerController.onCurrentTileUpdate += HandlePositionUpdate;
    }

    private void HandlePositionUpdate(Tile tile)
    {
        current_tile = tile;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            HandleMouseClick(pathfinding.Djikistra);
        }
        else if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            HandleMouseClick(pathfinding.AStar);
        }

    }


    private void HandleMouseClick(System.Func<Tile, Tile, Stack<Tile>> pathfindingMethod)
    {
        RaycastHit hitInfo;
        if (TryGetRaycastHit(out hitInfo))
        {
            Tile goal_tile = hitInfo.transform.gameObject.GetComponentInParent<Tile>();

            if (goal_tile._TileType != Tile.TileType.Void)
            {
                TileMap.GetComponent<TileMap>().ResetAllTiles();
                Stack<Tile> path = pathfindingMethod(current_tile, goal_tile);
                if (path != null)
                {
                    playerController.SetPath(path);
                }
            }
        }
    }

    private bool TryGetRaycastHit(out RaycastHit hitInfo)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("Tile"); // Layer mask for the "Tile" layer
        bool hasHit = Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);

        if (hasHit)
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.red, 2.0f); // Draw line for visual debugging
        }

        return hasHit;
    }


    private Tile GetSpawntile()
    {
        Tile spawn_tile = grid[0][0];

        int sizeX = TileMap.GetComponent<TileMap>().sizeX;

        int i = 0;
        int j = 0;

        while(spawn_tile._TileType == Tile.TileType.Void)
        {

            j = (j < sizeX - 1) ? j + 1 : 0;
            if (j == 0) i++;

            spawn_tile = grid[i][j];
        }

        Debug.Log("spawn tile found at: " +  spawn_tile.name);
        return spawn_tile; 
    }


    private void SpawnPlayer()
    {
        current_tile = GetSpawntile();
        player.transform.position = new Vector3(current_tile.transform.position.x, 0.2f, current_tile.transform.position.z);
    }
}
