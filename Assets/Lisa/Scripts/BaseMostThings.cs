using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMostThings : MonoBehaviour
{
    public ItKnows valueKeeper;
    public virtual void Start()
    {
        valueKeeper = FindObjectOfType<ItKnows>(); // most things need a reference to this
    }
}
