using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCountOnLVL : MonoBehaviour
{
    public static Action BonusOnLVL;
    private void Awake()
    {
        BonusOnLVL?.Invoke();
    }
}
