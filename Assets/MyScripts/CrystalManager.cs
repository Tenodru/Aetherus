using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalManager : MonoBehaviour
{
    #region Singleton

    public static CrystalManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject crystal;
}
