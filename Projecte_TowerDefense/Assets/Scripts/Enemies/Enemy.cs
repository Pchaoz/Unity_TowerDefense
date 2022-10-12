using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //ESTADISTICAS BASICAS ENEMIGO
    public int m_hp;
    public float m_speed;
    public int m_reward;
    public int m_damage;
    public bool volador;
    public bool carguero;
    public Color color;

   

    //Rigidbody del enemic
    private Rigidbody2D m_rb;

    //NODE COUNT
    int m_nodeSel;

    
    
    //LISTA NODOS PARA MOVERSE
    List<Vector3> m_Nodes;

    [SerializeField]
    private GameEventInteger m_EnemyMuerteRewardEvent;

    [SerializeField]
    private GameEventInteger m_EnemyDiesEvent;


    public List<Vector3> Nodes
    {
        get
        {
            return m_Nodes;
        }
        set
        {
            m_Nodes = value;
        }
    }

    private void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        transform.position = m_Nodes[m_nodeSel];
        NextNode(m_nodeSel + 1);
    }

    void FixedUpdate()
    {
        // print("Node actual " + m_Nodes.Count);
        m_rb.MovePosition(transform.position + ((m_Nodes[m_nodeSel] - transform.position).normalized * m_speed * Time.fixedDeltaTime));

        if (Vector3.Distance(transform.position, m_Nodes[m_nodeSel]) < .05f)
            NextNode(m_nodeSel + 1);
    }

    private void NextNode(int v)
    {
        m_nodeSel++;

        if (!(m_nodeSel < m_Nodes.Count))
        {
            //Destroy(gameObject);
        }

    }
    public void LoadInfo(EnemyInfo inf)
    {
        m_hp = inf.m_HP;
        m_reward = inf.m_Recompensa;
        m_speed = inf.m_Velocity;
        m_damage = inf.m_Penalizacion;
        carguero = inf.carguero;
        volador = inf.volador;
        GetComponent<SpriteRenderer>().color = inf.color;

    }
    public int getDmg ()
    {
        return m_damage;
    }  

    //Falta crear funcion de recibir daño de la bala
    public void RecibirDano(int dano)
    {
        this.m_hp -= dano;
        if(this.m_hp<=0)
        {
            int reward = this.m_reward;
            Destroy(this.gameObject);
            this.m_EnemyMuerteRewardEvent.Raise(reward);
            this.m_EnemyDiesEvent.Raise(1);
        }
    }   
}

