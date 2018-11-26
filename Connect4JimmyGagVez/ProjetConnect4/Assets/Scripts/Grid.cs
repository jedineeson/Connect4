using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Slot;

    private int m_ColCount = 7;
    private int m_RowCount = 6;

    private List<List<GameObject>> m_Slots = new List<List<GameObject>>();

    public List<List<GameObject>> Slots
    {
        get { return m_Slots; }
    }

    private void Start()
    {
        SetGrid();
    }

    private void SetGrid()
    {
        float posX = SetPos(m_ColCount);
        float posY = SetPos(m_RowCount);

        Vector3 pos = new Vector3(posX, posY, 0f);

        for (int i = 0; i < m_ColCount; i++)
        {         
            m_Slots.Add(new List<GameObject>());

            for(int j = 0; j < m_RowCount; j++)
            {
                GameObject slot = Instantiate(m_Slot, pos, Quaternion.identity, transform);

                slot.GetComponent<Slot>().Row = j;
                slot.GetComponent<Slot>().Col = i;

                m_Slots[i].Add(slot);
                pos.y += 1;               
            }

            pos.y = posY;
            pos.x += 1;
        }

        GameManager.Instance.Grid = m_Slots;
    }

    private float SetPos(float aCount)
    {
        float pos = aCount * 0.5f;

        pos -= 0.5f;

        return -pos;
    }
}
