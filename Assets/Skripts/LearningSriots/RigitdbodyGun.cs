using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigitdbodyGun : MonoBehaviour
{
    [SerializeField] private Rigidbody _projectile;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private float _speed = 10;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody projectile = Instantiate(_projectile, _startPosition.position, _projectile.rotation);
            Vector3 forward = _startPosition.forward;
            projectile.velocity = forward * _speed;
        }
    }
}
