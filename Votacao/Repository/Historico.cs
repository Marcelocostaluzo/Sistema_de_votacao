using SistemaDeVotacao.Models;

namespace SistemaDeVotacao.Repository;

public class Historico
{
    private readonly List<RegistroVotacao> _registro = [];

    public IReadOnlyList<RegistroVotacao> Registro => _registro;

    public void Adicionar(RegistroVotacao r) => _registro.Add(r);

    public void Limpar() => _registro.Clear();
}
