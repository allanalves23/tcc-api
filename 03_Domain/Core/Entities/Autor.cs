using System;
using System.Collections.Generic;
using Core.Enums;

namespace Core.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public virtual IEnumerable<Artigo> Artigos { get; set; }
        public string UsuarioId { get; set; }
        public Genero Genero { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataRemocao { get; set; }

        public Autor() { }

        public Autor(Usuario usuario) {
            Nome = usuario.UserName;
            Email = usuario.Email;
            UsuarioId = usuario.Id;
            DataCadastro = DateTime.Now;
        }

        public void Validar()
        {
            if(string.IsNullOrEmpty(Nome))
                throw new ArgumentException("É necessário informar o nome");

            ValidarEnderecoDeEmail();
        }

        public void ValidarEnderecoDeEmail()
        {
            if(string.IsNullOrEmpty(Email))
                throw new ArgumentException("É necessário informar o endereço de email");

            if(!Email.Contains("@") || !Email.Contains("."))
                throw new ArgumentException("Endereço de e-mail inválido");
        }

        public void Atualizar() => DataAtualizacao = DateTime.Now;

        public void Remover() => DataRemocao = DateTime.Now;
    }
}