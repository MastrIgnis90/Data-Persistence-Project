using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string userName;
    public string highScoreUserName;
    public int highScoreValue;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string UserName;
        public int HighScore;
    }

    public void SaveHighScore()
    {
        var data = new SaveData
        {
            UserName = highScoreUserName,
            HighScore = highScoreValue
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highScore.json", json);
    }

    private void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highScore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreUserName = data.UserName;
            highScoreValue = data.HighScore;
        }
    }
}
