using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyInfo", menuName = "Scriptables/EnemyInfo")]

public class EnemyInfo : ScriptableObject
{
    public int m_HP;
    public float m_Velocity;
    public int m_Recompensa;
    public int m_Penalizacion;
    public bool volador;
    public bool carguero;
    public Color color;

}

