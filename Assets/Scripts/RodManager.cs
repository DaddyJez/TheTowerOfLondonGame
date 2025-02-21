using UnityEngine;

public class RodManager : MonoBehaviour
{
    [SerializeField] private float _snapDistance = 1.5f;
    private GameObject[] _rods;

    private void Start()
    {
        // Кэшируем стержни один раз при старте
        _rods = GameObject.FindGameObjectsWithTag("Rod");
    }

    public GameObject GetNearestRod(Vector3 position)
    {
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject rod in _rods)
        {
            Vector2 rodXZ = new Vector2(rod.transform.position.x, rod.transform.position.z);
            Vector2 posXZ = new Vector2(position.x, position.z);
            float distance = Vector2.Distance(rodXZ, posXZ);

            if (distance < minDistance && distance <= _snapDistance)
            {
                minDistance = distance;
                nearest = rod;
            }
        }
        return nearest;
    }
}