using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-10)]
public class LevelManager : Singelton<LevelManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        if (!PlayerPrefs.HasKey(StringHelper.CURRENT_LEVEL))
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL, 0);
        }

    }
    private void Start()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL));
    }

    public void LoadNextLevel()
    {
        int currentLevel = PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL);
        currentLevel++;
        currentLevel %= 3;
        PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL, currentLevel);
        SceneManager.LoadScene(currentLevel);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
