using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplayNew : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI accuracyText;
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
        scoreText.text = "Score: " + gameManager.roundedScore.ToString();
        comboText.text = "Combo: " + gameManager.combo.ToString();
        accuracyText.text = "Accuracy:" + gameManager.roundedHitAccuracy.ToString() + "%";
        highscoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighscoreOdeLeicht");
    }
}
