using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainText;

    public Button newGameBtn;
    public Button highScoresBtn;
    public Button optionsBtn;
    public Button exitBtn;
    public Button optionsReturnToMainBtn;
    public Button highScoresReturnToMainBtn;

    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject highScorePanel;
    //public GameObject signInPanel;

    // Start is called before the first frame update
    void Start()
    {
        exitBtn.onClick.AddListener(exitButtonClickEvent);
        optionsBtn.onClick.AddListener(optionsButtonClickEvent);
        optionsReturnToMainBtn.onClick.AddListener(optionsReturnToMainClickEvent);
        newGameBtn.onClick.AddListener(newGameClickEvent);
        highScoresBtn.onClick.AddListener(highScoreBtnClickEvent);
        highScoresReturnToMainBtn.onClick.AddListener(highScoresReturnToMainBtnClickEvent);
    }

    void exitButtonClickEvent()
    {
        Debug.Log("EXIT BUTTON CLICKED.");
        Application.Quit();
    }

    void optionsButtonClickEvent()
    {
        mainPanel.SetActive(false);
        mainText.SetActive(false);
        optionsPanel.SetActive(true);
    }

    void optionsReturnToMainClickEvent()
    {
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
        mainText.SetActive(true);
    }

    void newGameClickEvent()
    {
        PlayerInfo.resetScore();
        SceneManager.LoadScene("Level1");
    }

    void highScoreBtnClickEvent()
    {
        mainPanel.SetActive(false);
        mainText.SetActive(false);
        highScorePanel.SetActive(true);
    }

    void highScoresReturnToMainBtnClickEvent()
    {
        highScorePanel.SetActive(false);
        mainPanel.SetActive(true);
        mainText.SetActive(true);
    }
}