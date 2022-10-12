using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIGlobalScript : MonoBehaviour
{
    [SerializeField]
    private ValorMaxMin m_vida;
    [SerializeField]
    private ValorMaxMin m_oro;
    [SerializeField]
    private ValorMaxMin m_oleada;

    // Start is called before the first frame update
    void Start()
    {
        m_vida.ValorActual = m_vida.ValorInicial;
        m_oro.ValorActual = m_oro.ValorInicial;
        m_oleada.ValorActual = m_oleada.ValorInicial; 
    }

    public void ActualizarValorVida()
    {
        GameObject.Find("VidasText").GetComponent<TextMeshProUGUI>().text = m_vida.ValorActual + " / " + m_vida.ValorMax;
    }

    public void ActualizarValorOro()
    {
        GameObject.Find("OroText").GetComponent<TextMeshProUGUI>().text = m_oro.ValorActual+"";
    }

    public void ActualizarValorOleada()
    {
        GameObject.Find("OleadasText").GetComponent<TextMeshProUGUI>().text = m_oleada.ValorActual + " / " + m_oleada.ValorMax;
    }
}
