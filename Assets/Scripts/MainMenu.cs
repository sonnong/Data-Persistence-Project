using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
#if UNITY_EDITOR
    using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu Instance;
    public TMP_InputField NameInput;
    public TextMeshProUGUI MenuBestScore;
    public string PlayerName, BestPlayer;
    public int BestScore;

    class HighScore
    {
        public string Player;
        public int Score;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        BestPlayer = LoadScore().Player;
        BestScore = LoadScore().Score;
        MenuBestScore.text = $"Best Score: {BestPlayer}: {BestScore}";
    }

    public void StartNew()
    {
        PlayerName = NameInput.text;
        SceneManager.LoadScene(1);
    }

    public void SaveScore()
    {
        HighScore data = new();
        data.Player = BestPlayer;
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

    public void Exit()
    {
        # if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        # else
            Application.Quit();
        # endif
    } 

}
