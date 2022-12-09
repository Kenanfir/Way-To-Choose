using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MahjongTile : MonoBehaviour
{
    public MahjongTile[] AdTiles;
    GameManager GM;
    public string TileName;
    public Material TileMat;
    public bool Matched;
    public bool Select;
    public bool Active;
    public int Top;

    private void Awake()
    {
        TileMat = GetComponent<SpriteRenderer>().material;
        Select = false;
        GM = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        ActiveState();
        Selected();
    }
    private void Selected()
    {
        if (Select)
        {
            TileMat.color = Color.yellow;
        }
        else if (Active == true && Select == false)
        {
            TileMat.color = Color.white;
        }
    }
    void ActiveState()
    {
        if (Active == false)
        {
            TileMat.color = Color.grey;
        }
        else
        {
            TileMat.color = Color.white;
        }
    }
    public void TileActivate()
    {
        for(int i = 0; i < AdTiles.Length; i++)
        {
            if (AdTiles[i].Top == 1)
            {
                AdTiles[i].Top = 0;
            }
            else if (AdTiles[i].Active == false && AdTiles[i].Top == 0)
            {
                AdTiles[i].Active = true;
            }
        }
    }
    public void OnMouseDown()
    {
        if (Active && Select == true && GM.tileA == TileName)
        {
            GM.tileA = "";
        }
        else if (Active && GM.tileA.Length <= 0 && Select == false)
        {
            GM.tileA = TileName;
        }
        else if (Active && GM.tileA.Length > 0 && Select == false)
        {
            GM.tileB = TileName;
        }
        if (Active == true)
        {
            Select = !Select;
        }
    }
    public void isMatched(bool state)
    {
        Matched = state;
    }
    public void OnMatch(bool isMatched)
    {
        TileActivate();

        if (isMatched == true && Active == true && TileName == GM.tileA || TileName == GM.tileB)
        {
            gameObject.SetActive(false);
        }
    }
}
