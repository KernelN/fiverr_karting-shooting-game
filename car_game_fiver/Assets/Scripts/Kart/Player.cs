using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Kart
{
    internal override void KartDestroyed()
    {
        KartDied.Invoke(this); //warn kartManager
        Destroy(gameObject);
    }
}
