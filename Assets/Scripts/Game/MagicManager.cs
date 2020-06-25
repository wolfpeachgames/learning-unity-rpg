using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicManager : MonoBehaviour
{
    public Slider magicSlider;
    public Inventory playerInventory;


    void Start()
    {
        magicSlider.maxValue = playerInventory.maxMagic;
        magicSlider.value = playerInventory.maxMagic;
        playerInventory.currentMagic = playerInventory.maxMagic;
    }


    void Update()
    {
        
    }


    public void UpdateMagicLevel()
    {
        float level = playerInventory.currentMagic;
        if (level > magicSlider.maxValue)
        {
            level = magicSlider.maxValue;
        }
        else if (level < 0)
        {
            level = 0;
        }
        magicSlider.value = level;
    }
}
