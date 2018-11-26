using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinnerScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_WinGameString;
    [SerializeField]
    private TextMeshProUGUI m_WinSerieString;

    [SerializeField]
    private GameObject m_PlayAgainBut;
    [SerializeField]
    private GameObject m_NewGameBut;
    [SerializeField]
    private GameObject m_ContinueBut;

	private void Start ()
    {
        SetStrings();
	}

    private void SetStrings()
    {
        GameManager gM = GameManager.Instance;
        if (gM.LastGameWinner == "Tie")
        {
            m_WinGameString.text = "It's a draw!";
        }
        else
        {
            m_WinGameString.text = gM.LastGameWinner + " win the game.";
        }

        if(gM.RedScore > gM.BlueScore)
        {
            if (gM.RedScore > gM.MatchesQty * 0.5)
            {
                if (gM.Player1IsRed)
                {
                    m_WinSerieString.text = gM.Player1Name + " win the series " + gM.RedScore + " / " + gM.BlueScore;
                }
                else
                {
                    m_WinSerieString.text = gM.Player2Name + " win the series " + gM.RedScore + " / " + gM.BlueScore;
                }
                gM.RedScore = 0;
                gM.BlueScore = 0;
                SetBut(true);
            }
            else
            {
                if (gM.Player1IsRed)
                {
                    m_WinSerieString.text = gM.Player1Name + " lead the series " + gM.RedScore + " / " + gM.BlueScore;
                }
                else
                {
                    m_WinSerieString.text = gM.Player2Name + " lead the series " + gM.RedScore + " / " + gM.BlueScore;
                }
                SetBut(false);
            }
        }
        else if(gM.BlueScore > gM.RedScore)
        {
            if (gM.BlueScore > gM.MatchesQty * 0.5)
            {
                if (gM.Player1IsRed)
                {
                    m_WinSerieString.text = gM.Player2Name + " win the series " + gM.BlueScore + " / " + gM.RedScore;
                }
                else
                {
                    m_WinSerieString.text = gM.Player1Name + " win the series " + gM.BlueScore + " / " + gM.RedScore;
                }
                gM.RedScore = 0;
                gM.BlueScore = 0;
                SetBut(true);
            }
            else
            {
                if (gM.Player1IsRed)
                {
                    m_WinSerieString.text = gM.Player2Name + " lead the series " + gM.BlueScore + " / " + gM.RedScore;
                }
                else
                {
                    m_WinSerieString.text = gM.Player1Name + " lead the series " + gM.BlueScore + " / " + gM.RedScore;
                }
                SetBut(false);
            }
        }
        else //tie
        {
            m_WinSerieString.text = "The series is even " + gM.RedScore + " / " + gM.BlueScore;
            SetBut(false);
        }
    }

    //Series is end or not
    private void SetBut(bool aIsOver)
    {
        if(aIsOver)
        {
            m_PlayAgainBut.SetActive(true);
            m_NewGameBut.SetActive(true);
            m_ContinueBut.SetActive(false);
        }
        else
        {
            m_PlayAgainBut.SetActive(false);
            m_NewGameBut.SetActive(false);
            m_ContinueBut.SetActive(true);
        }
    }
}
