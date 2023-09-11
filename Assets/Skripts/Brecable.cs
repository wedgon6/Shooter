using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brecable : AbstratHealth
{
    internal override void Die()
    {
        Destroy(gameObject);
    }
}
