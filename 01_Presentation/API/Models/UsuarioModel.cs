using System;
using Core.Entities;

namespace API.Models
{
    public class UsuarioModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PerfilDeAcesso { get; set; }
        public string Senha { get; set; }
        public DateTime? DataDeCadastro { get; set; }
        public DateTime? DataDeAtualizacao { get; set; }
        public bool Ativo { get; set; }

        public UsuarioModel() { }

        public UsuarioModel((Usuario usuario, PerfilDeAcesso perfilDeAcesso) tupla) 
        { 
            Id = tupla.usuario?.Id;
            UserName = tupla.usuario?.UserName;
            Email = tupla.usuario?.Email;
            PerfilDeAcesso = tupla.perfilDeAcesso?.Perfil.ToString();
            DataDeCadastro = tupla.perfilDeAcesso?.DataCadastro;
            DataDeAtualizacao = tupla.perfilDeAcesso?.DataAtualizacao;
            Ativo = !tupla.perfilDeAcesso?.DataInativacao.HasValue ?? false;
        }
    }
}