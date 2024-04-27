using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    private PathFindingSettings _settings;
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




    private Renderer _renderer;
    private MaterialPropertyBlock propBlock;

    private int _x;
    private int _y;

    private float _cost = 1.0f;
    public float Cost
    {
        get => _cost;
        set => _cost = value;
    }

    private void SetTileCost()
    {
        switch(_tileType)
        {
            case TileType.Ground:
                Cost = _settings.GroundCost;
                break;
            case TileType.Tree:
                Cost = _settings.TreeCost;
                break;
            case TileType.Hill:
                Cost = _settings.HillCost;
                break;
            case TileType.Void:
                Cost = Mathf.Infinity;
                break;
            default:
                break;
        }
    }

    private float _costToReach = Mathf.Infinity;
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

    private Color _Color { 
        
        set
        {
            if (_renderer == null) return;
            _renderer.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", value);
            _renderer.SetPropertyBlock(propBlock);
        } 
    }

    private void Awake()
    {
        propBlock = new MaterialPropertyBlock();
        LoadSettings();
    }

    private void Update()
    {
        SetTileCost();
    }

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
                    break;
                case TileType.Tree:
                    _renderer = _GroundGO.GetComponent<Renderer>();

                    _GroundGO.SetActive(true);
                    _TreeGO.SetActive(true);
                    _HillGO.SetActive(false);
                    break;
                case TileType.Hill:
                    _renderer = _GroundGO.GetComponent<Renderer>();

                    _GroundGO.SetActive(true);
                    _TreeGO.SetActive(false);
                    _HillGO.SetActive(true);
                    break;

                case TileType.Void:
                    _renderer = null;

                    _GroundGO.SetActive(false);
                    _TreeGO.SetActive(false);
                    _HillGO.SetActive(false);
                    break;
                default:
                    break;
            }

            SetTileCost();
        }
            
    }


    public void ColorTile(Color color)
    {
        _Color = color;
    }



    public int _X => _x;
    public int _Y => _y;

    private void LoadSettings()
    {
        _settings = Resources.Load<PathFindingSettings>("PathFindingSettings");
        if (_settings != null)
        {
            Debug.Log("Settings loaded successfully.");
        }
        else
        {
            Debug.LogError("Failed to load settings.");
        }
    }

}

