using System;
using EventDispatchers;
using UnityEngine;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button nextButton;

    [Header("Screens")]
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;

    public static event Action OnClick;

    private void Awake()
    {
        retryButton.onClick.AddListener( ()=>OnClick?.Invoke());
        nextButton.onClick.AddListener( ()=>OnClick?.Invoke());
    }

    private void OnEnable()
    {
        PlayerCollisionEventsDispatcher.OnPlayerIsDead += ShowLoseScreen;
        LevelGenerator.OnNextLevel += ShowWinScreen;
    }
    
    private void OnDisable()
    {
        PlayerCollisionEventsDispatcher.OnPlayerIsDead -= ShowLoseScreen;
        LevelGenerator.OnNextLevel -= ShowWinScreen;

    }

    private void ShowLoseScreen() => loseScreen.SetActive(true);
    private void ShowWinScreen() => winScreen.SetActive(true);
}
