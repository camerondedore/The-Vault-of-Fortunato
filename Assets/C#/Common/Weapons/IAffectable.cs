using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAffectable
{
    void Affect(float multiplier, Vector3 inheritedVelocity);
}
