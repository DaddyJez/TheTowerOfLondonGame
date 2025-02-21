using UnityEngine;

public class Ring : MonoBehaviour
{
    [SerializeField] private int _size; // 1 (маленькое), 2, 3 (большое)
    public int Size => _size;
    public Rod CurrentRod { get; set; }
    public Vector3 OriginalPosition { get; private set; }

    private void Start()
    {
        OriginalPosition = transform.position;
    }

    // Метод для тестовой инициализации
    public void Init(int size)
    {
        _size = size;
    }

    public void ResetToOriginalPosition()
    {
        transform.position = OriginalPosition;
        CurrentRod = null;
    }
}