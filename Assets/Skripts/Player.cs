using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 7f;
    [SerializeField] private float _strafeSpeed = 7f;
    [SerializeField] private float _jumpSpeed = 7f;
    [SerializeField] private float _gravityFactor = 2f;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _horizontalTunrSensitivity = 10;
    [SerializeField] private float _verticalTunrSensitivity = 10;
    [SerializeField] private float _verticalMinAngle = -89f;
    [SerializeField] private float _verticalMaxAngle = 89f;

    [Header("Weapon")]
    [SerializeField] private Shotgun _shotgun;

    private Vector3 _verticalVelosity;
    private Transform _transform;
    private CharacterController _characterController;
    private float _cameraAngle = 0;

    private void Awake()
    {
        _transform = transform;
        _characterController = GetComponent<CharacterController>();
        _cameraAngle = _cameraTransform.localEulerAngles.x;
        _shotgun.Initialize(_characterController);
    }

    private void Update()
    {
        Movment();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _shotgun.Shoot(_cameraTransform.position,_cameraTransform.forward);
        }
    }

    private void Movment()
    {
        Vector3 forward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;

        _cameraAngle -= Input.GetAxis("Mouse Y") * _verticalTunrSensitivity;
        _cameraAngle = Mathf.Clamp(_cameraAngle, _verticalMinAngle, _verticalMaxAngle);
        _cameraTransform.localEulerAngles = Vector3.right * _cameraAngle;

        _transform.Rotate(Vector3.up * _horizontalTunrSensitivity * Input.GetAxis("Mouse X"));

        if (_characterController != null)
        {
            Vector3 palyerSpeed = forward * Input.GetAxis("Vertical") * _speed + right * Input.GetAxis("Horizontal") * _strafeSpeed;

            if (_characterController.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _verticalVelosity = Vector3.up * _jumpSpeed;
                }
                else
                {
                    _verticalVelosity = Vector3.down;
                }

                _characterController.Move((palyerSpeed + _verticalVelosity) * Time.deltaTime);
            }
            else
            {
                Vector3 horizontalVelocity = _characterController.velocity;
                horizontalVelocity.y = 0;
                _verticalVelosity += Physics.gravity * Time.deltaTime * _gravityFactor;
                _characterController.Move((horizontalVelocity + _verticalVelosity) * Time.deltaTime);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var character = GetComponent<CharacterController>();
        Gizmos.DrawWireCube(transform.position,Vector3.right + Vector3.forward + Vector3.up * character.height);
    }
}
