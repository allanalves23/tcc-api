using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IArtigoService 
    { 
        Artigo Criar(string titulo, string idUsuario);
        Artigo Atualizar(int? id, string titulo, string descricao, string conteudo);
        Artigo Atualizar(int? id, int? temaId, int? categoriaId);
        Artigo Atualizar(int? id, string urlPersonalizada);
        Artigo Obter(int? idArtigo);
        Artigo Obter(string urlPersonalizada);
        IEnumerable<Artigo> Obter(string termo, int? skip = 0, int? take = 10);
        Artigo Remover(int? idArtigo);
        Artigo Publicar(int? idArtigo);
        Artigo Inativar(int? idArtigo);
        Artigo Impulsionar(int? idArtigo);
    }
}