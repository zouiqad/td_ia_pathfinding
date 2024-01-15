using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [HideInInspector]
    public Tile predecessor = null;
    public List<Tile> neighbors;

    [SerializeField] private GameObject _GroundGO;
    [SerializeField] private GameObject _TreeGO;
    [SerializeField] private GameObject _HillGO;

    public enum TileType
    {
        Ground,
        Tree,
        Hill,
        Void
    }

    [HideInInspector]
    public TileType _tileType;

    [SerializeField]
    private float _cost = 1.0f;

    private Renderer _renderer;
    private int _x;
    private int _y;

    private float _costToReach = Mathf.Infinity;
    private Color _Color { get => _renderer.material.color; set => _renderer.material.color = value; }

    public void Init(int x, int y, int type = (int)TileType.Ground)
    {
        neighbors = new List<Tile>();

        _x = x;
        _y = y;
        _TileType = (TileType)type;
        name = "Tile_" + x + "_" + y;
    }


    public TileType _TileType
    {
        get => _tileType;

        set
        {
            _tileType = value;

            switch (_tileType)
            {
                case TileType.Ground:
                    _renderer = _GroundGO.GetComponent<Renderer>();

                    _GroundGO.SetActive(true);
                    _TreeGO.SetActive(false);
                    _HillGO.SetActive(false);

                    Cost = 1.0f;
                    break;
                case TileType.Tree:
                    _renderer = _TreeGO.GetComponent<Renderer>();

                    _GroundGO.SetActive(true);
                    _TreeGO.SetActive(true);
                    _HillGO.SetActive(false);

                    Cost = 3.0f;
                    break;
                case TileType.Hill:
                    _renderer = _HillGO.GetComponent<Renderer>();

                    _GroundGO.SetActive(true);
                    _TreeGO.SetActive(false);
                    _HillGO.SetActive(true);

                    Cost = 5.0f;
                    break;

                case TileType.Void:
                    _renderer = null;

                    _GroundGO.SetActive(false);
                    _TreeGO.SetActive(false);
                    _HillGO.SetActive(false);

                    Cost = Mathf.Infinity;
                    break;
                default:
                    break;
            }
        }
            
    }


    public void ColorTile(Color color)
    {

    }


    public float Cost
    {
        get => _cost;
        set => _cost = value;
    }

    public float CostToReach
    {
        get
        {
            return _costToReach == Mathf.Infinity ? Mathf.Infinity : _costToReach;
        }

        set
        {
            _costToReach = value;
        }
    }


    public int _X => _x;
    public int _Y => _y;

}

