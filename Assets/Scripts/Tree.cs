using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEditorInternal;
using UnityEngine.UIElements;

public class TileTest : MonoBehaviour
{
    [SerializeField]
    TileBase _tile;
    Tilemap _tilemap;
    Tilemap _parentTilemap;

    void Awake()
    {
        GenerateTiles();
    }

    void GenerateTiles()
    {
        _tilemap = GetComponent<Tilemap>();
        _parentTilemap = transform.parent.GetComponent<Tilemap>();

        // 오브젝트 타일맵 경계와 크기 설정
        _tilemap.origin = _parentTilemap.origin + new Vector3Int(1, 1, 0);
        _tilemap.size = _parentTilemap.size - new Vector3Int(2, 2, 0);

        // 한 줄에 오브젝트 타일 2개씩 생성
        for (int i = _tilemap.origin.y; i < _tilemap.origin.y + _tilemap.size.y; i++)
        {
            RandomGenerateTiles(i);
            RandomGenerateTiles(i);
        }
    }

    void RandomGenerateTiles(int y)
    {
        // 충돌 검사 범위 정의
        Vector2Int collideRange = new Vector2Int(3, 3);

        while (true)
        {
            int x = UnityEngine.Random.Range(_tilemap.origin.x, _tilemap.origin.x + _tilemap.size.x);
            Collider2D isCollide = Physics2D.OverlapBox(new Vector2Int(x, y), collideRange, 0, LayerMask.GetMask("Player"));

            Vector3Int newPos = new Vector3Int(x, y, 0);
            // CheckNeighbor(newPos);

            // 충돌과 범위 검사
            if (isCollide != null || _tilemap.HasTile(newPos) || existNeighbors.Count > 0)
                continue;

            _tilemap.SetTile(newPos, _tile);
            return;
        }
    }

    // 인접한 8개 위치 중 타일이 존재하는 위치를 담는 배열
    List<Vector3Int> existNeighbors = new List<Vector3Int>();

    // 인접한 8개 위치의 타일 검사
    void CheckNeighbor(Vector3Int pos)
    {
        existNeighbors.Clear();

        Vector3Int[] neighbors = new Vector3Int[]
        {
            Vector3Int.up,
            Vector3Int.up + Vector3Int.right,
            Vector3Int.right,
            Vector3Int.right + Vector3Int.down,
            Vector3Int.down,
            Vector3Int.down + Vector3Int.left,
            Vector3Int.left,
            Vector3Int.left + Vector3Int.up
        };

        for (int i = 0; i < neighbors.Length; i++)
        {
            Vector3Int newPos = pos + neighbors[i];

            if (newPos.x < _tilemap.origin.x || newPos.x > _tilemap.origin.x + _tilemap.size.x || newPos.y < _tilemap.origin.y || newPos.y > _tilemap.origin.y + _tilemap.size.y)
                continue;
            if (_tilemap.HasTile(newPos) == false)
                continue;

            existNeighbors.Add(newPos);
        }
    }
}
