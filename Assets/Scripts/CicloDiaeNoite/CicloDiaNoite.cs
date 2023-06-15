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
    [SerializeField] [Tooltip("Duração do dia em segundos")] public int duracaoDoDia;
    [SerializeField] private TextMeshProUGUI horarioText;
    [SerializeField] private TextMeshProUGUI estadoText;
    [SerializeField] private TextMeshProUGUI anoText;
    //[SerializeField] public EstacaoDoAno estacaoAtual;

    public float segundos;
    public float multiplacador;
    public float soma = 86400f;

    public int diaAtual = 1;
    
    public Estacao estacaoAtual = Estacao.Primavera;
    private int anoAtual = 1850;
   // public int estacaoIndex = 0;

    public bool pode2 = false;
    public int diaTest = 0;
    

    void Start()
    {
        multiplacador = 86400 / duracaoDoDia;
        diaAtual = 1;
    }

    void Update()
    {
        segundos += Time.deltaTime * multiplacador;
       
        if (segundos >= soma)
        {
            
            segundos = 0;
            diaAtual++;
            if(pode2)
            {
                diaTest++;
            }            
            if (diaAtual == 8)
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
        anoText.text = "DIA " + " \n" + diaAtual;
    }
}
