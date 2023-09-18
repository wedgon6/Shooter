using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    private void OnDrawGizmos()
    {
        if(_rigidbody.SweepTest(Vector3.down, out RaycastHit hitInfo, 10f, QueryTriggerInteraction.UseGlobal))
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(hitInfo.point + Vector3.up*0.5f, 0.5f);
        }

    }
}
