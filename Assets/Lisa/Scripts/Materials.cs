using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : Interactive
{
    public Sprite[] materials;

    public override void Start()
    {
        instructionChild = 2;
        base.Start();
    }
    public void UpdateText(bool mine)
    {
        if (materials.Length > 0)
        {
            instructions.transform.GetChild(materials.Length - 1).gameObject.SetActive(mine);
            if (mine)
            {
                for (int i = 1; i < inventory.renderers[materials.Length - 1].Length; i++)
                {
                    inventory.renderers[materials.Length - 1][i].sprite = materials[i - 1];
                }
            }
            else
            {
                InactivateOthers(false);
            }
        }
        else
        {
            instructions.transform.GetChild(materials.Length).gameObject.SetActive(false);
            InactivateOthers(false);
        }
    }
}
