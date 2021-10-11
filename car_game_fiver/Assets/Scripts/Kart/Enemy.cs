using UnityEngine;
using UnityEngine.AI;

public class Enemy : Kart
{
    internal enum MachineState { SEARCH, FOLLOW }

    [SerializeField] Transform player;
    [SerializeField] GameObject explosionEffect;
    internal MachineState state;
    NavMeshAgent navMesh;

    #region Unity Events
    internal void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
    }
    internal void Update()
    {
        RunStateMachine();
    }
    #endregion

    virtual internal void GetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    virtual internal void FollowPlayer()
    {
        navMesh.destination = player.position;
    }
    void RunStateMachine()
    {
        switch (state)
        {
            case MachineState.SEARCH:
                GetPlayer();
                if (player)
                {
                    state = MachineState.FOLLOW;
                }
                break;
            case MachineState.FOLLOW:
                if (!player)
                {
                    state = MachineState.SEARCH;
                    return;
                }
                FollowPlayer();
                break;
            default:
                break;
        }
    }

    internal override void KartDestroyed()
    {
        KartDied.Invoke(this); //warn kartManager
        GameObject explosion = Instantiate(explosionEffect, transform);
        explosion.transform.parent = transform.parent;
        Destroy(gameObject);
    }
}