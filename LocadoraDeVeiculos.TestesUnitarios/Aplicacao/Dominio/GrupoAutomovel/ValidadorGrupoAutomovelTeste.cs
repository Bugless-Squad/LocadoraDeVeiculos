using FluentValidation.TestHelper;
using LocadoraDeVeiculos.Dominio.ModuloGrupoAutomovel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocadoraDeVeiculos.TestesUnitarios.Dominio.ModuloGrupoAutomovel
{
    [TestClass]
    public class GrupoAutomovelTest
    {
        private GrupoAutomovel grupoAutomovel;
        private ValidadorGrupoAutomovel validador;

        [TestInitialize]
        public void Setup()
        {
            grupoAutomovel = new GrupoAutomovel();
            validador = new ValidadorGrupoAutomovel();
        }

        [TestMethod]
        public void Nome_Do_Grupo_Automovel_É_Obrigatório()
        {
            grupoAutomovel.nome = null;

            var resultado = validador.TestValidate(grupoAutomovel);

            resultado.ShouldHaveValidationErrorFor(g => g.nome);
        }

        [TestMethod]
        public void Nome_Do_Grupo_Automovel_Deve_Ter_Pelo_Menos_Tres_Caracteres()
        {
            grupoAutomovel.nome = "Ab";

            var resultado = validador.TestValidate(grupoAutomovel);

            resultado.ShouldHaveValidationErrorFor(g => g.nome);
        }

        [TestMethod]
        public void Nome_Do_Grupo_Automovel_Pode_Conter_Numero()
        {
            grupoAutomovel.nome = "Grupo123";

            var resultado = validador.TestValidate(grupoAutomovel);

            resultado.ShouldNotHaveValidationErrorFor(g => g.nome);
        }

        [TestMethod]
        public void Nome_Do_Grupo_Automovel_Nao_Deve_Possuir_Caracteres_Especiais()
        {
            grupoAutomovel.nome = "Grupo@Automovel";

            var resultado = validador.TestValidate(grupoAutomovel);

            resultado.ShouldHaveValidationErrorFor(g => g.nome);
        }

        [TestMethod]
        public void Nome_Do_Grupo_Automovel_Deve_Ser_Valido()
        {
            grupoAutomovel.nome = "Grupo Automovel";

            var resultado = validador.TestValidate(grupoAutomovel);

            resultado.ShouldNotHaveValidationErrorFor(g => g.nome);
        }
    }
}
