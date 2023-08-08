using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Infra.Orm.Compartilhado;

namespace LocadoraDeVeiculos.Infra.Orm.ModuloFuncionario
{
    public class RepositorioFuncionarioEmOrm : RepositorioBaseEmOrm<Funcionario>, IRepositorioFuncionario
    {
        public RepositorioFuncionarioEmOrm(LocadoraDeVeiculosDbContext dbContext) : base(dbContext)
        {
        }

        public Funcionario SelecionarPorNome(string nome)
        {
            return registros.SingleOrDefault(x => x.nome == nome);
        }
    }
}
