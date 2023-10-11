using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplayNew : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI highscoreText;
    private GameManagerNew gameManager;

    void Awake()
    {

    }

    void Start()
    {
        //scoreText = GetComponent<TextMeshProUGUI>();
        gameManager = GameManagerNew.instance;
        UpdateScoreDisplay();
    }

    void Update()
    {
        UpdateScoreDisplay();

    }

    void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + gameManager.score.ToString();
        comboText.text = "Combo: " + gameManager.combo.ToString();
        highscoreText.text = "Highscoere: " + PlayerPrefs.GetInt("HighscoreRhythmGame");
    }
}
