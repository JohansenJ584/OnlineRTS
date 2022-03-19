using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class Health : NetworkBehaviour
{
    [SerializeField] private int maxHealth = 100;

    [SyncVar(hook = nameof(HandleHealthUpdated))]
    private int currenthealth;

    public event Action ServerOnDie;

    public event Action<int, int> ClientOnHealthUpdated;

    #region Server
    public override void OnStartServer()
    {
        currenthealth = maxHealth;
        UnitBase.ServerOnPlayerDie += ServerHandlePlayerDie;
    }

    public override void OnStopServer()
    {
        UnitBase.ServerOnPlayerDie -= ServerHandlePlayerDie;
    }
    [Server]
    private void ServerHandlePlayerDie(int connectionId)
    {
        if(connectionToClient.connectionId != connectionId)
        {
            return;
        }
        DealDamage(currenthealth);
    }

    [Server]
    public void DealDamage(int damageAmount)
    {
        if(currenthealth == 0)
        {
            return;
        }
        currenthealth = Mathf.Max(currenthealth - damageAmount, 0);

        if(currenthealth != 0)
        {
            return;
        }
        ServerOnDie?.Invoke();
    }
    #endregion

    #region Client
    private void HandleHealthUpdated(int oldHealth, int newHealth)
    {
        ClientOnHealthUpdated?.Invoke(newHealth, maxHealth);
    }
    #endregion

}
