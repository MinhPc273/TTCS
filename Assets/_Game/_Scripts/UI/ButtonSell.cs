using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonSell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtCoin;
    public int Coin {
        set {
            txtCoin.text = value.ToString();
        }
        get {
            return int.Parse(txtCoin.text);
        }
    }

    public void Sell() {
        Prefs.Coin += Coin;
    }
}
