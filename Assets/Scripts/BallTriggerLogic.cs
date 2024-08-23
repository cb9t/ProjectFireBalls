using System;
using UnityEngine;

public class BallTriggerLogic : MonoBehaviour
{
    public static Action Bonus;
    public static Action WinGame;

    [SerializeField] private LayerMask _maintower;
    [SerializeField] private LayerMask _lastTower;
    [SerializeField] private LayerMask _bonus;
    [SerializeField] private LayerMask _shield;

   private void OnTriggerEnter(Collider collider)
   {
        if (_maintower == (_maintower | (1 << collider.gameObject.layer)))
        {   
            var parent = collider.gameObject.transform.parent;
            Destroy(parent.gameObject);
            Destroy(gameObject);
        }
        if (_lastTower == (_lastTower | (1 << collider.gameObject.layer)))
        {
            var parent = collider.gameObject.transform.parent;
            Destroy(parent.gameObject);
            Destroy(gameObject);
            WinGame?.Invoke();
        }
        if (_bonus == (_bonus | (1 << collider.gameObject.layer)))
        {
            Destroy(gameObject);
            Bonus?.Invoke();
        }
        if (_shield == (_shield | (1 << collider.gameObject.layer)))
        {
            Destroy(gameObject);
            collider.gameObject.GetComponent<ShieldLogic>().DamageShield();
        }
    }

}
