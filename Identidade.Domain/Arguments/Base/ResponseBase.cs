namespace Identidade.Domain.Arguments.Base
{
    public class ResponseBase<T>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public List<string> Erros { get; set; } = new List<string>();
        public List<T> Resultado { get; set; } = new List<T>();
    }
}
