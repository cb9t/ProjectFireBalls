using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button _buttonAttack;
    [SerializeField] private BallsSpawner _ballsSpawner;

    private void OnEnable()
    {
        _buttonAttack.onClick.AddListener(OnAttack);
    }
    private void OnDisable()
    {
        _buttonAttack.onClick.RemoveListener(OnAttack);
    }
    private void OnAttack()
    {
        _ballsSpawner.AttackBall();
    }
}
