using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private int score = 0;

    [SerializeField] private TextMeshProUGUI uiText;


    private void Awake()
    {
        Instance = this;
        uiText.text = score.ToString();
    }

    public void UpdateScore()
    {
        score++;
        uiText.text = score.ToString();
    }
}
