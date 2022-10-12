using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    private bool m_UIisActive;
    //private bool m_TowerMenuIsActive;
    private bool m_TowerToBuildIsSelected;

    [SerializeField]
    Tilemap m_tilemap;
    [SerializeField]
    private ValorMaxMin m_Vida;
    [SerializeField]
    private GameEventInteger m_vidaEvent;
    [SerializeField]
    private ValorMaxMin m_Dinero;
    [SerializeField]
    private GameEventInteger m_DineroEvent;
    [SerializeField]
    private ValorMaxMin m_Oleadas;
    [SerializeField]
    private GameEventInteger m_OleadasEvent;
    [SerializeField]
    private GameObject m_botoTorretaClasica;
    [SerializeField]
    private GameObject m_botoTorretaArea;
    [SerializeField]
    private GameObject m_botoTorretaMachinegun;
    [SerializeField]
    private GameObject m_botoTorretaSniper;

    [Tooltip("Prefab de la torreta que se mostrara en el mouse en el modo constuir")]
    [SerializeField]
    private GameObject m_torretaEditable;
    private GameObject m_torretaSeleccionada;

    [SerializeField]
    GameObject m_instanceTorreta;
    [SerializeField]
    GameObject m_PanelConstruccion;

    private TorretasInfo m_TorretaInfoAConstruir;

    [SerializeField]
    private Pool m_Pool;

    // Start is called before the first frame update
    void Start()
    {
        m_UIisActive = true;
        //m_TowerMenuIsActive = false;
        m_TowerToBuildIsSelected = false;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
        IniciarValores();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
            IniciarValores();
    }


    private void IniciarValores()
    {
        m_Vida.ValorActual = m_Vida.ValorMax;
        m_Dinero.ValorActual = m_Dinero.ValorMax;
        m_Oleadas.ValorActual = m_Oleadas.ValorInicial;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            //UIOpen(); ??? 
            // Si el menú de UI no está activo - Despliega el menú de construcción de torres (Activa UIisActive)
            // Si el menú de UI está activo - Despliega el menú de construcción de torres (Desactiva UIisActive)      
            if (m_UIisActive == true)
            {
                m_PanelConstruccion.gameObject.SetActive(false);
                m_UIisActive = false;
            } else
            {
                m_PanelConstruccion.gameObject.SetActive(true);
                m_UIisActive = true;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            //Click izquierdo
            // Si el menú de UI está activo - Seleccionar y Construir (Si UIisActive = true)
            // Si el menú de UI no está activo - Seleccionar una torre para activar menú de torre (Si UIisActive = false entonces activa TowerMenuIsActive)
            // Si el menú de torre está activo - Seleccionar mejora o borrar torre
            if (m_TowerToBuildIsSelected)
            {
                Build(m_TorretaInfoAConstruir);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            //Click derecho
            // Si has seleccionado una torre a construir y le das click derecho, la desselecciona y te devuelve el dinero
            if (m_TowerToBuildIsSelected)
            {
                Destroy(m_TorretaInfoAConstruir);
                Destroy(m_torretaSeleccionada);
                m_TowerToBuildIsSelected = false;
            }
        }

        if (m_TowerToBuildIsSelected)
        {
            Vector3 positionPreview = m_tilemap.GetCellCenterWorld(m_tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
            positionPreview.z = 0;
            m_torretaSeleccionada.transform.position = positionPreview;        
        }
    }

    private void UIOpen()
    {

    }

    private void UIClose()
    {

    }

    private void UpgradeUIOpen()
    {

    }

    private void UpgradeUIClose()
    {

    }

    public void SelectTowerToBuild()
    {
        //Selección de la torre en la UI para comprarla
        m_TowerToBuildIsSelected = true;        
        
    }

    public void ActivarModoEdicion(TorretasInfo torretaInfo)
    {
        if(!m_TowerToBuildIsSelected)
        {
            m_TowerToBuildIsSelected = true;
            m_TorretaInfoAConstruir = torretaInfo;
            m_torretaSeleccionada = Instantiate(m_torretaEditable);
            m_torretaSeleccionada.GetComponent<SpriteRenderer>().color = torretaInfo.color;
            //TODO que la torreta se vea como la torreta que hemos seleccionado
        }
        
    }
        

    private void SelectTowerOnTilemap()
    {
        //Selección de una torre ya construida para derribar o mejorar
    }

    public void GetValorsTorreta()
    {

    }

    private void Build(TorretasInfo info)
    {
        Vector3 posicio = m_tilemap.GetCellCenterWorld(m_tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)));

        if (BuildingInValidPosition(posicio) && info.m_costeInicial <= m_Dinero.ValorActual)
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int spawnPosCell = m_tilemap.WorldToCell(spawnPosition);
            Destroy(m_torretaSeleccionada);
            spawnPosition.z = 0;
            
            GameObject torreta = m_Pool.GetElement();

            if (torreta)
            {
                torreta.GetComponent<TorretaGeneral>().leerValoresTorreta(info);
                torreta.transform.position = m_tilemap.GetCellCenterWorld(spawnPosCell);
            }
            m_TorretaInfoAConstruir = null;
            m_TowerToBuildIsSelected = false;
            ModificaDiner(info.m_costeInicial);


        }
    }



    private void Demolish()
    {
        //Si haces click en el botón para destruir la torre se destruye la torre seleccionada
    }

    private void Upgrade()
    {
        //Si haces click en el botón de mejora se mejora la torreta
    }

    private bool BuildingInValidPosition(Vector3 spawnPosition)
    {
        GameObject[] objectesCela = GameObject.FindGameObjectsWithTag("torreta");
        foreach(GameObject torreta in objectesCela)
        {
            if(torreta.transform.position == spawnPosition && m_tilemap)
            {
                return false;
            }
        }
        return true;
    }

    public void ModificaDiner(int valor)
    {
            m_Dinero.ValorActual -= valor;
            m_DineroEvent.Raise(m_Dinero.ValorActual);
    }

    public void ReciveEventIntegerVida(int param0)
    {
        m_Vida.ValorActual += param0;
        if (m_Vida.ValorActual < 0)
        {
            m_Vida.ValorActual = 0;
            //emigramos a nueva scene derrota
            SceneManager.LoadScene("EndGameScene");
        }
        print(m_Vida.ValorActual + " eso me ha dolido");
    }

    public void ReciveEventRecibirRecompensa(int param0)
    {
        m_Dinero.ValorActual += param0;
        m_DineroEvent.Raise(m_Dinero.ValorActual);
        print(m_Dinero.ValorActual + " soy rico!");
        
    }

    public void ReciveEventRecibirNuevaOleada(int param0)
    {
        m_Oleadas.ValorActual += param0;
        print(m_Oleadas.ValorActual + " vienen mas enemigos!");

        if (m_Oleadas.ValorActual == m_Oleadas.ValorMax)
        {
            //emigramos a nueva scene victoria
            SceneManager.LoadScene("EndGameScene");


        }
    }
}
