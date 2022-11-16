using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Text _highScoresTitle;
    [SerializeField] private TMP_Text _highScoresText;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneHelper.MainSceneIndex());
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else  
        Application.Quit();
#endif
    }

    private void Start()
    {
        _updateUI();
    }

    private void OnEnable()
    {
        _inputField.onSubmit.AddListener(_setPlayerName);
    }

    private void OnDisable()
    {
        _inputField.onSubmit.RemoveListener(_setPlayerName);
    }

    private void _setPlayerName(string playerName)
    {
        GameManager.Instance.PlayerName = playerName;
    }

    private void _updateUI()
    {
        _inputField.SetTextWithoutNotify(GameManager.Instance.PlayerName);

        var highScores = GameManager.Instance.HighScores;
        var hasHighScores = highScores.Count > 0;

        _highScoresText.text = "";

        foreach (var highScore in highScores)
        {
            _highScoresText.text += $"{highScore.PlayerName}        {highScore.Score}{Environment.NewLine}";
        }

        _highScoresTitle.gameObject.SetActive(hasHighScores);
        _highScoresText.gameObject.SetActive(hasHighScores);
    }
}
