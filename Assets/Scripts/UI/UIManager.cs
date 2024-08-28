using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Space]
    [Header("Screens")]
    [SerializeField] private RectTransform _screenGame;
    [SerializeField] private RectTransform _screenPause;
    [SerializeField] private RectTransform _screenLose;
    [SerializeField] private RectTransform _screenWin;
    [Space]
    [Header("Screen Game Buttons")]
    [SerializeField] private Button _buttonPauseGameScreen;
    [SerializeField] private Button _buttonAttackGameScreen;
    [Space]
    [Header("Screen Pause Buttons")]
    [SerializeField] private Button _buttonPlayPauseScreen;
    [SerializeField] private Button _buttonMenuPauseScreen;
    [SerializeField] private Button _buttonReplayPauseScreen;
    [SerializeField] private Toggle _buttonSoundsPauseScreen;
    [Space]
    [Header("Screen Lose Buttons")]
    [SerializeField] private Button _buttonMenuLoseScreen;
    [SerializeField] private Button _buttonReplayLoseScreen;
    [SerializeField] private Toggle _buttonSoundsLoseScreen;
    [Space]
    [Header("Screen Win Buttons")]
    [SerializeField] private Button _buttonNextWinScreen;
    [SerializeField] private Button _buttonMenuWinScreen;
    [SerializeField] private Button _buttonReplayWinScreen;
    [SerializeField] private Toggle _buttonSoundsWinScreen;

    [Space][Header("Other")]
    
    [SerializeField] private BallsSpawner _ballsSpawner;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private TextMeshProUGUI _bonusCountText;
    [SerializeField] private int _bonusCountMax;
    [SerializeField] private int _ballCountMinForStar;
    [SerializeField] private RectTransform[] _stars;

    private bool _isPause;
    private int _bonusCountCurrent;
    private int _starsCount;
    private int _ballCountCurrent;
    private void Awake()
    {
        BallTriggerLogic.Bonus += Bonus;
        BallTriggerLogic.WinGame += WinGame;
        _ballsSpawner.LoseGame += LoseGame;
        _ballsSpawner.CountBall += BallCount;

        BonusCount();
    }
    private void OnEnable()
    {
        _buttonAttackGameScreen.onClick.AddListener(OnAttack);
        _buttonPauseGameScreen.onClick.AddListener(TogglePause);

        _buttonPlayPauseScreen.onClick.AddListener(TogglePause);
        _buttonMenuPauseScreen.onClick.AddListener(MainManu);
        _buttonReplayPauseScreen.onClick.AddListener(Replay);
        _buttonSoundsPauseScreen.onValueChanged.AddListener(SoundsToggle);

        _buttonMenuLoseScreen.onClick.AddListener(MainManu);
        _buttonReplayLoseScreen.onClick.AddListener(Replay);
        _buttonSoundsLoseScreen.onValueChanged.AddListener(SoundsToggle);

        _buttonNextWinScreen.onClick.AddListener(NextLVL);
        _buttonMenuWinScreen.onClick.AddListener(MainManu);
        _buttonReplayWinScreen.onClick.AddListener(Replay);
        _buttonSoundsWinScreen.onValueChanged.AddListener(SoundsToggle);
    }
    private void OnDisable()
    {
        _buttonAttackGameScreen.onClick.RemoveListener(OnAttack);
        _buttonAttackGameScreen.onClick.RemoveListener(OnAttack);
        _buttonPauseGameScreen.onClick.RemoveListener(TogglePause);

        _buttonPlayPauseScreen.onClick.RemoveListener(TogglePause);
        _buttonMenuPauseScreen.onClick.RemoveListener(MainManu);
        _buttonReplayPauseScreen.onClick.RemoveListener(Replay);
        _buttonSoundsPauseScreen.onValueChanged.RemoveListener(SoundsToggle);

        _buttonMenuLoseScreen.onClick.RemoveListener(MainManu);
        _buttonReplayLoseScreen.onClick.RemoveListener(Replay);
        _buttonSoundsLoseScreen.onValueChanged.RemoveListener(SoundsToggle);

        _buttonNextWinScreen.onClick.RemoveListener(NextLVL);
        _buttonMenuWinScreen.onClick.RemoveListener(MainManu);
        _buttonReplayWinScreen.onClick.RemoveListener(Replay);
        _buttonSoundsWinScreen.onValueChanged.RemoveListener(SoundsToggle);
    }
    private void OnDestroy()
    {
        BallTriggerLogic.Bonus -= Bonus;
        BallTriggerLogic.WinGame -= WinGame;
        _ballsSpawner.LoseGame -= LoseGame;
    }
    private void OnAttack()
    {
        _ballsSpawner.AttackBall();
    }
    private void TogglePause()
    {
        _isPause = !_isPause;
        _screenPause.gameObject.SetActive(_isPause);

        if (_isPause ) Time.timeScale = 0f;
        else Time.timeScale = 1f;
    }
    private void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    private void NextLVL()
    {
        // SceneManager.LoadScene();
        //Time.timeScale = 1f;
    }
    private void MainManu()
    {
        // SceneManager.LoadScene();
        //Time.timeScale = 1f;
    }
    private void SoundsToggle(bool isSound)
    {
       // if (isSound)_audioMixer.SetFloat("GameMusic", -80f);
       // else _audioMixer.SetFloat("GameMusic", 0f);

        _buttonSoundsPauseScreen.isOn = isSound;
        _buttonSoundsLoseScreen.isOn = isSound;
        _buttonSoundsWinScreen.isOn = isSound;
    }
    private void Bonus()
    {
        _bonusCountCurrent++;
        BonusCount();
    }
    private void WinGame()
    {
        _screenWin.gameObject.SetActive(true);
        Time.timeScale = 0f;

        _starsCount++;
        if (_bonusCountCurrent == _bonusCountMax) _starsCount++;
        if (_ballCountCurrent >= _ballCountMinForStar) _starsCount++;

        for (int i = 0; i < _stars.Length; i++)
        {
            if (i < _starsCount) _stars[i].gameObject.SetActive(true);
        }
    }
    private void LoseGame()
    {
        _screenLose.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    private void BonusCount()
    {
        var bonusCountCurrent = _bonusCountCurrent.ToString();
        var bonusCountMax = _bonusCountMax.ToString();
        _bonusCountText.text = bonusCountCurrent + "/" + bonusCountMax;
    }
    private void BallCount(int ballCount)
    {
        _ballCountCurrent = ballCount;
    }
}
