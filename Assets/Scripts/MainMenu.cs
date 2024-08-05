using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TMP_InputField NameInput;
    public static string PlayerName;
    public static int BestScore;

    class HighScore
    {
        public string Player;
        public int Score;
    }

    void Awake()
    {
        BestScore = LoadScore().Score;
    }

    public void StartNew()
    {
        PlayerName = NameInput.text;
        SceneManager.LoadScene(1);
    }

    void SaveScore()
    {
        HighScore data = new();
        data.Player = PlayerName;
        data.Score = BestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }
    
    HighScore LoadScore()
    {
        HighScore data = new();
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            data = JsonUtility.FromJson<HighScore>(json);
        }

        return data;
    } 

}
