using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TorretaGeneral : MonoBehaviour
{
    [SerializeField]
    private GameObject m_bala;
    public CircleCollider2D m_AreaTorreta;
    public float m_Cooldown;
    public int m_costeInicial;
    public int m_costeEvolucion;
    public int m_Valor;
    public int m_Dolor;
    public float m_Alcance;
    public List<GameObject> m_targets = new List<GameObject>();
    public bool m_targetInRange;

    public void leerValoresTorreta(TorretasInfo towerinfo)
    {
        GetComponent<SpriteRenderer>().color = towerinfo.color;
        m_Cooldown = towerinfo.m_Cooldown;
        m_costeInicial = towerinfo.m_costeInicial;
        m_costeEvolucion = towerinfo.m_costeEvolucion;
        m_Valor = towerinfo.m_Valor;
        m_Dolor = towerinfo.m_Dolor;
        m_Alcance = towerinfo.m_Alcance;
        m_AreaTorreta = this.GetComponent<CircleCollider2D>();
        m_AreaTorreta.GetComponent<CircleCollider2D>().radius = m_Alcance;

    }
    private void Start()
    {
        StartCoroutine(Disparar());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            m_targets.Add(collision.gameObject);
            collision.gameObject.name = ""+m_targets.IndexOf(collision.gameObject);
            m_targetInRange = true;
        }
        print(m_targets.Count + " ha colisionat un enemic nou.");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int m_targetExits = int.Parse(collision.gameObject.name);
        m_targets.RemoveAt(m_targetExits);
        if(m_targets.Count == 0)
        {
            m_targetInRange = false;
        }
    }

    private IEnumerator Disparar()
    {
        while (true)
        {

            if (m_targets.Count > 0)
            {
                GameObject primer_enemic = m_targets[0];
                Enemy primer_enemic_Enemy = primer_enemic.GetComponent<Enemy>();
                Color color_enemic = primer_enemic_Enemy.color;
                Vector3 posicio_enemicActual = primer_enemic.transform.position;
                primer_enemic_Enemy.RecibirDano(m_Dolor);
                print("pium");
                //StartCoroutine(Danyar(primer_enemic_Enemy));
                //primer_enemic.GetComponent<SpriteRenderer>().color = color_enemic;

            }
            yield return new WaitForSeconds(this.m_Cooldown);

        }
    }

    private IEnumerator Danyar(Enemy enemic)
    {
        enemic.color = Color.red;
        yield return new WaitForSeconds(0.5f);
    }


}

