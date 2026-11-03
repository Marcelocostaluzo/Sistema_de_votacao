using SistemaDeVotacao.Exceptions;
using SistemaDeVotacao.Helpers;
using SistemaDeVotacao.Models;
using SistemaDeVotacao.Repository;

namespace SistemaDeVotacao.UI;

public class Menu
{
    public static void MenuPrincipal(ListaOpcoes<Opcao> listaOpcoes, ListaDeVotantes listaDeVotantes)
    {
        string[] opcoes = [ "Nova votação", "Iniciar votação", "Ver resultado", "Histórico", "Sair" ];

        while (true)
        {
            Helper.Mensagem("=== Menu ===\n", true, false);
            Helper.CriarMenu(opcoes);
            int escolha = Helper.ValidarSaidaInt("\nEscolha uma das opções");
            switch (escolha)
            {
                case 1: NovaVotacao(listaOpcoes, listaDeVotantes); break;
                case 2: IniciarVotacao(listaOpcoes, listaDeVotantes); break;
                case 3: VerResultado(listaOpcoes); break;
                case 4: Helper.Mensagem("A fazer...", true, true); break;
                case 5: Helper.Mensagem("Sair", true, false); return;
                default: Helper.Mensagem("Escolha uma opção válida", true, false); break;
            }
        }
    }

    private static void NovaVotacao(ListaOpcoes<Opcao> listaOpcoes, ListaDeVotantes listaDeVotantes)
    {
        Helper.Mensagem("=== Nova votação ===\n", true, false);
        int quantOpcoes = Helper.ValidarSaidaInt("Quantas opções de voto");
        while (quantOpcoes > listaOpcoes.Registro.Count)
        {
            try
            {
                Helper.Mensagem($"=== {listaOpcoes.Registro.Count + 1}° opção ===\n", true, false);
                string nome = Helper.ValidarSaidaString("Digite o nome");
                Opcao opcao = new Opcao(listaOpcoes.NovaChave(), nome);
                listaOpcoes.Adicionar(opcao);

            } catch (IdInvalidoException ex)
            {
                Helper.Mensagem($"{ex.Message}", true, true);

            } catch (NomeNaoPodeSerVazioOuNuloException ex)
            {
                Helper.Mensagem($"{ex.Message}", true, true);

            }
        }

        Helper.Mensagem("=== Nova votação ===\n", true, false);
        int quantVotantes = Helper.ValidarSaidaInt("Quantos votantes");
        while (quantVotantes > listaDeVotantes.Registro.Count)
        {
            try
            {
                Helper.Mensagem($"=== {listaDeVotantes.Registro.Count + 1}° votante ===\n", true, false);
                string matricula = Helper.ValidarSaidaMatricula("Digite a matrícula");
                string nome = Helper.ValidarSaidaString("Digite o nome");
                Votante votante = new Votante(matricula: matricula, nome: nome);
                listaDeVotantes.Adicionar(votante);

            } catch (MatriculaNaoPodeSerVazioOuNuloException ex)
            {
                Helper.Mensagem($"{ex.Message}", true, true);
                
            } catch (NomeNaoPodeSerVazioOuNuloException ex)
            {
                Helper.Mensagem($"{ex.Message}", true, true);

            } catch (MatriculaJaRegistradaException ex)
            {
                Helper.Mensagem($"{ex.Message}", true, true);

            }
        }
    }

    private static void IniciarVotacao(ListaOpcoes<Opcao> listaOpcoes, ListaDeVotantes listaDeVotantes)
    {
        if (listaOpcoes.Registro.Count == 0)
        {
            Helper.Mensagem("A lista de opções está vazia.", true, true);
            return;   
        }

        int contVotos = 0;
        while (contVotos < listaDeVotantes.Registro.Count)
        {
            Helper.Mensagem($"=== {contVotos + 1}° voto ===\n", true, false);
            string matricula = Helper.ValidarSaidaMatricula("Digite sua matrícula");

            var votante = listaDeVotantes.Buscar(matricula);
            if (votante != null)
            {
                try
                {
                    Voto(listaOpcoes, votante);
                    Helper.Mensagem("Voto confirmado.", true, true);
                    contVotos ++;

                } catch (VotanteJaVotouException ex)
                {
                    Helper.Mensagem($"{ex.Message}", true, true);

                }
            }
            else 
                Helper.Mensagem("Matrícula não encontrada.", true, true);
        }

        Helper.Mensagem("Votação finalizada.", true, true);
    }

    private static void Voto(ListaOpcoes<Opcao> listaOpcoes, Votante votante)
    {
        string[] opcoes = [.. listaOpcoes.Registro.Select(o => o.Nome)];

        while (true)
        {
            Helper.Mensagem($"=== Votante {votante.Nome} ===\n", true, false);
            Helper.CriarMenu(opcoes);
            int escolha = Helper.ValidarSaidaInt("\nEscolha uma das opção");
            
            var opcao = listaOpcoes.Buscar(escolha);

            if (opcao != null)
            {
                votante.Votou();
                opcao.NovoVoto();
                break;

            } else
            {
                Helper.Mensagem("Escolha uma opção válida", true, true);
            }
        }
    }

    private static void VerResultado(ListaOpcoes<Opcao> listaOpcoes)
    {
        Helper.Mensagem("=== Resultado ===\n", true, false);

        if (listaOpcoes.Registro.Count == 0)
        {
            Helper.Mensagem("A lista de opções está vazia.", false, true);
            return;
        }

        if (listaOpcoes.Registro.Max(o => o.QuantidadeVotos) == 0)
        {
            Helper.Mensagem("A votação ainda não foi iniciada.", false, true);
            return;
        }

        Console.WriteLine($"{"ID", -2} | {"Nome", -22} | {"Quantidade de votos", -22} \n---------------------------------------------------------------------------------");
        foreach (var opcao in listaOpcoes.Registro)
            Console.WriteLine($"{opcao.Id, -2} | {opcao.Nome, -22} | {opcao.QuantidadeVotos, -22}");
        Helper.Mensagem("", false, true);
    }
}
