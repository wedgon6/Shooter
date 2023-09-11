using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _cooldown;

    [SerializeField] private GameObject _target;
    [SerializeField] private Rigidbody _bullet;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        var waitForSeconds = new WaitForSeconds(_cooldown);

        while (true)
        {
            Vector3 derection = (_target.transform.position - transform.position).normalized;
            var newBullet = Instantiate(_bullet, transform.position + derection, Quaternion.identity);

            newBullet.transform.up = derection;
            newBullet.velocity = derection * _speed;

            yield return waitForSeconds;
        }
    }
}
