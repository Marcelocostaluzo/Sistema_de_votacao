using SistemaDeVotacao.Models;
using SistemaDeVotacao.Repository;
using SistemaDeVotacao.UI;

namespace SistemaDeVotacao;

public class Program
{
    public static void Main()
    {
        ListaOpcoes<Opcao> listaOpcoes = new ListaOpcoes<Opcao>();
        ListaDeVotantes listaDeVotantes = new ListaDeVotantes();
        Historico historico = new Historico();

        Menu.MenuPrincipal(listaOpcoes, listaDeVotantes, historico);
    }
}
