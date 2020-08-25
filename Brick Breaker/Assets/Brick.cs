using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int points;
    public int hitsNo;
    public Sprite hitSprite;

    public void BreakBrick()
    {
        hitsNo--;
        GetComponent<SpriteRenderer>().sprite = hitSprite;
    }
}
