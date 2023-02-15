using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int _level;
    private int _lives = 3;
    private int _coins;

    private void Awake()
    {
        if (Instance != null) DestroyImmediate(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void NewGame()
    {
        _lives = 3;
        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        _level = level;
        SceneManager.LoadScene($"Level {level}");
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        _lives--;
        if (_lives > 0) LoadLevel(1);
        else GameOver();
    }

    private void GameOver() => NewGame();

    public void AddCoins()
    {
        _coins++;
        if (_coins == 50)
        {
            AddLives();
            _coins = 0;
        }
    }

    public void AddLives()
    {
        _lives++;
    }
}
