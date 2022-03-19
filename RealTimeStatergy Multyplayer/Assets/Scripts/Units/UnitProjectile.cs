using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class UnitProjectile : NetworkBehaviour
{
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private int damageToDeal = 20;
    [SerializeField] private float destoryAfterASeconds = 5f;
    [SerializeField] private float launchForce = 10f;

    private void Start()
    {
        rb.velocity = transform.forward * launchForce;
    }
    public override void OnStartServer()
    {
        Invoke(nameof(DestorySelf),  destoryAfterASeconds);
    }

    [ServerCallback]
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<NetworkIdentity>(out NetworkIdentity netowrkIdentity))
        {
            if(netowrkIdentity.connectionToClient == connectionToClient)
            {
                return;
            }
            if(other.TryGetComponent<Health>(out Health health))
            {
                health.DealDamage(damageToDeal);
            }
            DestorySelf();
        }
    }

    [Server]
    private void DestorySelf()
    {
        NetworkServer.Destroy(gameObject);
    }
}
