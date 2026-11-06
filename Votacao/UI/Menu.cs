using SistemaDeVotacao.Exceptions;
using SistemaDeVotacao.Helpers;
using SistemaDeVotacao.Models;
using SistemaDeVotacao.Repository;

namespace SistemaDeVotacao.UI;

public class Menu
{
    public static void MenuPrincipal(ListaOpcoes<Opcao> listaOpcoes, ListaDeVotantes listaDeVotantes, Historico historico)
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
                case 2: IniciarVotacao(listaOpcoes, listaDeVotantes, historico); break;
                case 3: VerResultado(listaOpcoes); break;
                case 4: Historico(historico); break;
                case 5: Helper.Mensagem("Sair", true, false); return;
                default: Helper.Mensagem("Escolha uma opção válida", true, false); break;
            }
        }
    }

    private static void NovaVotacao(ListaOpcoes<Opcao> listaOpcoes, ListaDeVotantes listaDeVotantes)
    {
        listaOpcoes.Limpar();
        listaDeVotantes.Limpar();

        Helper.Mensagem("=== Nova votação ===\n", true, false);
        int quantOpcoes = Helper.ValidarSaidaInt("Quantas opções de voto");
        while (quantOpcoes > listaOpcoes.Registro.Count)
        {
            try
            {
                Helper.Mensagem($"=== {listaOpcoes.Registro.Count + 1}ª opção ===\n", true, false);
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

    private static void IniciarVotacao(ListaOpcoes<Opcao> listaOpcoes, ListaDeVotantes listaDeVotantes, Historico historico)
    {
        if (listaOpcoes.Registro.Count == 0)
        {
            Helper.Mensagem("A lista de opções está vazia.", true, true);
            return;   
        }

        if (listaDeVotantes.Registro.Count == 0)
        {
            Helper.Mensagem("A lista de votantes está vazia.", true, true);
            return;
        }

        if (listaDeVotantes.Registro.All(v => v.JaVotou == true))
        {
            Helper.Mensagem("Todos os votantes já votaram", true, true);
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

        var ganhador = listaOpcoes.Registro.OrderByDescending(o => o.QuantidadeVotos).First();

        RegistroVotacao registro = new RegistroVotacao(listaOpcoes.Registro.Count, listaDeVotantes.Registro.Count, ganhador.Nome, ganhador.QuantidadeVotos, DateTime.Now);
        historico.Adicionar(registro);

        Helper.Mensagem("Votação finalizada.", true, true);
    }

    private static void Voto(ListaOpcoes<Opcao> listaOpcoes, Votante votante)
    {
        string[] opcoes = [.. listaOpcoes.Registro.Select(o => o.Nome)];

        while (true)
        {
            Helper.Mensagem($"=== Votante {votante.Nome} ===\n", true, false);
            Helper.CriarMenu(opcoes);
            int escolha = Helper.ValidarSaidaInt("\nEscolha uma das opções");
            
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

        Console.WriteLine($"{"ID", -2} | {"Nome", -22} | {"Quantidade de votos", -22} \n-----------------------------------------------------------------------------------------");

        if (listaOpcoes.Registro.Count == 0)
        {
            Helper.Mensagem("\nA lista de opções está vazia", false, true);
            return;
        }

        if (listaOpcoes.Registro.All(o => o.QuantidadeVotos == 0))
        {
            Helper.Mensagem("\nA votação ainda não foi iniciada.", false, true);
            return;
        }

        foreach (var opcao in listaOpcoes.Registro.OrderByDescending(o => o.QuantidadeVotos))
            Console.WriteLine($"{opcao.Id, -2} | {opcao.Nome, -22} | {opcao.QuantidadeVotos, -22}");
        Helper.Mensagem("", false, true);
    }

    private static void Historico(Historico historico)
    {
        Helper.Mensagem("=== Histórico ===\n", true, false);

        Console.WriteLine($"{"Total de opções", -16} | {"Total de votantes", -16} | {"Ganhador", -22} | {"Total de votos", -16} | {"Data", -14}\n----------------------------------------------------------------------------------------------------------------------------------------------------------------");
        if (historico.Registro.Count == 0)
        {
            Helper.Mensagem("\nHistórico está vazio.", false, true);
            return;
        }

        foreach (var registro in historico.Registro)
            Console.WriteLine($"{registro.TotalOpcoes, -16} | {registro.TotalVotantes, -16} | {registro.Ganhador, -22} | {registro.TotalVotosGanhador, -16} | {registro.DataVotacao.ToString("dd/MM/yyyy"), -14}");
        Helper.Mensagem("", false, true);
    }
}
