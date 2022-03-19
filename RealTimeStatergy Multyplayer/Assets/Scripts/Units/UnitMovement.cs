using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Mirror;

public class UnitMovement : NetworkBehaviour
{
    [SerializeField]
    private Targeter targeter = null;

    [SerializeField]
    private NavMeshAgent agent = null;

    [SerializeField] private float chaseRange = 10f;

    public override void OnStartServer()
    {
        GameOverHandler.ServerOnGameOver += ServerHandleGameOver;
    }

    public override void OnStopServer()
    {
        GameOverHandler.ServerOnGameOver -= ServerHandleGameOver;
    }


    #region Server
    [ServerCallback]
    private void Update()
    {
        Targetable target = targeter.GetTarget();

        if(target != null)
        {
            if((target.transform.position - transform.position).sqrMagnitude > chaseRange * chaseRange)   //This is more "optimized" than doing the Unity.Distance because it doesnt sqr root
            {
                agent.SetDestination(target.transform.position);
            }
            else if(agent.hasPath)
            {
                agent.ResetPath();
            }

            return;
        }

        if(!agent.hasPath)
        {
            return;
        }
        if(agent.remainingDistance > agent.stoppingDistance)
        {
            return;
        }
        agent.ResetPath();
    }

    [Command]
    public void CmdMove(Vector3 position)
    {
        ServerMove(position);
    }
    [Server]
    public void ServerMove(Vector3 position)
    {
        targeter.ClearTarget();

        if(!NavMesh.SamplePosition(position, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            return;
        }
        agent.SetDestination(hit.position);
    }


    [Server]
    private void ServerHandleGameOver()
    {
        agent.ResetPath();
    }


    #endregion


    /*
    #region Client

    public override void OnStartAuthority()
    {
        maineCamera = Camera.main;
        base.OnStartAuthority();
    }
    [ClientCallback]
    private void Update()
    {
        //Debug.Log($"{gameObject.name} Ordered to move");
        if (!hasAuthority)
        {
            return;
        }
        //if(!Input.GetMouseButtonDown(0))
        if(!Mouse.current.rightButton.wasPressedThisFrame)
        {
            return;
        }
        //Debug.Log($"{gameObject.name} Ordered to move");
        Ray ray = maineCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if(!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            return;
        }

        CmdMove(hit.point);
    }

    #endregion
    */
}
