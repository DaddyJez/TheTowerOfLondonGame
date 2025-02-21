using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Rod : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _ringSpacing = 1.0f;
    [SerializeField] private float _baseHeight = 0.5f;
    [SerializeField] private bool _autoDetectRings = true;

    [SerializeField] private int _index;
    public int Index => _index;
    
    private List<Ring> _rings = new List<Ring>();
    public List<int> GetRingSizes() => _rings.Select(r => r.Size).ToList(); 

    private void Start()
    {
        if (_autoDetectRings) DetectInitialRings();
        UpdateRingPositions();
    }

    private void DetectInitialRings()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(0.5f, 10f, 0.5f));
        foreach (Collider col in colliders)
        {
            Ring ring = col.GetComponent<Ring>();
            if (ring != null && !_rings.Contains(ring))
            {
                _rings.Add(ring);
                ring.CurrentRod = this;
            }
        }
        _rings.Sort((a, b) => a.transform.position.y.CompareTo(b.transform.position.y));
    }

    public bool TryAddRing(Ring ring)
    {
        if (_rings.Count > 0 && _rings[_rings.Count - 1].Size < ring.Size)
        {
            Logger.Warn($"Нельзя положить {ring.Size} на {_rings[_rings.Count - 1].Size}");
            return false;
        }

        if (ring.CurrentRod != null)
            ring.CurrentRod.RemoveRing(ring);

        _rings.Add(ring);
        ring.CurrentRod = this;
        UpdateRingPositions();
        Logger.Info($"Кольцо {ring.name} добавлено на стержень {name}");
        return true;
    }

    public void RemoveRing(Ring ring)
    {
        _rings.Remove(ring);
        UpdateRingPositions();
        Logger.Info($"Кольцо {ring.name} удалено со стержня {name}");
    }

    private void UpdateRingPositions()
    {
        for (int i = 0; i < _rings.Count; i++)
        {
            _rings[i].transform.position = new Vector3(
                transform.position.x,
                _baseHeight + (i * _ringSpacing),
                transform.position.z
            );
        }
    }

    public Ring GetTopRing() => _rings.Count > 0 ? _rings[_rings.Count - 1] : null;

    public bool CheckConfiguration(int[] targetSizes)
    {
        if (_rings.Count != targetSizes.Length)
        {
            Logger.Warn($"Несоответствие количества колец: {_rings.Count} vs {targetSizes.Length}");
            return false;
        }

        for (int i = 0; i < targetSizes.Length; i++)
        {
            if (_rings[i].Size != targetSizes[i])
            {
                Logger.Warn($"Несоответствие на позиции {i}: {_rings[i].Size} vs {targetSizes[i]}");
                return false;
            }
        }

        return true;
    }
}