using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void selectLevel1()
    {
        PlayerPrefs.SetString("RhythGameLevel", "OdeLeicht");
    }
    public void selectLevel2()
    {
        PlayerPrefs.SetString("RhythGameLevel", "CanonLeicht");
    }
    public void selectLevel3()
    {
        PlayerPrefs.SetString("RhythGameLevel", "CarolSchwer");
    }
    public void selectLevel4()
    {
        PlayerPrefs.SetString("RhythGameLevel", "CanonSchwer");
    }
    public void startLevel()
    {
        Debug.Log("Started Level");
        SceneManager.LoadScene("NewRhythmGame");
    }
}
