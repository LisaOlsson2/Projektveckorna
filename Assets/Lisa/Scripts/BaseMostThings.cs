using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMostThings : MonoBehaviour
{
    public ItKnows reference;

    public virtual void Start()
    {
        reference = FindObjectOfType<ItKnows>();
    }
}
