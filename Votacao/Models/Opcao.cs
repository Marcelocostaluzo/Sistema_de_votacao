using SistemaDeVotacao.Exceptions;
using SistemaDeVotacao.Interfaces;

namespace SistemaDeVotacao.Models;

public class Opcao : IEntidade
{
    public int Id { get; private set; }
    public string Nome { get; private set; }
    public int QuantidadeVotos { get; private set; }

    public Opcao(int id, string? nome)
    {
        if (id <= 0) throw new IdInvalidoException("Id inválido.");
        if (string.IsNullOrWhiteSpace(nome)) throw new NomeNaoPodeSerVazioOuNuloException("Nome não pode ser vazio ou nulo.");

        Id = id;
        Nome = nome;
        QuantidadeVotos = 0;
    }

    public void NovoVoto()
    {
        QuantidadeVotos += 1;
    }
}
