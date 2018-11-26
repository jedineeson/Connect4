using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private ColourState m_MyColour;
    private GameManager m_GM = GameManager.Instance;

    private void Start()
    {
        if (m_GM.Player1IsRed)
        {
            m_MyColour = ColourState.Blue;
        }
        else
        {
            m_MyColour = ColourState.Red;
        }
    }

    public GameObject TravelBoard()
    {
        List<List<GameObject>> grid = m_GM.Grid;

        //Potential return value
        GameObject slotToPlay = null;

        int myCount = 0;
        int playersCount = 0;
     
        for (int z = 3; z >= 0; z--)
        {
            if(z == 0)
            {
                //If im first, I play mid
                return grid[3][0].gameObject;
            }

            //Check matches in Column
            for (int i = 0; i < grid.Count; i++)
            {
                myCount = 0;
                playersCount = 0;
                for (int j = 0; j < grid[i].Count; j++)
                {
                    if (grid[i][j].GetComponent<Slot>().StateOfSlot == m_MyColour)
                    {
                        myCount += 1;
                        playersCount = 0;
                    }
                    else if ((grid[i][j].GetComponent<Slot>().StateOfSlot != m_MyColour &&
                        grid[i][j].GetComponent<Slot>().StateOfSlot != ColourState.Empty))
                    {
                        myCount = 0;
                        playersCount += 1;
                    }
                    else //if slot is empty
                    {
                        if (myCount == z)
                        {                           
                            return grid[i][j].gameObject;
                        }
                        else if (playersCount == z)
                        {
                            slotToPlay = grid[i][j].gameObject;
                        }                       
                        myCount = 0;
                        playersCount = 0;
                    }
                }
            }

            //Check matches in Row, right to left
            for (int i = 0; i < grid[0].Count; i++)
            {
                myCount = 0;
                playersCount = 0;
                for (int j = grid.Count-1; j >= 0; j--)
                {
                    if (grid[j][i].GetComponent<Slot>().StateOfSlot == m_MyColour)
                    {
                        myCount += 1;
                        playersCount = 0;
                    }
                    else if ((grid[j][i].GetComponent<Slot>().StateOfSlot != m_MyColour &&
                        grid[j][i].GetComponent<Slot>().StateOfSlot != ColourState.Empty))
                    {
                        myCount = 0;
                        playersCount += 1;
                    }
                    else //if slot is empty
                    {
                        if (myCount == z)
                        {
                            if(i - 1 < 0 || grid[j][i - 1].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                            {
                                return grid[j][i].gameObject;
                            }
                        }
                        else if (playersCount == z)
                        {
                            if (i - 1 < 0 || grid[j][i - 1].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                            {
                                slotToPlay = grid[j][i].gameObject;
                            }
                        }
                        myCount = 0;
                        playersCount = 0;
                    }
                }
            }

            //Check matches in Row, left to right
            for (int i = 0; i < grid[0].Count; i++)
            {
                myCount = 0;
                playersCount = 0;
                for (int j = 0; j < grid.Count; j++)
                {
                    if (grid[j][i].GetComponent<Slot>().StateOfSlot == m_MyColour)
                    {
                        myCount += 1;
                        playersCount = 0;
                    }
                    else if ((grid[j][i].GetComponent<Slot>().StateOfSlot != m_MyColour &&
                        grid[j][i].GetComponent<Slot>().StateOfSlot != ColourState.Empty))
                    {
                        myCount = 0;
                        playersCount += 1;
                    }
                    else //if slot is empty
                    {
                        if (myCount == z)
                        {
                            if (i - 1 < 0 || grid[j][i - 1].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                            {
                                return grid[j][i].gameObject;
                            }
                        }
                        else if (playersCount == z)
                        {
                            if (i - 1 < 0 || grid[j][i - 1].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                            {
                                slotToPlay = grid[j][i].gameObject;
                            }
                        }
                        myCount = 0;
                        playersCount = 0;
                    }                    
                }
            }


            //Check Diagonals from SW to NE
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[0].Count - 1; j++)
                {
                    myCount = 0;
                    playersCount = 0;
                    for (int k = 0; k < grid[0].Count; k++)
                    {
                        if (i + k < grid.Count - 1 && j + k < grid[0].Count - 1)
                        {
                            if (grid[i + k][j + k].GetComponent<Slot>().StateOfSlot == m_MyColour)
                            {
                                myCount += 1;
                                playersCount = 0;
                            }
                            else if (grid[i + k][j + k].GetComponent<Slot>().StateOfSlot != m_MyColour &&
                                grid[i + k][j + k].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                            {
                                myCount = 0;
                                playersCount += 1;
                            }
                            else //if empty
                            {
                                if (myCount == z)
                                {
                                    if (j + k - 1 < 0 || grid[i + k][j + k - 1].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                                    {
                                        return grid[i + k][j + k].gameObject;
                                    }
                                }
                                else if (playersCount == z)
                                {
                                    if (j + k - 1 < 0 || grid[i + k][j + k - 1].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                                    {
                                        slotToPlay = grid[i + k][j + k].gameObject; ;
                                    }
                                }
                                myCount = 0;
                                playersCount = 0;
                            }
                        }
                        else
                        {
                            myCount = 0;
                            playersCount = 0;
                            continue;
                        }
                    }
                }
            }

            //Check Diagonals from NW to SE
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[0].Count; j++)
                {
                    myCount = 0;
                    playersCount = 0;
                    for (int k = 0; k < grid[0].Count; k++)
                    {
                        if (i + k < grid.Count - 1 && j - k > 0)
                        {
                            if (grid[i + k][j - k].GetComponent<Slot>().StateOfSlot == m_MyColour)
                            {
                                myCount += 1;
                                playersCount = 0;
                            }
                            else if (grid[i + k][j - k].GetComponent<Slot>().StateOfSlot != m_MyColour &&
                                grid[i + k][j - k].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                            {
                                myCount = 0;
                                playersCount += 1;
                            }
                            else // if empty
                            {
                                if (myCount == z)
                                {
                                    if (j - k - 1 < 0 || grid[i + k][j - k - 1].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                                    {
                                        return grid[i + k][j - k].gameObject;
                                    }
                                }
                                else if (playersCount == z)
                                {
                                    if (j - k - 1 < 0 || grid[i + k][j - k - 1].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                                    {
                                        slotToPlay = grid[i + k][j - k].gameObject; ;
                                    }
                                }
                                myCount = 0;
                                playersCount = 0;
                            }
                        }
                        else
                        {
                            myCount = 0;
                            playersCount = 0;
                            continue;
                        }
                    }
                }
            }

            //Check Diagonals from SE to NW
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[0].Count; j++)
                {
                    myCount = 0;
                    playersCount = 0;
                    for (int k = 1; k < grid[0].Count; k++)
                    {
                        if (i - k >= 0 && j + k < grid[0].Count - 1)
                        {
                            if (grid[i - k][j + k].GetComponent<Slot>().StateOfSlot == m_MyColour)
                            {
                                myCount += 1;
                                playersCount = 0;
                            }
                            else if (grid[i - k][j + k].GetComponent<Slot>().StateOfSlot != m_MyColour &&
                                grid[i - k][j + k].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                            {
                                myCount = 0;
                                playersCount += 1;
                            }
                            else // if empty
                            {
                                if (myCount == z)
                                {
                                    if (j + k - 1 < 0 || grid[i - k][j + k - 1].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                                    {
                                        return grid[i - k][j + k].gameObject;
                                    }
                                }
                                else if (playersCount == z)
                                {
                                    if (j + k - 1 < 0 || grid[i - k][j + k - 1].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                                    {
                                        slotToPlay = grid[i - k][j + k].gameObject; ;
                                    }
                                }
                                myCount = 0;
                                playersCount = 0;
                            }
                        }
                        else
                        {
                            myCount = 0;
                            playersCount = 0;
                            continue;
                        }
                    }
                }
            }

            //Check Diagonals from NE to SW
            for (int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[0].Count; j++)
                {
                    myCount = 0;
                    playersCount = 0;
                    for (int k = 0; k < grid[0].Count; k++)
                    {
                        if (i - k >= 0 && j - k >= 0)
                        {
                            if (grid[i - k][j - k].GetComponent<Slot>().StateOfSlot == m_MyColour)
                            {
                                myCount += 1;
                                playersCount = 0;
                            }
                            else if (grid[i - k][j - k].GetComponent<Slot>().StateOfSlot != m_MyColour &&
                                grid[i - k][j - k].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                            {
                                myCount = 0;
                                playersCount += 1;
                            }
                            else //if empty
                            {
                                if (myCount == z)
                                {
                                    if (j - k - 1 < 0 || grid[i - k][j - k - 1].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                                    {
                                        return grid[i - k][j - k].gameObject;
                                    }
                                }
                                else if (playersCount == z)
                                {
                                    if (j - k - 1 < 0 || grid[i - k][j - k - 1].GetComponent<Slot>().StateOfSlot != ColourState.Empty)
                                    {
                                        slotToPlay = grid[i - k][j - k].gameObject; ;
                                    }
                                }
                                myCount = 0;
                                playersCount = 0;
                            }
                        }
                        else
                        {
                            myCount = 0;
                            playersCount = 0;
                            continue;
                        }
                    }
                }
            }
            
            if (slotToPlay != null)
            {
                return slotToPlay;
            }
        }

        //Not supposed to be hit
        return null;
    }
}
