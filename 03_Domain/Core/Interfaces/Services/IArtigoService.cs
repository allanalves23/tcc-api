using System.Collections.Generic;
using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IArtigoService { 
        Artigo Criar(string titulo, string idUsuario);
        Artigo Obter(int? idArtigo);
        IEnumerable<Artigo> Obter(string termo, int? skip = 0, int? take = 10);
        Artigo Remover(int? idArtigo);
        Artigo Publicar(int? idArtigo);
        Artigo Inativar(int? idArtigo);
        Artigo Impulsionar(int? idArtigo);
    }
}