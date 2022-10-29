using System.Collections;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int levelOneIndex = 1;
    public int finalLevelIndex = 2;

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

    public void ResetSave()
    {
        PlayerPrefs.SetInt("level", levelOneIndex);
        PlayerPrefs.Save();
    }
}