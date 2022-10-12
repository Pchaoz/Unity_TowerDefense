using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TorretasInfo", menuName = "Scriptables/TorretasInfo")]
public class TorretasInfo : ScriptableObject
{
    public GameObject m_Bala;

    public Color color;
    public float m_Cooldown;
    public int m_costeInicial;
    public int m_costeEvolucion;
    public int m_Valor;
    public int m_Dolor;
    public float m_Alcance;


   
}
