 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eliminattor : MonoBehaviour
{
    [SerializeField]
    private GameEventInteger m_vidaEvent;

    [SerializeField]
    private ValorMaxMin m_Vida;

    [SerializeField]
    private GameEventInteger m_EnemyReachEnd;



    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            int pupita = (col.GetComponent<Enemy>().getDmg()) * -1;
            Destroy(col.gameObject);
            m_vidaEvent.Raise(pupita);
            m_EnemyReachEnd.Raise(1);
        }
    }
}

