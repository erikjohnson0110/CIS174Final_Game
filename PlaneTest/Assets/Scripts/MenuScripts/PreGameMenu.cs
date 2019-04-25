using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PreGameMenu : MonoBehaviour
{
    public Button signInBtn;
    public Button noSignInBtn;
    public InputField emailInput;
    public InputField passwordInput;
    public GameObject errorText;

    private string emailInputString;
    private string passwordInputString;
    private UserViewModel currentUser;

    void Start()
    {
        signInBtn.onClick.AddListener(signInListener);
        noSignInBtn.onClick.AddListener(noSignInListener);
    }

    void signInListener()
    {
        errorText.SetActive(false);
        signInBtn.gameObject.SetActive(false);
        noSignInBtn.gameObject.SetActive(false);
        emailInputString = emailInput.text;
        passwordInputString = passwordInput.text;
        var co = StartCoroutine(logIn(emailInputString, passwordInputString));

        Debug.Log("END OF SIGN IN LISTENER");

    }

    void noSignInListener()
    {
        PlayerInfo.uvm = null;
        SceneManager.LoadScene("Menu");
    }

    IEnumerator logIn(string email, string password)
    {
        string getCommand = "https://cis174-eejohnson1-web.azurewebsites.net/api/GameSignInApi/signIn/?email=" + email + "&password=" + password;
        UnityWebRequest hs = UnityWebRequest.Get(getCommand);
        yield return hs.SendWebRequest();

        if (hs.isNetworkError || hs.isHttpError)
        {
            Debug.Log(hs.error);
            signInBtn.gameObject.SetActive(true);
            noSignInBtn.gameObject.SetActive(true);
            emailInput.text = "";
            passwordInput.text = "";
            errorText.SetActive(true);
        }
        else
        {
            //Debug.Log("Connection Successful");
            //Debug.Log(hs.downloadHandler.text);
            currentUser = JsonConvert.DeserializeObject<UserViewModel>(hs.downloadHandler.text);
            //Debug.Log("DONE");

            if (currentUser == null)
            {
                signInBtn.gameObject.SetActive(true);
                noSignInBtn.gameObject.SetActive(true);
                emailInput.text = "";
                passwordInput.text = "";
                errorText.SetActive(true);
            }
            else
            {
                PlayerInfo.uvm = currentUser;
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
