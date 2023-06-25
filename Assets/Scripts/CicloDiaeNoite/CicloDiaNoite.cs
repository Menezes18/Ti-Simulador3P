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

    public GameObject sol;
    public GameObject lua;
    public GameObject Primavera;
    public GameObject Verao;
    public GameObject Outono;
    public GameObject Inverno;

    void Start()
    {
        Primavera.SetActive(true);
        multiplacador = 86400 / duracaoDoDia;
        diaAtual = 1;
        segundos = 0;
        TimeSpan horarioInicial = TimeSpan.FromHours(6);
        segundos = (float)horarioInicial.TotalSeconds;

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
            if (diaAtual == 30)
            {
                
                diaAtual = 1;
                estacaoAtual = (Estacao)(((int)estacaoAtual + 1) % Enum.GetValues(typeof(Estacao)).Length);
                estadoText.text = estacaoAtual.ToString();
                if(estacaoAtual == Estacao.Primavera)
                {
                    anoAtual++;
                }
                AtualizarEstacao();
            }


        }
        ProcessarCeu();
        CalcularHorario();
        CalcularAno();

        // Verificar se é noite (entre 18:00 e 05:00) ou dia (entre 06:00 e 17:59)
        TimeSpan horarioAtual = TimeSpan.FromSeconds(segundos);
        TimeSpan inicioNoite = TimeSpan.FromHours(18);
        TimeSpan fimNoite = TimeSpan.FromHours(5);
        TimeSpan inicioDia = TimeSpan.FromHours(6);
        TimeSpan fimDia = TimeSpan.FromHours(17).Add(TimeSpan.FromMinutes(59)); // Adiciona 59 minutos ao fim do dia

        if (horarioAtual >= inicioNoite || horarioAtual <= fimNoite)
        {
            lua.SetActive(true);
            sol.SetActive(false);
            //Debug.Log("noite");
        }
        else if (horarioAtual >= inicioDia && horarioAtual <= fimDia)
        {
            lua.SetActive(false);
            sol.SetActive(true);
            //Debug.Log("dia");
        }


    }

    private void ProcessarCeu()
    {
        float rotacaoX = Mathf.Lerp(-90, 270, segundos / 86400);
        luzDirecional.rotation = Quaternion.Euler(rotacaoX, 0, 0);
    }

    public void AtualizarEstacao()
    {
        switch (estacaoAtual)
        {
            case Estacao.Primavera:
            Primavera.SetActive(true);
            Verao.SetActive(false);
            Outono.SetActive(false);
            Inverno.SetActive(false);
            break;
            case Estacao.Verao:
            Primavera.SetActive(false);
            Verao.SetActive(true);
            Outono.SetActive(false);
            Inverno.SetActive(false);
            break;
            case Estacao.Outono:
            Primavera.SetActive(false);
            Verao.SetActive(false);
            Outono.SetActive(true);
            Inverno.SetActive(false);
            break;
            case Estacao.Inverno:
            Primavera.SetActive(false);
            Verao.SetActive(false);
            Outono.SetActive(false);
            Inverno.SetActive(true);
            break;
        }
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
