using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [Header("Sprites")]
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    [Header("Player Stats")]
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;


    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }


    public void UpdateHearts()
    {
        InitHearts();
        float tempHealth = playerCurrentHealth.RuntimeValue / 2;
        for (int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            if (i <= tempHealth - 1)
            {
                // full heart
                hearts[i].sprite = fullHeart;
            }
            else if (i >= tempHealth)
            {
                // empty heart
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                // half full heart
                hearts[i].sprite = halfHeart;
            }
        }
    }
}
