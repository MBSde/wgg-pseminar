using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highscoreText1;
    public TextMeshProUGUI highscoreText2;
    public TextMeshProUGUI highscoreText3;
    public TextMeshProUGUI highscoreText4;
    // Start is called before the first frame update
    void Start()
    {
        highscoreText1.text = "Highscore: " + PlayerPrefs.GetInt("HighscoreOdeLeicht", 0);
        highscoreText2.text = "Highscore: " + PlayerPrefs.GetInt("HighscoreCanonLeicht", 0);
        highscoreText3.text = "Highscore: " + PlayerPrefs.GetInt("HighscoreCarolSchwer", 0);
        highscoreText4.text = "Highscore: " + PlayerPrefs.GetInt("HighscoreCanonSchwer", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
