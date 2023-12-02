using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public List<Tile> neighbors;

    [HideInInspector] public Tile predecessor = null;


    public enum TileType
    {
        Ground,
        Wall
    }

    [SerializeField]
    private TileType _tileType;


    private Renderer _renderer;
    private int _x;
    private int _y;

    private float _cost = Mathf.Infinity;
    public Color _Color { get => _renderer.material.color; set => _renderer.material.color = value; }

    public void Init(int x, int y, int type = (int)TileType.Ground)
    {
        _renderer = GetComponent<Renderer>();
        neighbors = new List<Tile>();

        _x = x;
        _y = y;
        _TileType = (TileType)type;
        name = "Tile_" + x + "_" + y;
    }

    
    [SerializeField]
    public TileType _TileType
    {
        get => _tileType;
        set
        {
            _tileType = value;
            switch (_tileType)
            {
                // Here add switch case to display models or not EX: turretModel.SetActive(true)
                default:
                    break;
            }
        }
    }

    public float CostToReach
    {
        get
        {
            switch (_tileType)
            {
                default:
                    return 1.0f;
            }
        }

    }

    public float Cost
    {
        get
        {
            return _cost == Mathf.Infinity ? Mathf.Infinity : _cost;
        }

        set
        {
            _cost = value;
        }
    }


    public int _X => _x;
    public int _Y => _y;

}

