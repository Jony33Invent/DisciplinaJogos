using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GiveBuffTemp : MonoBehaviour
{
    public Character CharRef;

    [Button]
    public void GiveBuff()
    {
        CharRef.AddBuff(new FlatBuff(10, 25));
    }

}