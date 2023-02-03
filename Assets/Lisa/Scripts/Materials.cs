using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : Interactive
{
    public Sprite[] materials;

    public void UpdateText(bool mine)
    {
        instructions.transform.GetChild(materials.Length - 1).gameObject.SetActive(mine);
        if (mine)
        {
            for (int i = 1; i < inventory.renderers[materials.Length - 1].Length; i++)
            {
                inventory.renderers[materials.Length - 1][i].sprite = materials[i - 1];
            }
        }
    }
}
