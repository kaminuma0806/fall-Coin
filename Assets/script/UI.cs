using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI PossessionCoinText; 

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = GManager.instance.Score.ToString();
        PossessionCoinText.text = GManager.instance.Possession.ToString();
    }
}
