using SistemaDeVotacao.Exceptions;
using SistemaDeVotacao.Helpers;
using SistemaDeVotacao.Models;
using SistemaDeVotacao.Repository;
using SistemaDeVotacao.Service;

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
                case 1: NovaVotacao(listaOpcoes, listaDeVotantes); break;
                case 2: Helper.Mensagem("Iniciar votação \n[A fazer...]", true, true); break;
                case 3: Helper.Mensagem("Ver resultado \n[A fazer...]", true, true); break;
                case 4: Helper.Mensagem("Histórico \n[A fazer...]", true, true); break;
                case 5: Helper.Mensagem("Saindo...", true, true); return;
                default: Helper.Mensagem("Escolha uma opção válida.", true, true); break;
            }
        }
    }

    private static void NovaVotacao(ListaOpcoes<Opcao> listaOpcoes, ListaDeVotantes listaDeVotantes)
    {
        bool existeVotacaoAtiva = listaOpcoes.Registro.Count != 0 && listaDeVotantes.Registro.Count != 0;

        if (existeVotacaoAtiva)
        {
            if (!ConfirmarCriarNova()) return;
            VotacaoService.LimparVotacaoAtiva(listaOpcoes, listaDeVotantes);
        }

        NovasOpcoes(listaOpcoes);
        NovosVotantes(listaDeVotantes);

        Helper.Mensagem("Nova votação criada com sucesso.", true, true);
    }

    private static bool ConfirmarCriarNova()
    {
        string[] opcoes = ["Criar nova", "Sair"];
        while (true)
        {
            Helper.Mensagem("Existe uma votação ativa.\n", true, false);
            Helper.CriarMenu(opcoes);
            switch (Helper.ValidarSaidaInt("\nEscolha uma das opções"))
            {
                case 1: return true;
                case 2: return false;
                default: Helper.Mensagem("Escolha uma opção válida.", true, true); break;
            }
        }
    }

    private static void NovasOpcoes(ListaOpcoes<Opcao> listaOpcoes)
    {
        while (true)
        {
            Helper.Mensagem("=== Nova votação ===\n", true, false);
            int quantOpcoes = Helper.ValidarSaidaInt("Digite a quantidade de opções");

            if (quantOpcoes < 2)
            {
                Helper.Mensagem("A quantidade de opções deve ser maior ou igual a 2.", true, true);
                continue;
            }

            while (listaOpcoes.Registro.Count < quantOpcoes)
            {
                try
                {
                    Helper.Mensagem($"=== {listaOpcoes.Registro.Count + 1}ª opção ===\n", true, false);
                    string nome = Helper.ValidarSaidaString("Digite o nome");

                    VotacaoService.CriaOpcao(listaOpcoes, nome);

                } catch (NomeNaoPodeSerVazioOuNuloException ex)
                {
                    Helper.Mensagem($"\n{ex.Message}", true, true);

                }
            }

            Helper.Mensagem("Opções adicionadas com sucesso.", true, true);
            return;
        }
    }

    private static void NovosVotantes(ListaDeVotantes listaDeVotantes)
    {
        while (true)
        {
            Helper.Mensagem("=== Nova votação ===\n", true, false);
            int quantVotantes = Helper.ValidarSaidaInt("Digite a quantidade de votantes");

            if (quantVotantes < 2)
            {
                Helper.Mensagem("A quantidade de votantes deve ser maior ou igual a 2.", true, true);
                continue;
            }

            while (listaDeVotantes.Registro.Count < quantVotantes)
            {
                try
                {
                    Helper.Mensagem($"=== {listaDeVotantes.Registro.Count + 1}° votante ===\n", true, false);
                    string nome = Helper.ValidarSaidaString("Digite o nome");
                    string matricula = Helper.ValidarSaidaMatricula("\nDigite a matrícula");

                    VotacaoService.CriaVotante(listaDeVotantes, nome, matricula);

                } catch (NomeNaoPodeSerVazioOuNuloException ex)
                {
                    Helper.Mensagem($"\n{ex.Message}", true, true);

                } catch (MatriculaNaoPodeSerVazioOuNuloException ex)
                {
                    Helper.Mensagem($"\n{ex.Message}", true, true);

                } catch (MatriculaJaRegistradaException ex)
                {
                    Helper.Mensagem($"\n{ex.Message}", true, true);

                }
            }

            Helper.Mensagem("Votantes adicionados com sucesso.", true, true);
            return;
        }
    }
}
