using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _masDistance = 500f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Projectale _prefab;
    [SerializeField] private float _velosity = 100f;
    [SerializeField] private float _impactForce = 10f;

    [SerializeField] private Transform _decal;
    [SerializeField] private float _decalOfSet;

    [SerializeField] private ShootEffect _shotEffect;

    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _reloadSound;

    private Vector3 _startPoint;
    private Vector3 _direction;
    private Collider _collider;

    public void Initialize(CharacterController characterController)
    {
        _collider = characterController as Collider;
    }

    public void Shoot(Vector3 startPoint, Vector3 direction)
    {
        _startPoint = startPoint;
        _direction = direction;

        //ProjectaleShoot(startPoint, direction * _velosity);
        ReycastShoot(startPoint, direction);
        _shotEffect.Perform();
        _animator.SetTrigger("Shoot");
    }

    private void ProjectaleShoot(Vector3 startPoint, Vector3 velosity)
    {
        var projectale = Instantiate(_prefab);
        projectale.Initialize(_damage, _collider);
        projectale.Shoot(startPoint, velosity);
    }

    private void ReycastShoot(Vector3 startPoint, Vector3 direction)
    {
        if (Physics.SphereCast(startPoint, 0.1f, direction, out RaycastHit hit, _masDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            var decal = Instantiate(_decal, hit.transform);
            decal.position = hit.point + hit.normal * _decalOfSet;
            decal.LookAt(hit.point);
            decal.Rotate(Vector3.up, 180, Space.Self);

            var health = hit.collider.GetComponentInParent<AbstratHealth>();

            if (health != null)
            {
                health.TakeDamage(_damage);
            }

            var victimBody = hit.rigidbody;

            if(victimBody != null)
            {
                victimBody.AddForceAtPosition(direction * _impactForce, hit.point,ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (Physics.SphereCast(_startPoint, 0.1f, _direction, out RaycastHit hit, _masDistance, _layerMask, QueryTriggerInteraction.Ignore))
        {
            Gizmos.DrawLine(_startPoint, hit.point);
        }
    }

    public void ShootSound()
    {
        _reloadSound.Play();
    }
}
