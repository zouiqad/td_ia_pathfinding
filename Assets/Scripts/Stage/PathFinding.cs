using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{

    public Stack<Tile> Djikistra(Tile depart_tile, Tile goal_tile)
    {
        Stack<Tile> path = new Stack<Tile>();

        List<Tile> closed_list = new List<Tile>();
        List<Tile> open_list = new List<Tile>();

        open_list.Add(depart_tile);

        Tile current_tile = depart_tile;
        current_tile.CostToReach = 0;
        depart_tile.ColorTile(Color.blue);

        while (open_list.Count > 0)
        {
            open_list.Sort((x, y) => x.CostToReach.CompareTo(y.CostToReach)); // Ordonner la liste pour toujours avoir l'element avec le plus bas cout en premier

            current_tile = open_list[0]; // Pop first element from open list

            if (current_tile == goal_tile)
                break; // break if goal is reached

            foreach (Tile neighbor in current_tile.neighbors)
            {
                if (!closed_list.Contains(neighbor) && neighbor._TileType != Tile.TileType.Void)
                {
                    float newCost = neighbor.Cost + current_tile.CostToReach;
                    if (neighbor.CostToReach > newCost)
                    {
                        neighbor.CostToReach = newCost;
                        neighbor.predecessor = current_tile;
                    }

                    if (!open_list.Contains(neighbor)) open_list.Add(neighbor);
                }
            }

            open_list.Remove(current_tile);
            closed_list.Add(current_tile);
        }



        while (current_tile.predecessor != null)
        {
            path.Push(current_tile);
            current_tile.ColorTile(Color.black);
            current_tile = current_tile.predecessor;
        }

        goal_tile.ColorTile(Color.red);
        return path;
    }

    public void AStar(Tile depart_tile, Tile goal_tile)
    {
        List<Tile> closed_list = new List<Tile>();
        List<Tile> open_list = new List<Tile>();

        open_list.Add(depart_tile);

        Tile current_tile = depart_tile;
        current_tile.Cost = 0;

        while (open_list.Count > 0)
        {
            open_list.Sort((x, y) => x.Cost.CompareTo(y.Cost)); // Ordonner la liste pour toujours avoir l'element avec le plus bas cout en premier

            current_tile = open_list[0]; // Pop first element from open list

            if (current_tile == goal_tile)
                break; // break if goal is reached

            foreach (Tile neighbor in current_tile.neighbors)
            {
                if (!closed_list.Contains(neighbor) && neighbor._TileType != Tile.TileType.Void)
                {
                    float gScore = neighbor.CostToReach + current_tile.Cost;
                    if (neighbor.Cost > gScore)
                    {
                        neighbor.Cost = gScore + ManhattanDistance(neighbor, goal_tile);
                        //neighbor._Color = Color.magenta;
                        neighbor.predecessor = current_tile;
                    }

                    if (!open_list.Contains(neighbor)) open_list.Add(neighbor);
                }
            }

            open_list.Remove(current_tile);
            closed_list.Add(current_tile);
        }

        goal_tile.ColorTile(Color.green);
        Tile current = goal_tile.predecessor;

        while (current.predecessor != null)
        {
            current.ColorTile(Color.grey);
            current = current.predecessor;
        }

    }

    private float ManhattanDistance(Tile tile, Tile goal_tile)
    {
        return Mathf.Abs(tile._X - goal_tile._X) + Mathf.Abs(tile._Y - goal_tile._Y);
    }


}