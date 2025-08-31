using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.Models
{
    public class Carteira : EntityBase
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public decimal Saldo { get; private set; }
        public StatusCarteira Status { get; set; }
        public List<Transacao> Transacoes { get; set; } = new();


        public void RegistrarTransacao(Transacao transacao)
        {
            if (transacao.Tipo == TipoTransacao.Credito)
            {
                Saldo += transacao.Valor;
            }
            else if (transacao.Tipo == TipoTransacao.Debito)
            {
                if (Saldo < transacao.Valor)
                    throw new InvalidOperationException("Saldo insuficiente.");

                Saldo -= transacao.Valor;
            }

            Transacoes.Add(transacao);
        }



    }
}
