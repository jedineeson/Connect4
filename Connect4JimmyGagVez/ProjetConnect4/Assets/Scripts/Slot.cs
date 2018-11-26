using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColourState
{
    Empty,
    Red,
    Blue
}

public class Slot : MonoBehaviour
{
    private int m_Row;
    private int m_Col;

    private bool m_IsEmpty = true;

    [SerializeField]
    private GameObject m_EmptySlot;
    [SerializeField]
    private GameObject m_BlueSlot;
    [SerializeField]
    private GameObject m_RedSlot;

    private ColourState m_StateOfSlot = ColourState.Empty;

    public int Row
    {
        get { return m_Row; }
        set { m_Row = value; }
    }
    public int Col
    {
        get { return m_Col; }
        set { m_Col = value; }
    }

    public bool IsEmpty
    {
        get { return m_IsEmpty; }
    }

    public ColourState StateOfSlot
    {
        get { return m_StateOfSlot; }
        set { m_StateOfSlot = value; }
    }

    public bool UpdateSlot()
    {
        switch (m_StateOfSlot)
        {
            case ColourState.Empty:
                m_EmptySlot.SetActive(true);
                m_BlueSlot.SetActive(false);
                m_RedSlot.SetActive(false);
                return false;
            case ColourState.Red:
                m_EmptySlot.SetActive(false);
                m_BlueSlot.SetActive(false);
                m_RedSlot.SetActive(true);
                m_IsEmpty = false;
                return true;
            case ColourState.Blue:
                m_EmptySlot.SetActive(false);
                m_BlueSlot.SetActive(true);
                m_RedSlot.SetActive(false);
                m_IsEmpty = false;
                return true;
            default:
                return false;
        }
    }

    public bool CheckIfAvailable()
    {
        if(m_IsEmpty && 
            (m_Row == 0 ||
            m_IsEmpty && !GameManager.Instance.Grid[m_Col][m_Row - 1].GetComponent<Slot>().IsEmpty))
        {
            return true;
        }

        return false;
    }
}
