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
        depart_tile.ColorTile(Color.green);

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
                    neighbor.ColorTile(Color.yellow);
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
            current_tile.ColorTile(Color.blue);
            current_tile = current_tile.predecessor;
        }

        goal_tile.ColorTile(Color.green);
        return path;
    }

    public Stack<Tile> AStar(Tile depart_tile, Tile goal_tile)
    {
        Stack<Tile> path = new Stack<Tile>();
        List<Tile> openList = new List<Tile> { depart_tile };
        HashSet<Tile> closedList = new HashSet<Tile>();
        Dictionary<Tile, Tile> cameFrom = new Dictionary<Tile, Tile>();

        Dictionary<Tile, float> gScore = new Dictionary<Tile, float>();
        gScore[depart_tile] = 0;

        Dictionary<Tile, float> fScore = new Dictionary<Tile, float>();
        fScore[depart_tile] = ManhattanDistance(depart_tile, goal_tile);

        while (openList.Count > 0)
        {
            openList.Sort((a, b) => fScore[a].CompareTo(fScore[b]));
            Tile current = openList[0];

            if (current == goal_tile)
            {
                while (current != depart_tile)
                {
                    path.Push(current);
                    current.ColorTile(Color.blue);
                    current = cameFrom[current];
                }
                path.Push(depart_tile);
                depart_tile.ColorTile(Color.red);
                goal_tile.ColorTile(Color.green);
                return path;
            }

            openList.RemoveAt(0);
            closedList.Add(current);

            foreach (Tile neighbor in current.neighbors)
            {
                if (closedList.Contains(neighbor) || neighbor._TileType == Tile.TileType.Void)
                {
                    continue;
                }

                float tentativeGScore = gScore[current] + neighbor.CostToReach;

                if (!openList.Contains(neighbor))
                {
                    openList.Add(neighbor);
                }
                else if (tentativeGScore >= gScore[neighbor])
                {
                    continue;
                }

                cameFrom[neighbor] = current;
                gScore[neighbor] = tentativeGScore;
                fScore[neighbor] = gScore[neighbor] + ManhattanDistance(neighbor, goal_tile);
                neighbor.ColorTile(Color.magenta);
            }
        }

        return new Stack<Tile>();
    }

    private float ManhattanDistance(Tile tile, Tile goal_tile)
    {
        return Mathf.Abs(tile._X - goal_tile._X) + Mathf.Abs(tile._Y - goal_tile._Y);
    }


}