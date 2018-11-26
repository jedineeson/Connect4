using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_BlueScore;
    [SerializeField]
    private TextMeshProUGUI m_RedScore;
    [SerializeField]
    private TextMeshProUGUI m_WhosTurn;

    private GameManager m_GM = GameManager.Instance;

    private void Awake()
    {
        m_GM.UI = this;
    }

    private void Start ()
    {
        if (m_GM.Player1IsRed)
        {
            m_RedScore.text = m_GM.Player1Name + " : " + m_GM.RedScore;
            m_BlueScore.text = m_GM.Player2Name + " : " + m_GM.BlueScore;
        }
        else
        {
            m_BlueScore.text = m_GM.Player1Name + " : " + m_GM.BlueScore;
            m_RedScore.text = m_GM.Player2Name + " : " + m_GM.RedScore;
        }
    }

    public void UpdateUI(bool IsRedTurn)
    {
        if(IsRedTurn)
        {
            if (m_GM.Player1IsRed)
            {
                m_WhosTurn.text = m_GM.Player1Name + " it's your turn !";
            }
            else
            {
                m_WhosTurn.text = m_GM.Player2Name + " it's your turn !";
            }
        }
        if (!IsRedTurn)
        {
            if (!m_GM.Player1IsRed)
            {
                m_WhosTurn.text = m_GM.Player1Name + " it's your turn !";
            }
            else
            {
                m_WhosTurn.text = m_GM.Player2Name + " it's your turn !";
            }
        }
    }
}
