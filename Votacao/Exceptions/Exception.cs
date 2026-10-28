namespace SistemaDeVotacao.Exceptions;

public class IdInvalidoException : Exception
{
    public IdInvalidoException(string mensagem) : base(mensagem) { }
}

public class NomeNaoPodeSerVazioOuNuloException : Exception
{
    public NomeNaoPodeSerVazioOuNuloException(string mensagem) : base(mensagem) { }
}
