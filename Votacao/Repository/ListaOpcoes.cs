using SistemaDeVotacao.Interfaces;

namespace SistemaDeVotacao.Repository;

public class ListaOpcoes <T> where T : IEntidade
{
    private List<T> _opcoes = new List<T>();

    public IReadOnlyList<T> Registro => _opcoes;

    public int NovaChave()
    {
       if (_opcoes.Count == 0) return 1;
        return _opcoes.Max(o => o.Id) + 1;
    }

    public void Adicionar(T opcao) => _opcoes.Add(opcao);

    public T? Buscar(int id) => _opcoes.FirstOrDefault(o => o.Id == id);

    public void Remover(int id) => _opcoes.RemoveAll(o => o.Id == id);

    public void Limpar() => _opcoes.Clear();
}
