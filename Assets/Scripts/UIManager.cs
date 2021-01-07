using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _coinsText;
    void Start()
    {
        _coinsText.text = "Coins: ";
    }
    public void CoinsTextUpdate(int coins)
    {
        _coinsText.text = "Coins: " + coins;
    }
}
