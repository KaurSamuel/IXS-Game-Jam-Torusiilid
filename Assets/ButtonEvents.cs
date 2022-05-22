using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    AudioSource m_audioClip;
    public GameObject ScoreboardText;
    public GameObject PlayernameInput;
    public GameObject playerScore;
    public GameObject Submitbutton;
    public GameObject ScoreDisplay;

    public string username_unconfirmed = "";
    // Start is called before the first frame update
    void Start()
    {
        RecieveScores();
        ScoreDisplay.GetComponent<TextMeshProUGUI>().text = ScoreHolder.Score.ToString();
        Debug.Log(ScoreHolder.Score.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NameChanged()
    {
        TMP_InputField PlayernameBox = PlayernameInput.GetComponent<TMP_InputField>();
        username_unconfirmed = PlayernameBox.text;
    }

    public void PlaySound()
    {
        m_audioClip = GetComponent<AudioSource>();
        m_audioClip.Play(0);
        //Debug.Log("Pressed");
    }

    public void SendScore()
    {
        StartCoroutine(Send(username_unconfirmed, ScoreHolder.Score));
        Submitbutton.SetActive(false);
    }

    IEnumerator Send(string Name, int Score){
        //string data = "{'Name':'" + Name + "','Score':'" + Score.ToString() + "'}";
        ScoreboardEntry entry = new ScoreboardEntry(Name, Score);
        yield return UnityWebRequest.Post("https://maker.ifttt.com/trigger/game_over/json/with/key/mxQ8nwvuGP5RWIBgR-HsHHmmo_l3Myf8TEwPfab6Aen", entry.ToJsonString()).SendWebRequest();    
    }

    public void RecieveScores()
    {
        StartCoroutine(Recieve());
    }

    IEnumerator Recieve(){
        UnityWebRequest www = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/1LYSmkx2AHC6DG6_9y2SQkuLkzIvPxG0RZpujGorNqAQ/gviz/tq?tqx=out:csv&sheet=Scores");
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            onSuccess(www.downloadHandler.text);
        }
    }

    void onSuccess(string result)
    {
        //Debug.Log(result);
        TextMeshProUGUI ScoreboardObject = ScoreboardText.GetComponent<TextMeshProUGUI>();
        string[] scores = result.Split("\n");
        //string final_scores = "";
        List<ScoreboardEntry> final_scores = new List<ScoreboardEntry>();
        foreach(string line in scores){
            string line_active = line.Replace("\\", "");
            line_active = line_active.Replace("\"", "");
            line_active = line_active.Replace("\'", "");
            //Debug.Log(line_active);
            int namespot = line_active.IndexOf("Name:") + 5;
            //Debug.Log(namespot);
            int scorespot = line_active.IndexOf("Score:") + 6;
            //Debug.Log(scorespot);
            int endspot = line_active.IndexOf("}", scorespot);
            //Debug.Log(endspot);
            if(namespot >= 0 && scorespot >= 7 && endspot >= 0)
            {
                string curr_name = line_active.Substring(namespot, scorespot-7-namespot);
                string curr_score = line_active.Substring(scorespot, endspot-scorespot);

                final_scores.Add(new ScoreboardEntry(curr_name, int.Parse(curr_score)));
                //final_scores += curr_name + " - " + curr_score + "\n";
            }
        }
        final_scores.Sort();
        string final_scores_text = "";
        List<int> indices = new List<int>();
        for(int i = 0; i < final_scores.Count; i++)
        {
            if(final_scores.FindIndex(x => x.Name == final_scores[i].Name) < i)
            //final_scores[i].Name
            {
                indices.Add(i);
                //Debug.Log(i);
            }
        }
        //Debug.Log(indices.ToString());
        int removals = 0;
        foreach(int index in indices)
        {
            final_scores.RemoveAt(index-removals);
            removals += 1;
        }
        foreach(ScoreboardEntry entry in final_scores)
        {
            final_scores_text += entry.ToString();
        }

        ScoreboardObject.text = final_scores_text;
        RecieveScores();
    }

}

public class ScoreboardEntry : IComparable<ScoreboardEntry>
{
    public ScoreboardEntry(string name, int score)
    {
        Name = name;
        Score = score;
    }
    public string Name;
    public int Score;

    public string ToJsonString()
    {
        return JsonUtility.ToJson(this);
    }

    public int CompareTo(ScoreboardEntry compareEntry)
    {
        return -this.Score.CompareTo(compareEntry.Score);
    }

    public override string ToString()
    {
        string output = Name + " - " + Score.ToString() + "\n";
        return output;
    }
}
