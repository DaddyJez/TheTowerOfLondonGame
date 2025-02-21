using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class DragController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _liftHeight = 3f;
    [SerializeField] private float _dragSpeed = 10f;

    [Header("Events")]
    public UnityEvent<GameObject, GameObject> OnDragEnd;

    private Camera _mainCamera;
    private Vector3 _offset;
    private bool _isDragging;
    private Vector3 _startPosition;

    private void Start()
    {
        _mainCamera = Camera.main;
        _startPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (_isDragging) return;
        
        Ring ring = GetComponent<Ring>();
        if (ring == null || ring.CurrentRod == null || ring.CurrentRod.GetTopRing() != ring)
        {
            Logger.Warn("Можно брать только верхнее кольцо!");
            return;
        }

        _isDragging = true;
        _startPosition = transform.position;
        transform.position = new Vector3(_startPosition.x, _liftHeight, _startPosition.z);
    }

    private void OnMouseDrag()
    {
        if (!_isDragging) return;

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 target = ray.GetPoint(distance) + _offset;
            target.y = _liftHeight;
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * _dragSpeed);
        }
    }

    // тут я использую public для последующего тестирования
    public void OnMouseUp()
    {
        if (!_isDragging) return;
        _isDragging = false;

        RodManager rodManager = FindFirstObjectByType<RodManager>();
        GameObject nearestRod = rodManager.GetNearestRod(transform.position);
        OnDragEnd?.Invoke(gameObject, nearestRod);
    }
}