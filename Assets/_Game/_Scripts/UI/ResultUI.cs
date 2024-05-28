using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private PlayResutBase _enemy;
    [SerializeField] private PlayResutBase _boss;

    public PlayResutBase Enemy => _enemy;
    public PlayResutBase Boss => _boss;
}
