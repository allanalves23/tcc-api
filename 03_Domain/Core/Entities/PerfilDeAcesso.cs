using System;
using Core.Enums;

namespace Core.Entities
{
    public class PerfilDeAcesso
    {
        public string UsuarioId { get; set; }
        public TipoUsuario Perfil { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataInativacao { get; set; }

        public PerfilDeAcesso() { }

        public PerfilDeAcesso(Usuario usuario, string perfil)
        { 
            ValidarPerfil(perfil);
            if (usuario == null || string.IsNullOrEmpty(usuario.Id))
                throw new ArgumentException("Usuario não informado");

            UsuarioId = usuario.Id;
            Perfil = (TipoUsuario) Enum.Parse(typeof(TipoUsuario), perfil);
            DataCadastro = DateTime.Now;
        }

        public void ValidarPerfil(string perfil)
        {
            if (!Enum.IsDefined(typeof(TipoUsuario), perfil))
                throw new ArgumentException("Perfil de Acesso inválido");
        }

        public void Atualizar(string perfil)
        {
            ValidarPerfil(perfil);

            Perfil = (TipoUsuario) Enum.Parse(typeof(TipoUsuario), perfil);
            DataAtualizacao = DateTime.Now;
        }
    }
}