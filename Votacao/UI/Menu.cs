using SistemaDeVotacao.Helpers;
using SistemaDeVotacao.Models;
using SistemaDeVotacao.Repository;

namespace SistemaDeVotacao.UI;

public class Menu
{
    public static void MenuPrincipal(ListaOpcoes<Opcao> listaOpcoes, ListaDeVotantes listaDeVotantes)
    {
        while (true)
        {
            string[] opcoes = [ "Nova votação", "Iniciar votação", "Ver resultado", "Histórico", "Sair"];

            Helper.Mensagem("=== Menu Principal ===\n", true, false);
            Helper.CriarMenu(opcoes);

            int escolha = Helper.ValidarSaidaInt("\nEscolha uma das opções");

            switch (escolha) {
                case 1: Helper.Mensagem("Nova votação \n[A fazer...]", true, true); break;
                case 2: Helper.Mensagem("Iniciar votação \n[A fazer...]", true, true); break;
                case 3: Helper.Mensagem("Ver resultado \n[A fazer...]", true, true); break;
                case 4: Helper.Mensagem("Histórico \n[A fazer...]", true, true); break;
                case 5: Helper.Mensagem("Saindo...", true, true); return;
                default: Helper.Mensagem("Escolha uma opção válida.", true, true); break;
            }
        }
    }
}
