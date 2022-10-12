using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MostrarValorPorGUI : MonoBehaviour
{
    [SerializeField]
    private ValorMaxMin m_Valor;
    [SerializeField]
    private TextMeshProUGUI m_Text;
    [Tooltip("Mostrar el valor como [Actual/Maximo] o solo [Actual]")]
    [SerializeField]
    private bool m_MostrarMax = true;

    private void Start()
    {
        ActualizarValor();
    }

    public void ActualizarValor()
    {
        m_Text.text = m_Valor.ValorActual + (m_MostrarMax ? " / " + m_Valor.ValorMax : ""); ;
    }
}
