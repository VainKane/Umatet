using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CharacterDataBase : ScriptableObject
{
    [SerializeField] internal List<Sprite> icons = new();
}
