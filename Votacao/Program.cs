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

        Menu.MenuPrincipal(listaOpcoes, listaDeVotantes);
    }
}
