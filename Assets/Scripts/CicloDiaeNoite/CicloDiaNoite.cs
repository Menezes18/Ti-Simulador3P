using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

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
    private string[] nomeEstacoes = { "Primavera", "Verão", "Outono", "Inverno" };
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

            if (diaAtual == 31)
            {
                if(estacaoIndex == 3 && diaAtual == 31)
                {
                    anoAtual++;
                    estacaoIndex = 0;
                    diaAtual = 1;
                    estadoText.text = nomeEstacoes[estacaoIndex];
                   // estacaoAtual = (EstacaoDoAno)estacaoIndex;
                } 
                else 
                {
                estacaoIndex++;
                estadoText.text = nomeEstacoes[estacaoIndex];
               // estacaoAtual = (EstacaoDoAno)estacaoIndex;
                diaAtual = 1;

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
        anoText.text = diaAtual + "/" + anoAtual;
    }
}
