using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Kart
{
    internal override void KartDestroyed()
    {
        GameManager gameManager = GameManager.Get();
        if(!gameManager)
        {
            Destroy(gameObject);
            return; 
        }

        gameManager.playerWon = false;
        gameManager.EndGame();
        Destroy(gameObject);
    }
}
