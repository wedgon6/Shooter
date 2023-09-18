using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSound : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    private bool _isActive = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isActive)
        {
            _isActive = false;
            _source.Play();
        }
    }
}
