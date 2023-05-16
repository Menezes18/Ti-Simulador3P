using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public enum Estacao
{
    Primavera,
    Verao,
    Outono,
    Inverno
}
public class CicloDiaNoite : MonoBehaviour
{
    [SerializeField] private Transform luzDirecional;
    [SerializeField] [Tooltip("Duração do dia em segundos")] private int duracaoDoDia;
    [SerializeField] private TextMeshProUGUI horarioText;
    [SerializeField] private TextMeshProUGUI estadoText;
    [SerializeField] private TextMeshProUGUI anoText;
    //[SerializeField] public EstacaoDoAno estacaoAtual;

    private float segundos;
    public float multiplacador;

    private int diaAtual;
   // private int diaPorEstacao = 30;
    public Estacao estacaoAtual = Estacao.Primavera;
    private int anoAtual = 1850;
    public int estacaoIndex = 0;

    void Start()
    {
        multiplacador = 86400 / duracaoDoDia;
    }

    void Update()
    {
        segundos += Time.deltaTime * multiplacador;
        if (segundos >= 86400)
        {
            
            segundos = 0;
            diaAtual++;

            if (diaAtual == 5)
            {
                diaAtual = 1;
                estacaoAtual = (Estacao)(((int)estacaoAtual + 1) % Enum.GetValues(typeof(Estacao)).Length);
                estadoText.text = estacaoAtual.ToString();
                if(estacaoAtual == Estacao.Primavera)
                {
                    anoAtual++;
                }
            }


        }
        ProcessarCeu();
        CalcularHorario();
        CalcularAno();
    }





    private void ProcessarCeu()
    {
        float rotacaoX = Mathf.Lerp(-90, 270, segundos / 86400);
        luzDirecional.rotation = Quaternion.Euler(rotacaoX, 0, 0);
    }

    private void CalcularHorario()
    {
        horarioText.text = TimeSpan.FromSeconds(segundos).ToString(@"hh\:mm");
    }

    private void CalcularAno()
    {
        anoText.text = diaAtual + "/" + anoAtual.ToString();
    }
}
