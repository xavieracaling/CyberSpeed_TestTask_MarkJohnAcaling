using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI ScoreText_Matches ;
    public TextMeshProUGUI ScoreText_Turns ;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //ScoreText = GetComponent<Text>();
    }

    public void UpdateTurns(int score)
    {
       ScoreText_Turns.text =  score.ToString();
    }
    public void UpdateMatches(int score)
    {
       ScoreText_Matches.text =  score.ToString();
    }
}
