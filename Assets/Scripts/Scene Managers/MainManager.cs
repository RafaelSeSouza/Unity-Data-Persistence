using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] private Brick _brickPrefab;
    [SerializeField] private int _lineCount = 6;
    [SerializeField] private Rigidbody _ball;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _highScoreText;
    [SerializeField] private GameObject _gameOverText;

    private bool _started = false;
    private bool _gameOver = false;

    private int _score;

    public void GameOver()
    {
        _gameOver = true;
        _gameOverText.SetActive(true);

        GameManager.Instance.AddScore(_score);
    }

    private void Start()
    {
        _updateHighScore();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] scoreCountValues = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < _lineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                var position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(_brickPrefab, position, Quaternion.identity);
                brick.ScoreValue = scoreCountValues[i];
                brick.onDestroyed.AddListener(_addScore);
            }
        }
    }

    private void Update()
    {
        if (!_started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                var forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                _ball.transform.SetParent(null);
                _ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (_gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneHelper.MainSceneIndex());
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene(SceneHelper.MenuSceneIndex());
            }
        }
    }

    private void _addScore(int score)
    {
        this._score += score;
        _scoreText.text = $"Score : {this._score}";
    }

    private void _updateHighScore()
    {
        var highScores = GameManager.Instance.HighScores;

        var highScore = highScores.Count > 0 ? highScores[0] : null;
        var hasHighScore = highScore != null;

        _highScoreText.gameObject.SetActive(hasHighScore);

        if (hasHighScore)
        {
            _highScoreText.text = $"High Score:{System.Environment.NewLine}" +
                $"{highScore.PlayerName}        {highScore.Score}";
        }

    }
}
