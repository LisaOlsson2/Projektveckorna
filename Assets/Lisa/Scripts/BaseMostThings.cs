using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMostThings : MonoBehaviour
{
    public ItKnows valueKeeper;
    public AudioController audioController;
    public virtual void Start()
    {
        valueKeeper = FindObjectOfType<ItKnows>();
        audioController = valueKeeper.audioController;
    }
}
