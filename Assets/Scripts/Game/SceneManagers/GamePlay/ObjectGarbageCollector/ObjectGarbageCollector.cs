using System.Collections.Generic;
using UnityEngine;

public class ObjectGarbageCollector : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameEnteties; // �������, ������� ����� ���������
    [SerializeField] private Transform _player;              // ����� (������ � ����������)
    [SerializeField] private float _distanceBelowPlayer = 10f; // ����������, �� ������� ������� ���������

    void Update()
    {
        // �������� ������� ������ �� Y
        float thresholdY = _player.position.y - _distanceBelowPlayer;

        // ���������� ��������� ������, ����� �������� ����������� �� ����� ��������
        List<GameObject> toRemove = new List<GameObject>();

        foreach (GameObject obj in _gameEnteties)
        {
            if (obj != null && obj.transform.position.y < thresholdY)
            {
                Destroy(obj);
                toRemove.Add(obj);
            }
        }

        // ������� ������������ ������� �� ������
        foreach (GameObject obj in toRemove)
        {
            _gameEnteties.Remove(obj);
        }
    }

    /// <summary>
    /// ���������� ������� � ������ �������������
    /// </summary>
    public void RegisterObject(GameObject obj)
    {
        if (!_gameEnteties.Contains(obj))
        {
            _gameEnteties.Add(obj);
        }
    }
}
