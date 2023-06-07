using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFire
{
    public IEnumerator fireDame(int damage);

    public IEnumerator fireTimer(float timer);
}
