using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public GameObject inGamePanel;

    public Button optionsBtn;
    public Button exitGameBtn;
    public Button returnToGameBtn;

    private bool isOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        optionsBtn.onClick.AddListener(optionsButtonClickEvent);
        exitGameBtn.onClick.AddListener(exitButtonClickEvent);
        returnToGameBtn.onClick.AddListener(returnToGameClickEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                closeMenu();
                isOpen = false;
            }
            else
            {
                openMenu();
                isOpen = true;
            }
        }
    }

    void optionsButtonClickEvent()
    {

    }

    void exitButtonClickEvent()
    {
        Application.Quit();
    }

    void returnToGameClickEvent()
    {
        closeMenu();
    }

    void openMenu()
    {
        Time.timeScale = 0;
        inGamePanel.SetActive(true);
    }

    void closeMenu()
    {
        inGamePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
