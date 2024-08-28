using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallsSpawner : MonoBehaviour
{
    public Action LoseGame;
    public Action<int> CountBall;

    [SerializeField] private Rigidbody _ballPrefab;
    [SerializeField] private Transform _ballSpawnPoint;
    [SerializeField] private float _lerpValue;
    [SerializeField] private float _speed;
    [SerializeField] private float _timeDelay;
    [SerializeField] private int _maxBallsCount;

    [SerializeField] private TextMeshProUGUI _bonusCount;

    private int _currentBallsCount;
    private Rigidbody _currentBall;
    private bool _readyForShoot;

    private void Start()
    {
        _currentBallsCount = _maxBallsCount;
        SpawnBall();
    }

    private void FixedUpdate()
    {
        if ( _currentBall != null)
        {
            if (_currentBall.transform.localScale.x < 0.9f)
            {
                _currentBall.transform.localScale = Vector3.Lerp(_currentBall.transform.localScale, Vector3.one, _lerpValue);
            }
            else _readyForShoot = true;
        }
        
        _bonusCount.text = _currentBallsCount.ToString();  
    }
    public void AttackBall()
    {
        if (_readyForShoot)
        {
            if (_currentBallsCount > 0)
            {
                _currentBall.AddForce(Vector3.forward * _speed, ForceMode.Impulse);
                _currentBall = null;
                _readyForShoot = false;
                StartCoroutine(TimePerSpawn());
                CountBall?.Invoke(_currentBallsCount);

                _currentBallsCount--;
            }
            else LoseGame?.Invoke();
        }
    }
    private void SpawnBall()
    {
        _currentBall = Instantiate(_ballPrefab, gameObject.transform.position, gameObject.transform.rotation);
        _currentBall.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
    }
    private IEnumerator TimePerSpawn()
    {
        yield return new WaitForSeconds(_timeDelay);
        SpawnBall();
    }
}
