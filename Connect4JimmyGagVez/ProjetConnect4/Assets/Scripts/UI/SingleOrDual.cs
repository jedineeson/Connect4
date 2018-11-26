using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleOrDual : MonoBehaviour
{
    public void IsSingle(bool aIsSingle)
    {
        GameManager.Instance.SinglePlayer = aIsSingle;
        LevelManager.Instance.ChangeLevel("SetUpMenu");
    }
}
