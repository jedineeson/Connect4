using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int m_MatchesQty = 1;

    private string m_Player1Name = "Player 1";
    private string m_Player2Name = "Player 2";
    private string m_LastGameWinner = "Player";

    private bool m_Player1IsRed = true;
    private bool m_SinglePlayer = true;

    private int m_RedScore = 0;
    private int m_BlueScore = 0;

    private List<List<GameObject>> m_Grid = new List<List<GameObject>>();

    private GameUI m_UI;

    #region Getter/Setter
    public int MatchesQty
    {
        get { return m_MatchesQty; }
        set { m_MatchesQty = value; }
    }
    public string Player1Name
    {
        get { return m_Player1Name; }
        set { m_Player1Name = value; }
    }
    public string Player2Name
    {
        get { return m_Player2Name; }
        set { m_Player2Name = value; }
    }
    public string LastGameWinner
    {
        get { return m_LastGameWinner; }
        set { m_LastGameWinner = value; }
    }
    public bool Player1IsRed
    {
        get { return m_Player1IsRed; }
        set { m_Player1IsRed = value; }
    }
    public bool SinglePlayer
    {
        get { return m_SinglePlayer; }
        set { m_SinglePlayer = value; }
    } 
    public int RedScore
    {
        get { return m_RedScore; }
        set { m_RedScore = value; }
    }
    public int BlueScore
    {
        get { return m_BlueScore; }
        set { m_BlueScore = value; }
    }
    public List<List<GameObject>> Grid
    {
        get { return m_Grid; }
        set { m_Grid = value; }
    }
    public GameUI UI
    {
        get { return m_UI; }
        set { m_UI = value; }
    }
    #endregion

    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get { return m_Instance; }
    }

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}
