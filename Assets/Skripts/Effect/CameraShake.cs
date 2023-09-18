using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [Header("Noise")]
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _perlinNoiseTimeScakle = 1f;
    [SerializeField] private AnimationCurve _animationCurve;

    [Header("Recoil")]
    [SerializeField] private float _tension = 10f;
    [SerializeField] private float _damping = 10f;
    [SerializeField] private float _impuls = 10f;

    private Vector3 _shakeAngels = new Vector3();
    private Vector3 _recoilAngels = new Vector3();
    private Vector3 _recoilVelocity = new Vector3();

    private float _amplitude = 0;
    private float _duration = 1;
    private float _shakeTimer = -1;

    private void Update()
    {
        UpdateRecoil();
        UpdateShake();

        _cameraTransform.localEulerAngles = _shakeAngels + _recoilAngels;
    }

    [ContextMenu("MakeShake")]
    public void MakeShake()
    {
        MakeShake(15, 3);
    }


    public void MakeShake(float amplitude, float duration)
    {
        _amplitude = amplitude;
        _duration = Math.Max(duration, 0.05f);
        _shakeTimer = 1;
    }

    public void MakeRecoil()
    {
        MakeRecoil(-Vector3.right * UnityEngine.Random.Range(_impuls * 0.5f, _impuls));
    }

    public void MakeRecoil(Vector3 impulse)
    {
        _recoilVelocity += impulse;
    }

    private void UpdateShake()
    {
        if(_shakeTimer > 0)
            _shakeTimer -= Time.deltaTime/_duration;

        float time = Time.time * _perlinNoiseTimeScakle;

        _shakeAngels.x = Mathf.PerlinNoise(time, 0);
        _shakeAngels.y = Mathf.PerlinNoise(0, time);
        _shakeAngels.z = Mathf.PerlinNoise(time, time);

        _shakeAngels *= _amplitude;
        _shakeAngels *= _animationCurve.Evaluate(Mathf.Clamp01(1 - _shakeTimer));
    }

    private void UpdateRecoil()
    {
        _recoilAngels += _recoilVelocity * Time.deltaTime;
        _recoilVelocity += -_recoilAngels * Time.deltaTime * _tension;
        _recoilVelocity = Vector3.Lerp(_recoilVelocity, Vector3.zero, Time.deltaTime * _damping);
    }
}
