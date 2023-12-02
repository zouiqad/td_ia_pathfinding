using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Pathfinding pathfinding;

    public GameObject TileMap;
    public GameObject player;

    private PlayerController playerController;
    private Tile[,] grid;

    private Tile current_tile;
    private Tile goal_tile;

    // Start is called before the first frame update
    void Start()
    {
        pathfinding = gameObject.GetComponent<Pathfinding>();
        grid = TileMap.GetComponent<TileMap>().grid;
        playerController = player.GetComponent<PlayerController>();
        SpawnPlayer();
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                Debug.DrawLine(ray.origin, raycastHit.point, Color.red, 2.0f); // Line will last for 2 seconds
                //pathfinding.ResetAllTiles();
                // Using the exact hit point for precision
                Vector3Int hitPosition = Vector3Int.FloorToInt(raycastHit.point);

                goal_tile = raycastHit.transform.gameObject.GetComponent<Tile>();
                goal_tile._Color = Color.green;
                print("Hit " + raycastHit.transform.name + " at " + raycastHit.point);
                Stack<Tile> path = pathfinding.Djikistra(current_tile, goal_tile);
                playerController.SetPath(path);
                current_tile = goal_tile;
                TileMap.GetComponent<TileMap>().ResetAllTiles();

            }
        }

/*        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                Vector3Int goalPos = Vector3Int.FloorToInt(raycastHit.transform.position);
                Tile goal_tile = tilemap.grid[goalPos.x, goalPos.z];

                pathfinding.Djikistra(start_tile, goal_tile);
            }
        }*/

        
    }
    

/*    public void startDjikistra()
    {
        print(worldGrid[0, 0]);
        print(worldGrid[0, 0].grid[0, 0]);
        pathfinding.Djikistra(worldGrid[0, 0].grid[1, 1], worldGrid[1, 1].grid[5, 5]);
    }*/





    private void SpawnPlayer()
    {
        player.transform.position = new Vector3(grid[0, 0].transform.position.x, 0.65f, grid[0, 0].transform.position.z);
        current_tile = grid[0, 0];

    }

/*    private void SpawnPlayer()
    {
        Camera player_camera = player.GetComponentInChildren<Camera>();

        int rand_x = Random.Range(0, tilemap.sizeX);
        int rand_y = Random.Range(0, tilemap.sizeY);

        while (tilemap.grid[rand_x, rand_y]._TileType == Tile.TileType.Wall)
        {
            rand_x = Random.Range(0, tilemap.sizeX);
            rand_y = Random.Range(0, tilemap.sizeY);
        }

        player.transform.position = new Vector3(rand_x, 1.5f, rand_y);

    }*/
}
