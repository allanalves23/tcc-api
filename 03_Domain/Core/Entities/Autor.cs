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
        public string Senha { get; set; }
        public virtual IEnumerable<Artigo> Artigos { get; set; }
        public TipoUsuario Tipo { get; set; }
        public Genero Genero { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataRemocao { get; set; }

        public void Validar()
        {
            if(string.IsNullOrEmpty(Nome))
                throw new ArgumentException("É necessário informar o nome");

            ValidarEnderecoDeEmail();
            ValidarSenha();
        }

        public void ValidarEnderecoDeEmail()
        {
            if(string.IsNullOrEmpty(Email))
                throw new ArgumentException("É necessário informar o endereço de email");

            if(!Email.Contains("@") || !Email.Contains("."))
                throw new ArgumentException("Endereço de e-mail inválido");
        }

        public void ValidarSenha()
        {
            if(string.IsNullOrEmpty(Senha))
                throw new ArgumentException("É necessário informar uma senha");

            if(Senha.Length < 8)
                throw new ArgumentException("É necessário informar pelo menos 8 caracteres");
        }

        public void Atualizar() => DataAtualizacao = DateTime.Now;

        public void Remover() => DataRemocao = DateTime.Now;
    }
}