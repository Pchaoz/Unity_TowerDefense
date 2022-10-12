using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Pathfinding))]
public class Spawner : MonoBehaviour
{

    [SerializeField]
    private GameObject m_EnemyGeneral;

    [SerializeField]
    private EnemyInfo[] m_EnemyInfos;

    [SerializeField]
    private GameEventInteger m_EnemiesRemaining;

    [SerializeField]
    private GameEventInteger m_OleadaMasEvent;

    [SerializeField]
    private float m_SpawnRate = 10f;
    [SerializeField]
    Tilemap m_Tilemap;

    Pathfinding m_Pathfinding;
    [SerializeField]
    Transform m_Origin;
    [SerializeField]
    Transform m_Destiny;
    List<Node> m_Nodes;

    private int m_Wave;
    
    [SerializeField]
    private GameEvent m_OnRoundFinished;

    int m_waveSize;
    int m_AliveEnemies;

    //TOTES LES PROBABILITATS DELS ENEMICS
    private int m_freighterRate;
    private int m_normalRate;
    private int m_fastRate;
    private int m_tankRate;
    private int m_flyingRate;



    private void Awake()
    {
        m_Pathfinding = GetComponent<Pathfinding>();
        NextRound(10);
        m_Wave = 1;
    }

    private IEnumerator SpawnEnemies()
    {
        int IncomingEnemies = m_waveSize;
        Vector3Int origen = m_Tilemap.WorldToCell(m_Origin.position);
        Vector3Int destino = m_Tilemap.WorldToCell(m_Destiny.position);
        while (true)
        {
            if (IncomingEnemies > 0)
            {
                GameObject enemigo = Instantiate(m_EnemyGeneral);
                List<Vector3Int> nodes;
                m_Pathfinding.FindPath(origen, destino, out nodes);
                //Nodes to world coordinates
                List<Vector3> path;
                m_Pathfinding.FromCellPathToWorldPath(nodes, out path);
                enemigo.GetComponent<Enemy>().Nodes = path;
                enemigo.GetComponent<Enemy>().LoadInfo(m_EnemyInfos[EnemyToSpawn()]);
                IncomingEnemies--;
            }
            yield return new WaitForSeconds(m_SpawnRate);
        }
    }
    //Funcion de enemigos a spawnear= (oleada*5) +20
    public void NextRound()
    {
        m_Wave++;
        NextRound(m_waveSize + 5);
    }

    private void NextRound(int amount)
    {
        getRates();
        m_waveSize = amount;
        m_AliveEnemies = m_waveSize;
        StartCoroutine(SpawnEnemies());
    }

    //FuncionOleada++ si todos los enemigos spawneados de ronda mueren se sube la oleada.
    //es decir los enemigos son 0 y ya han spawneado todos llamar a nextRound() 
    public void SumarOleada()
    {
        m_OleadaMasEvent.Raise(1);
    }
    public void ReciveEventReciveDead(int param)
    {
        m_AliveEnemies -= param;
        if (m_AliveEnemies == 0)
        {
            NextRound();
            SumarOleada();
        }
    }
    public int EnemyToSpawn()
    {
        int value = Random.Range(0, 101);

        if (value >= m_normalRate && m_freighterRate <= value)
        {
            return 1;
        }else if (value >= m_freighterRate && value <= m_fastRate)
        {
            return 0;
        }else if (value >= m_fastRate && value <= m_tankRate)
        {
            return 2;

        }else if (value >= m_normalRate && value <= m_fastRate)
        {
            return 3;

        }else
        {
            return 4;
        }
    }
    private void getRates()
    {
        if (m_Wave == 1)
        {
            m_normalRate = 100;
            m_normalRate = 0;
            m_fastRate = 0;
            m_tankRate = 0;
            m_flyingRate = 0;

        }
        else if (m_Wave == 3)
        {
            m_normalRate = 50;
            m_normalRate = 70;
            m_fastRate = 90;
            m_tankRate = 100;
            m_flyingRate = 0;

        }
        else if (m_Wave == 6)
        {
            m_normalRate = 30;
            m_normalRate = 50;
            m_fastRate = 70;
            m_tankRate = 90;
            m_flyingRate = 100;

        }
        else if (m_Wave == 8)
        {
            m_normalRate = 20;
            m_normalRate = 40;
            m_fastRate = 60;
            m_tankRate = 80;
            m_flyingRate = 100;
        }
    }

}
