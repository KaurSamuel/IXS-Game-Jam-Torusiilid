using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        if (volumeSlider != null )
        {
            volumeSlider.value = ScoreHolder.Volume;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (volumeSlider != null )
        {
            ScoreHolder.Volume = volumeSlider.value;   
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
