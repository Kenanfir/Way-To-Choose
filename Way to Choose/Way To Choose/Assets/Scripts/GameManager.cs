using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject WinUI;
    
    public GameObject[] Tiles;
    public bool[] TilesMatched;
    public string tileA;
    public string tileB;
    public float timer;
    public int CountDownTime;
    public Text CountDownDisplay;
    public Text CountDownDisplay2;
    public GameObject CountDownUI;
    MahjongTile MJ;

    private void Start()
    {
        StartCoroutine(StartCount());
    }
    IEnumerator StartCount()
    {
        while (CountDownTime > 0)
        {
            CountDownDisplay.text = CountDownTime.ToString();
            yield return new WaitForSeconds(1f);
            CountDownTime--;
        }
        CountDownDisplay.gameObject.SetActive(false);
        CountDownDisplay2.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        CountDownUI.gameObject.SetActive(false);
    }
    private void Awake()
    {
        MJ = FindObjectOfType<MahjongTile>();
    }
    private void Update()
    {
        OnMatch();
        TileReset();
        WinCheck();
        timer += Time.deltaTime;
    }
    void Matches(int pos)
    {
        if (TilesMatched[pos] == true)
        {
            return;
        }
        else
        {
            TilesMatched[pos] = true;
        }
        Tiles[pos].GetComponent<MahjongTile>().isMatched(TilesMatched[pos]);
    }
    void TileReset()
    {
        for(int i = 0; i < Tiles.Length; i++)
        {
            if (tileA.Length > 0 && tileB.Length > 0 && timer >= 2)
            {
                tileA = "";
                tileB = "";
                timer = 0;

                for (int k = 0; k <= Tiles.Length; k++)
                {
                    if (Tiles[k].GetComponent<MahjongTile>().Select == true)
                    {
                        Tiles[k].GetComponent<MahjongTile>().Select = false;
                        k = 0;
                    }
                }
            }
        }
    }
    void OnMatch()
    {
        for(int i = 0; i < Tiles.Length; i++)
        {
            if (tileA.Length > 0 && tileB.Length > 0)
            {
                if (tileA != tileB)
                {
                    tileA = "";
                    tileB = "";

                    for (int k = 0; k <= Tiles.Length; k++)
                    {
                        if (Tiles[k].GetComponent<MahjongTile>().Select == true)
                        {
                            Tiles[k].GetComponent<MahjongTile>().Select = false;
                            k = 0;
                        }
                    }
                }
                else if (tileA == tileB)
                {
                    if (Tiles[i].GetComponent<MahjongTile>().TileName==tileA|| Tiles[i].GetComponent<MahjongTile>().TileName == tileB)
                    {
                        Tiles[i].GetComponent<MahjongTile>().OnMatch(true);
                        Tiles[i].GetComponent<MahjongTile>().isMatched(true);
                        Matches(i);
                    }
                }
            }
        }
    }
    bool WinCheck()
    {
        for(int i = 0; i < TilesMatched.Length; i++)
        {
            if (TilesMatched[i] == false)
            {
                return false;
            }
        }
        WinUI.SetActive(true);
        Debug.Log("You Win");
        return true;
    }
}
