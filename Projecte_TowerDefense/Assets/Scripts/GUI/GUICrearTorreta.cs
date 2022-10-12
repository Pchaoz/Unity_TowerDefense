using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUICrearTorreta : MonoBehaviour
{
    [SerializeField]
    private TorretasInfo m_InfoTorreta;
    [SerializeField]
    private GameEventTorretaInfo m_Evento;
    [Tooltip("Mostrar el valor como [Actual/Maximo] o solo [Actual]")]
    //[SerializeField]
    //private bool m_MostrarMax = true;

    private void Awake()
    {
        //TODO poner la info de la torreta en el sprite y el coste y cosas
        //GetComponent<Image>().sprite = m_InfoTorreta.sprite;
    }

    public void Raise()
    {
        m_Evento.Raise(m_InfoTorreta);
    }
}
