using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    private int _score = 0;
    [SerializeField]
    private Sprite[] _liveSprites;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + _score;
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score:" + playerScore;
    }
}
