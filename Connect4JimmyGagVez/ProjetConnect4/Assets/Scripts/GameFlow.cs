using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFlow : MonoBehaviour
{
    [SerializeField]
    private float m_CPUPlayDelay = 1f;
    [SerializeField]
    private float m_DropSpeed = 0.25f;

    [SerializeField]
    private AI m_CPU;

    [SerializeField]
    private AudioClip m_DescendingSFX;
    [SerializeField]
    private AudioClip m_InPlaceSFX;

    private bool m_IsRedTurn = true;
    private bool m_CanControl = true;

    private GameManager m_GM = GameManager.Instance;

    private void Start()
    {
        if (m_GM.RedScore + m_GM.BlueScore == 0 ||
            (m_GM.RedScore + m_GM.BlueScore) % 2 == 0)
        {
            m_IsRedTurn = true;
            if(m_GM.SinglePlayer && !m_GM.Player1IsRed)
            {
                StartCoroutine(CPUPlay());
            }
        }
        else
        {
            m_IsRedTurn = false;
            if (m_GM.SinglePlayer && m_GM.Player1IsRed)
            {
                StartCoroutine(CPUPlay());
            }
        }
        m_GM.UI.UpdateUI(m_IsRedTurn);
    }

    void Update()
    {
        if (!m_GM.SinglePlayer || (m_GM.Player1IsRed && m_IsRedTurn) || (!m_GM.Player1IsRed && !m_IsRedTurn) )
        {
            if (m_CanControl && Input.GetKeyDown(KeyCode.Mouse0))
            {
                OnClick();
            }
        }
    }

    public void EndTurn()
    {
        m_IsRedTurn = !m_IsRedTurn;
        if (CheckWin(ColourState.Red))
        {
            m_CanControl = false;
            StartCoroutine(EndGame(ColourState.Red));
        }
        else if(CheckWin(ColourState.Blue))
        {
            m_CanControl = false;
            StartCoroutine(EndGame(ColourState.Blue));
        }
        else
        {           
            m_GM.UI.UpdateUI(m_IsRedTurn);
            if (m_GM.SinglePlayer && ((m_IsRedTurn && !m_GM.Player1IsRed) || (!m_IsRedTurn && m_GM.Player1IsRed)))
            {
                StartCoroutine(CPUPlay());
            }
        }
    }

    private void OnClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            if (hit.transform.tag == "Slot")
            {
                ChangeStateOfSlot(hit.transform.gameObject.GetComponent<Slot>().Col);
            }
        }
    }

    private void ChangeStateOfSlot(int aCol)
    {
        GameObject slot;

        for (int i = 0; i < m_GM.Grid[0].Count; i++)
        {
            if(m_GM.Grid[aCol][i].GetComponent<Slot>().StateOfSlot == ColourState.Empty)
            {
                slot = m_GM.Grid[aCol][i];

                if (m_IsRedTurn)
                {
                    StartCoroutine(DropPiece(slot.GetComponent<Slot>(), ColourState.Red));
                }
                else
                {
                    StartCoroutine(DropPiece(slot.GetComponent<Slot>(), ColourState.Blue));
                }

                break;
            }
        }
    }

    private bool CheckWin(ColourState aSlotState)
    {
        List<List<GameObject>> grid = GameManager.Instance.Grid; 
        int checkCount = 0;

        //CheckColumn
        for (int i = 0; i < grid.Count; i++)
        {
            checkCount = 0;
            for (int j = 0; j < grid[i].Count; j++)
            {
                if(grid[i][j].GetComponent<Slot>().StateOfSlot == aSlotState)
                {
                    checkCount += 1;
                    if(checkCount == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    checkCount = 0;
                }
            }         
        }

        //CheckRow
        for (int i = 0; i < grid[i].Count; i++)
        {
            checkCount = 0;
            for (int j = 0; j < grid.Count; j++)
            {
                if (grid[j][i].GetComponent<Slot>().StateOfSlot == aSlotState)
                {
                    checkCount += 1;
                    if (checkCount == 4)
                    {
                        return true;
                    }
                }
                else
                {
                    checkCount = 0;
                }
            }
        }

        //CheckRightDiagonals
        for (int i = 0; i < grid.Count; i++)
        {
            checkCount = 0;
            for (int j = 0; j < grid[i].Count - 1; j++)
            {
                checkCount = 0;
                if (grid[i][j].GetComponent<Slot>().StateOfSlot == aSlotState)
                {
                    checkCount += 1;
                    for(int k = 1; k < grid[i].Count; k++)
                    {
                        if (i + k < grid.Count && j + k < grid[i].Count)
                        {
                            if (grid[i+k][j+k].GetComponent<Slot>().StateOfSlot == aSlotState)
                            {
                                checkCount += 1;
                                if(checkCount == 4)
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }

        //CheckLeftDiagonals
        for (int i = 0; i < grid.Count; i++)
        {
            checkCount = 0;
            for (int j = 0; j < grid[i].Count - 1; j++)
            {
                checkCount = 0;
                if (grid[i][j].GetComponent<Slot>().StateOfSlot == aSlotState)
                {
                    checkCount += 1;
                    for (int k = 1; k < grid[i].Count; k++)
                    {
                        if (i - k >= 0 && j + k < grid[i].Count)
                        {
                            if (grid[i - k][j + k].GetComponent<Slot>().StateOfSlot == aSlotState)
                            {
                                checkCount += 1;
                                if (checkCount == 4)
                                {
                                    return true;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }

        return false;
    }

    private IEnumerator DropPiece(Slot aSlot, ColourState aColour)
    {
        Slot previousSlot = null;
        Slot actualSlot = null;
        int row = m_GM.Grid[0].Count - 1;
        int col = aSlot.Col;
        while(actualSlot != aSlot)
        {
            previousSlot = actualSlot;
            actualSlot = m_GM.Grid[col][row].GetComponent<Slot>();
            if(previousSlot != null)
            {
                previousSlot.StateOfSlot = ColourState.Empty;
                previousSlot.UpdateSlot();
            }

            actualSlot.StateOfSlot = aColour;
            actualSlot.UpdateSlot();
            row -= 1;
            if(actualSlot == aSlot)
            {
                EndTurn();
                AudioManager.Instance.PlaySFX(m_InPlaceSFX, aSlot.gameObject.transform.position);
            }
            else
            {
                AudioManager.Instance.PlaySFX(m_DescendingSFX, aSlot.gameObject.transform.position);
            }
            yield return new WaitForSeconds(m_DropSpeed);
        }
    }

    private IEnumerator CPUPlay()
    {
        yield return new WaitForSeconds(m_CPUPlayDelay);
        GameObject slot = m_CPU.TravelBoard();
        ChangeStateOfSlot(slot.GetComponent<Slot>().Col);
    }


    private IEnumerator EndGame(ColourState aColour)
    {
        yield return new WaitForSeconds(2f);

        if (aColour == ColourState.Red)
        {
            m_GM.RedScore += 1;

            if (m_GM.Player1IsRed)
            {
                m_GM.LastGameWinner = m_GM.Player1Name;
            }
            else
            {
                m_GM.LastGameWinner = m_GM.Player2Name;
            }
        }
        else if (aColour == ColourState.Blue)
        {
            m_GM.BlueScore += 1;

            if (m_GM.Player1IsRed)
            {
                m_GM.LastGameWinner = m_GM.Player2Name;
            }
            else
            {
                m_GM.LastGameWinner = m_GM.Player1Name;
            }
        }

        LevelManager.Instance.ChangeLevel("WinnerScreen");
    }
}
