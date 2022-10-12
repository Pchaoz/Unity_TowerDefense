using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneral : MonoBehaviour
{
    public int m_Hp;
    public float m_Velocity;
    public int m_Recompensa;
    public int m_Penalizacion;
    public bool m_Volador;
    public bool m_Carguero;





    public void leerValoresEnemigo(EnemyInfo enemyInfo)
    {
        GetComponent<SpriteRenderer>().color = enemyInfo.color;
        m_Hp = enemyInfo.m_HP;
        m_Velocity = enemyInfo.m_Velocity;
        m_Recompensa = enemyInfo.m_Recompensa;
        m_Penalizacion = enemyInfo.m_Penalizacion;
        m_Volador = enemyInfo.volador;
        m_Carguero = enemyInfo.carguero;

    }

    void Update()
    {

    }
}
