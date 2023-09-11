using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RiditbiodyCentreMass : MonoBehaviour
{
    [SerializeField] private Transform _centerOfMass;

    private void Awake()
    {
        var rig = GetComponent<Rigidbody>();
        rig.centerOfMass = _centerOfMass.localPosition;
    }
}
