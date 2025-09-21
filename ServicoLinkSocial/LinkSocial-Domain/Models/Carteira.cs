using LinkSocial_Domain.Enum;

namespace LinkSocial_Domain.Models
{
    public class Carteira : EntityBase
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public StatusCarteira Status { get; set; }
        public List<Transacao> Transacoes { get; set; } = new();


        public decimal SaldoAprovado => Transacoes
            .Where(t => t.Status == StatusPagamento.Aprovado && t.Tipo == TipoTransacao.Credito)
            .Sum(t => t.Valor)
          - Transacoes
            .Where(t => t.Status == StatusPagamento.Aprovado && t.Tipo == TipoTransacao.Debito)
            .Sum(t => t.Valor);


        public decimal SaldoPendente => Transacoes
            .Where(t => t.Status == StatusPagamento.Pendente && t.Tipo == TipoTransacao.Credito)
            .Sum(t => t.Valor)
          - Transacoes
            .Where(t => t.Status == StatusPagamento.Pendente && t.Tipo == TipoTransacao.Debito)
            .Sum(t => t.Valor);


        public decimal SaldoTotal => SaldoAprovado + SaldoPendente;


        public void RegistrarTransacao(Transacao transacao)
        {
            Transacoes.Add(transacao);
        }

        public void AprovarTransacao(Transacao transacao)
        {
            if (transacao.Status != StatusPagamento.Aprovado)
                throw new InvalidOperationException("Só é possível aprovar transações pendentes.");


            transacao.Status = StatusPagamento.Aprovado;
        }

        public void ExpirarTransacoesPendentes()
        {
            var agora = DateTime.UtcNow;

            foreach (var transacao in Transacoes.Where(t => t.Status == StatusPagamento.Pendente))
            {
                if ((transacao.Criado_em - agora).TotalMinutes >= 30)
                {
                    transacao.Status = StatusPagamento.Cancelado;
                }
            }
        }
        public void RejeitarTransacao(Transacao transacao)
        {
            transacao.Status = StatusPagamento.Rejeitado;

        }
    }
}
