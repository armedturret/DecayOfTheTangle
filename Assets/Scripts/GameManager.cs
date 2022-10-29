using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int levelOneIndex = 1;
    public int finalLevelIndex = 2;
    public int mainMenuIndex = 0;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetLevel()
    {
        return PlayerPrefs.GetInt("level", levelOneIndex);
    }

    //sets highest unlock value
    public void UnlockLevel(int buildIndex)
    {
        int newUnlock = Mathf.Max(GetLevel(), buildIndex);
        newUnlock = Mathf.Min(newUnlock, finalLevelIndex);
        PlayerPrefs.SetInt("level", newUnlock);
        PlayerPrefs.Save();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuIndex);
    }

    public void ReloadLevel()
    {
        PlayerPrefs.SetInt("deaths", PlayerPrefs.GetInt("deaths") + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CompleteLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        UnlockLevel(nextLevel);
        //determine if next level should be shown
        if(nextLevel > finalLevelIndex)
        {
            //load the game over scene (non existent)
            MainMenu();
        }
        else
        {
            SceneManager.LoadScene(nextLevel);
        }
    }

    public void Continue()
    {
        if (GetLevel() == levelOneIndex)
            ResetSave();
        SceneManager.LoadScene(GetLevel());
    }

    public void ResetSave()
    {
        PlayerPrefs.SetInt("level", levelOneIndex);
        PlayerPrefs.SetInt("deaths", 0);
        PlayerPrefs.Save();
    }
}