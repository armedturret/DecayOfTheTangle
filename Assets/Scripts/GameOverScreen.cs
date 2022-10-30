using System.Collections;

using UnityEngine;
using UnityEngine.InputSystem;

using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI deathsText;
    [SerializeField]
    private GameObject star;

    private void Awake()
    {
        deathsText.text = "And only restarted " + GameManager.instance.GetDeaths() + " times";

        star.SetActive(GameManager.instance.GetDeaths() == 0);

        MusicManager.instance.StopAll();
    }

    public void OnExit(InputValue value)
    {
        //open main menu on exit
        GameManager.instance.MainMenu();
    }
}