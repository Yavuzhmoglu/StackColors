using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static GameController instance;
    [SerializeField] Text scoreDisplay;
   public int score;
    float multiplierValue;
    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void UpdateScore(int valueIn)
    {
        /*
        if (PlayerPrefs.GetInt("Control") == 1)
        {
            score *= valueIn;
        }
        */

        score += valueIn;
        scoreDisplay.text = score.ToString();

        
    }
    public void UpdateMultiplier(float valueIn)
    {
        /*
        if (valueIn<=multiplierValue)
        {
            return;
        }
        */
        multiplierValue = valueIn;
        scoreDisplay.text = (score * multiplierValue).ToString();

    }
}
