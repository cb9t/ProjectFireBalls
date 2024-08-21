using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShieldLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameObject;
    [SerializeField] private Vector3 _scaleOnDamage;
    [SerializeField] private float _lerpValue;
    private int _count = 0;
    private bool _isDamaged = false;

    private void Start()
    {
        
    }
    public void DamageShield()
    {
        _count++;
        if (_count == _gameObject.Length) Destroy(gameObject);
        else _isDamaged = true;

        foreach (var gameObject in _gameObject.Select((value, index) => new {value, index})) 
        {
            if (gameObject.index == _count) gameObject.value.SetActive(true);
            else gameObject.value.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if (_isDamaged) 
        {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, _scaleOnDamage, _lerpValue);
            if (gameObject.transform.localScale == _scaleOnDamage) _isDamaged = false;
        }
        if (!_isDamaged && gameObject.transform.localScale != Vector3.one)
        {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, Vector3.one, _lerpValue);
        }
    }
}
