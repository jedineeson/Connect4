using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_P1Information;
    [SerializeField]
    private TextMeshProUGUI m_P2Information;
    [SerializeField]
    private TextMeshProUGUI m_SeriesInformation;
    [SerializeField]
    private GameObject m_P1Red;
    [SerializeField]
    private GameObject m_P2Red;
    [SerializeField]
    private GameObject m_P1Blue;
    [SerializeField]
    private GameObject m_P2Blue;
    [SerializeField]
    private TMP_InputField m_Player2InputField;

    private GameManager m_GM = GameManager.Instance;

    private string[] m_CpuNames = { "Elton John", "Hulk Hogan", "Bugs Bunny", "Mike Tyson", "Lionel Messi", "Buzz Lighter", "Michel Louvain" };

    private void Start()
    {
        ResetAll();
        m_P1Information.text = m_GM.Player1Name;
        m_P2Information.text = m_GM.Player2Name;
        SetSeriesDuration(m_GM.MatchesQty);
        SelectColor(m_GM.Player1IsRed);
        if (m_GM.SinglePlayer)
        {
            m_Player2InputField.interactable = false;
        }
    }

    private void ResetAll()
    {
        m_GM.Player1Name = "Player 1";
        if (m_GM.SinglePlayer)
        {
            int random = Random.Range(0, m_CpuNames.Length);
            m_GM.Player2Name = m_CpuNames[random];
        }
        else
        {
            m_GM.Player2Name = "Player 2";
        }
        m_GM.Player1IsRed = true;
        m_GM.MatchesQty = 1;
    }

    public void ChangePlayer1Name(string aName)
    {
        if (aName == "" || aName == null)
        {
            aName = "Player 1";
        }
        m_GM.Player1Name = aName;
        m_P1Information.text = aName;
    }

    public void ChangePlayer2Name(string aName)
    {
        if(aName == "" || aName == null)
        {
            aName = "Player 2";
        }
        m_GM.Player2Name = aName;
        m_P2Information.text = aName;
    }

    public void SelectColor(bool aIsRed)
    {
        m_GM.Player1IsRed = aIsRed;
        m_P1Red.SetActive(aIsRed);
        m_P2Blue.SetActive(aIsRed);
        m_P2Red.SetActive(!aIsRed);
        m_P1Blue.SetActive(!aIsRed);
    }

    public void SetSeriesDuration(int aDuration)
    {
        m_GM.MatchesQty = aDuration;
        m_SeriesInformation.text = (int)(aDuration * 0.5 + 1) + " of " + aDuration;
    }
}
