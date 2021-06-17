using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.UI.Game.Utils;
public class VisualCard : MonoBehaviour
{
    [SerializeField]
    public CardSignType CardSignType;

    [SerializeField]
    public CardValueType CardValueType;

    public Card BaseCard { get; set; }
}
