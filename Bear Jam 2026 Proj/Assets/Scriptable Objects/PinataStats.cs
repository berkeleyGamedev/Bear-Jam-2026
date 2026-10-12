using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PinataStats", menuName = "Scriptable Objects/PinataStats")]
public class PinataStats : ScriptableObject
{
    public float health;
    public float scoreOnDeath;
    public float deathDelay;
    [Header("Scale")]
    public float startScale;
    public float scaleIncPerHit;
    public float scaleMax;
    [Header("Knockback")]
    public float startKBMult;
    public float KBMultIncPerHit;
    public float KBMax;
    [Header("Sprites")]
    public List<Sprite> sprites;

}
