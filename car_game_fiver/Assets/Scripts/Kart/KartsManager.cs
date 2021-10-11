using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartsManager : MonoBehaviour
{
    [SerializeField] List<Kart> enemies;
    [SerializeField] Kart player;
    GameManager gameManager;

    private void Start()
    {
        //Game Manager
        gameManager = GameManager.Get();

        //Player
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        player.KartDied += OnPlayerDeath;

        //Enemies
        foreach (var enemyGO in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            enemies.Add(enemy);
            enemy.KartDied += OnEnemyDeath;
        }
    }

    void AllEnemiesDied()
    {
        gameManager.playerWon = true;
        gameManager.EndGame();
    }

   
    void OnEnemyDeath(Kart enemy)
    {
        gameManager.score++;
        enemies.Remove(enemy);
        if (enemies.Count < 1)
        {
            AllEnemiesDied();
        }
    }
    void OnPlayerDeath(Kart player)
    {
        gameManager.playerWon = false;
        gameManager.EndGame();
    }
}
