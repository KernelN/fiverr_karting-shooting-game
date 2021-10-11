using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kart : MonoBehaviour, IHittable
{
    public int health;
    public Action<Kart> KartDied;

    public void Hitted()
    {
        KartHitted();
    }
    internal void KartHitted() 
    {
        health--;
        if (health <= 0)
        {
            KartDestroyed();
        }
    }
    internal virtual void KartDestroyed() { }
}
