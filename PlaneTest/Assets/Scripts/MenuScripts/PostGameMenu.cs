using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PostGameMenu : MonoBehaviour
{
    public Button returnToMainBtn;
    public Text scoreValueText;

    public GameObject scoreContainer;
    public GameObject rowPrefab;
    public GameObject errorMessage;

    private List<HighScoreViewModel> scores;

    // Start is called before the first frame update
    void Start()
    {
        returnToMainBtn.onClick.AddListener(returnToMainBtnListener);
        returnToMainBtn.gameObject.SetActive(false);

        scoreValueText.text = PlayerInfo.getScoreString();

        scores = new List<HighScoreViewModel>();

        // API test data and method.
        //string testGUID = "acfcd50d-c3ca-4df4-ad00-fefdc883938f";
        //int testScore = 45000;
        //StartCoroutine(setScore(testGUID, testScore));

        if (PlayerInfo.uvm != null)
        {
            StartCoroutine(setScore(PlayerInfo.uvm.PersonId.ToString(), PlayerInfo.getScore()));
        }
        else
        {
            StartCoroutine(getScores());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void returnToMainBtnListener()
    {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator getScores()
    {
        UnityWebRequest hs = UnityWebRequest.Get("https://cis174-eejohnson1-web.azurewebsites.net/api/HighScoreApi/GetTopTen/");
        yield return hs.SendWebRequest();

        if (hs.isNetworkError || hs.isHttpError)
        {
            Debug.Log(hs.error);
            errorMessage.SetActive(true);
            returnToMainBtn.gameObject.SetActive(true);
        }
        else
        {
            // populate scores.
            //Debug.Log("Connection Successful");
            //Debug.Log(hs.downloadHandler.text);
            var top = JsonConvert.DeserializeObject<List<HighScoreViewModel>>(hs.downloadHandler.text);
            //Debug.Log("DONE");
            int position = 1;
            foreach (HighScoreViewModel vm in top)
            {
                GameObject newRow = Instantiate(rowPrefab);
                newRow.transform.SetParent(scoreContainer.transform);
                newRow.GetComponent<ScoreRowScript>().UserName.text = position + ") " + vm.userName;
                newRow.GetComponent<ScoreRowScript>().Score.text = vm.getScoreString();
                newRow.GetComponent<ScoreRowScript>().Date.text = vm.getDateString();
                position++;
            }
            returnToMainBtn.gameObject.SetActive(true);
        }
    }

    IEnumerator setScore(string guid, int score)
    {
        string postUrl = "https://cis174-eejohnson1-web.azurewebsites.net/api/HighScoreApi/SetUserHighScore/?param1=" + guid + "&param2=" + score;
        UnityWebRequest sr = UnityWebRequest.Post(postUrl, ""); // this constructor was not working, so I left second param blank and got it to work
        yield return sr.SendWebRequest();
        //Debug.Log(sr.url);

        if (sr.isNetworkError || sr.isHttpError)
        {
            Debug.Log(sr.error);
            errorMessage.SetActive(true);
            returnToMainBtn.gameObject.SetActive(true);
        }
        else
        {
            //Debug.Log("POST CALL TO API SUCCESSFUL");
            string result = sr.downloadHandler.text;

            if (result.Equals("Success!"))
            {
                scoreValueText.text += " - A NEW PERSONAL BEST!";
            }
            StartCoroutine(getScores());
        }
    }

}
