using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileBase : MonoBehaviour
{
    [SerializeField]
    private int _row = 2;

    [SerializeField]
    private int _column = 2;

    [SerializeField]
    private Vector3 _size = new Vector3(1f, 1f, 1f);

    protected Stack<GameObject> _objects = new Stack<GameObject>();

    public int ObjectCount => _objects.Count;

    public GameObject tile;

    void Start()
    {
        StartCoroutine(CoSpawnTile());
    }

    IEnumerator CoSpawnTile()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(1f);

            GameObject go = PoolManager.Instance.Pop(tile);
            AddToPile(go);
        }

        // for (int i = 0; i < 20; i++)
        // {
        //     yield return new WaitForSeconds(1f);

        //     GameObject go = RemoveFromPile();
        //     if ( go != null)
        //     PoolManager.Instance.Push(tile);
        // }
    }

    public void AddToPile(GameObject go)
    {
        _objects.Push(go);

        go.transform.position = GetPositionAt(_objects.Count - 1);
    }

    public GameObject RemoveFromPile()
    {
        if (_objects.Count == 0)
            return null;

        return _objects.Pop();
    }

    // 파일 인덱스가 주어지면, 위치를 리턴해줌.
    // 인덱스랑 좌표를 오고가게 해주는 유틸리티 함수
    private Vector3 GetPositionAt(int pileIndex)
    {
        Vector3 offset = new Vector3((_row - 1) * _size.x / 2, 0, (_column - 1) * _size.z / 2);
        Vector3 startPos = transform.position - offset;

        int row = (pileIndex / _row) % _column;
        int column = pileIndex % _row;
        int height = pileIndex / (_row * _column);

        float x = startPos.x + column * _size.x;
        float y = startPos.y + height * _size.y;
        float z = startPos.z + row * _size.z;

        return new Vector3(x, y, z);
    }
    #region Editor
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Vector3 offset = new Vector3((_row - 1) * _size.x / 2, 0, (_column - 1) * _size.z / 2); //0 번칸 위치부터 하나하나 만들기 위한 간격.
        Vector3 startPos = transform.position - offset; // 0번 칸의 위치.

        Gizmos.color = Color.yellow;

        for (int r = 0; r < _row; r++)
        {
            for (int c = 0; c < _column; c++)
            {
                Vector3 centor = startPos + new Vector3(r * _size.x, _size.y / 2, c * _size.z);
                Gizmos.DrawWireCube(centor, _size);
            }
        }
    }
#endif
    #endregion
}