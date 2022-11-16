using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string PlayerName
    {
        get => _data.PlayerName;
        set
        {
            _data.PlayerName = value;
            Save();
        }
    }

    public List<ScoreData> HighScores { get => _data.HighScores; }

    private const int _maxHighScores = 3;

    private _Data _data = new();

    private string _dataFilename() => Application.persistentDataPath + "/gamedata.json";

    public void Load()
    {
        var filename = _dataFilename();

        if (!File.Exists(filename)) return;

        var json = File.ReadAllText(filename);

        _data = JsonUtility.FromJson<_Data>(json);
    }

    // Tries to add the new score to the high score list, using the current player name.
    public void AddScore(int score)
    {
        _data.HighScores.Add(new ScoreData(_data.PlayerName, score));
        _data.HighScores.Sort(ScoreData.CompareHighScores);

        if (_data.HighScores.Count > _maxHighScores)
        {
            _data.HighScores.RemoveRange(_maxHighScores, _data.HighScores.Count - _maxHighScores);
        }

        Save();
    }

    public void Save()
    {
        var json = JsonUtility.ToJson(_data);

        File.WriteAllText(_dataFilename(), json);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    [System.Serializable]
    private class _Data
    {
        public string PlayerName;
        public List<ScoreData> HighScores = new();
    }

    [System.Serializable]
    public class ScoreData
    {
        public string PlayerName;
        public int Score;

        public ScoreData(string PlayerName, int Score)
        {
            this.PlayerName = PlayerName;
            this.Score = Score;
        }

        public static int CompareHighScores(ScoreData a, ScoreData b)
        {
            if (a == null)
            {
                return b == null ? 0 : -1;
            }

            if (b == null) return 1;

            return b.Score.CompareTo(a.Score);
        }
    }
}
