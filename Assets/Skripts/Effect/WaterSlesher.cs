using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSlesher : MonoBehaviour
{
    [SerializeField] private Transform _slashPrefab;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public Vector3 RaycastToVirtualPlane(Vector3 startPoint,Vector3 direction)
    {
        Plane plane = new Plane(Vector3.up, _transform.position);
        Ray ray =  new Ray(startPoint,direction);

        if (plane.Raycast(ray, out float enter))
        {
            return (startPoint + direction.normalized * enter);
        }

        return Vector3.zero;
    }

    public bool CheckWater(Vector3 input)
    {
        return input.y < _transform.position.y;
    }

    public void TryCreateWaterSlash(Vector3 startPoint,Vector3 endPoint)
    {
        if (CheckWater(endPoint))
        {
            var point =  RaycastToVirtualPlane(startPoint, endPoint - startPoint);
            Destroy(Instantiate(_slashPrefab, point, _slashPrefab.rotation), 5);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
