using SistemaDeVotacao.Models;
using SistemaDeVotacao.Repository;

namespace SistemaDeVotacao.Service;

public class VotacaoService
{
    public static void LimparVotacaoAtiva(ListaOpcoes<Opcao> listaOpcoes, ListaDeVotantes listaDeVotantes)
    {
        listaOpcoes.Limpar();
        listaDeVotantes.Limpar();
    }

    public static void CriaOpcao(ListaOpcoes<Opcao> listaOpcoes, string nome)
    {
        Opcao opcao = new Opcao(listaOpcoes.NovaChave(), nome);
        listaOpcoes.Adicionar(opcao);
    }

    public static void  CriaVotante(ListaDeVotantes listaDeVotantes, string nome, string matricula)
    {
        Votante votante = new Votante(nome, matricula);
        listaDeVotantes.Adicionar(votante);
    }
}
