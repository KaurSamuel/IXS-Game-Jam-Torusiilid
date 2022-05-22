using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slider != null)
        {
            ScoreHolder.Volume = slider.value;
        }
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
