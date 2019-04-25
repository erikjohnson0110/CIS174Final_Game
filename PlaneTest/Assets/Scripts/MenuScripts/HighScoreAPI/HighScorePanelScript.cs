using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HighScorePanelScript : MonoBehaviour
{
    public GameObject scoreContainer;
    public GameObject rowPrefab;

    private List<HighScoreViewModel> scores;

    void Start()
    {
        scores = new List<HighScoreViewModel>();
        StartCoroutine(getScores());
    }

    IEnumerator getScores()
    {
        UnityWebRequest hs = UnityWebRequest.Get("https://cis174-eejohnson1-web.azurewebsites.net/api/HighScoreApi/GetAllHighScores/");
        yield return hs.SendWebRequest();

        if (hs.isNetworkError || hs.isHttpError)
        {
            Debug.Log(hs.error);
        }
        else
        {
            // populate scores.
            Debug.Log("Connection Successful");
            Debug.Log(hs.downloadHandler.text);
            var top = JsonConvert.DeserializeObject<List<HighScoreViewModel>>(hs.downloadHandler.text);
            Debug.Log("DONE");
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
        }
    }
}
