using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Data/State Data/LookForPlayer State")]
public class D_LookForPlayerState : ScriptableObject
{
    public int amountOfTurns = 2;
    public float timeBetweenTurns = 0.75f;
}
