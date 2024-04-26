using System;
using UnityEngine;

public class FloorType1 : MonoBehaviour
{
    [SerializeField] private TileData[] _tiles;
    public const float FloorBaseDistance = 0.1f;

    public Transform _pageTransform;

    private void Start()
    {
        foreach (var tile in _tiles)
        {
            tile.GetEndTransform().parent = _pageTransform;
        }
    }

    void Update()
    {
        var distance = 0f;

        for (int i = 0; i < _tiles.Length; i++)
        {
            distance += _tiles[i].TileDistance * FloorBaseDistance;
        }

        //IKのStartからEndまでの直線距離がTileDistanceの距離より長いか確認
        var d = Vector3.Distance(_tiles[^1].GetEndTransform().position,
            _tiles[0].GetStartTransform().position);

        for (int i = 0; i < _tiles.Length; i++)
        {
            Vector3 position, direction;
            Quaternion delta;
            //IK
            if (Vector3.Distance(_tiles[0].GetStartTransform().position, _tiles[^1].GetEndTransform().position) >=
                distance * FloorBaseDistance)
            {
                position = _tiles[i].GetStartTransform().position;
                direction = _tiles[i].GetEndTransform().position - position;
                delta = Quaternion.LookRotation(_tiles[i].GetFloorTransform().forward, direction);
                _tiles[i].GetFloorTransform().rotation = delta;
            }
            else
            {
                position = _tiles[0].GetStartTransform().position;
                direction = _tiles[^1].GetEndTransform().position - position;
                delta = Quaternion.LookRotation(_tiles[i].GetFloorTransform().forward, direction);
                _tiles[i].GetFloorTransform().rotation = delta;
            }

            if (!_tiles[i].IsEndTile())
            {
                if (!_tiles[i].IsEndTile())
                {
                    _tiles[i + 1].GetFloorTransform().position =
                        position + direction.normalized * _tiles[i].TileDistance * FloorBaseDistance;
                }
            }
        }
    }
}

[Serializable]
public class TileData
{
    public int TileDistance = 10;
    [SerializeField] private Transform _boneStart;
    [SerializeField] private Transform _boneEnd;
    [SerializeField] private Transform _floor;
    [SerializeField] private bool _endTile = false;

    /// <summary>
    /// old
    /// </summary>
    public void UpdateIK()
    {
        //IK
        var position = _boneStart.position;
        var direction = _boneEnd.position - position;
        var delta = Quaternion.LookRotation(_floor.forward, direction);
        _floor.rotation = delta;

        if (!_endTile)
        {
            _boneEnd.position = position + direction.normalized * TileDistance * FloorType1.FloorBaseDistance;
        }
    }

    public Transform GetStartTransform()
    {
        return _boneStart;
    }

    public Transform GetEndTransform()
    {
        return _boneEnd;
    }

    public Transform GetFloorTransform()
    {
        return _floor;
    }

    public bool IsEndTile()
    {
        return _endTile;
    }
}