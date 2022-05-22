using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGameButtonClick()
    {
        SceneManager.LoadScene("Tutourial");
    }
    public void MainMenuButtonClick()
    {
        SceneManager.LoadScene("Main menu");
    }
    public void ScoreboardButtonClick()
    {
        SceneManager.LoadScene("ScoreBoard");
    }
    public void CreditsButtonClick()
    {
        SceneManager.LoadScene("Credits");
    }
}
