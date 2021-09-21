
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entidade;
using static System.String;
using static System.Guid;
using Domain.Repositorio;

namespace Domain.Service
{
    public class PessoaService
    {
        private IPessoaRepositorio RepositorioPessoa { get; }

        public PessoaService(IPessoaRepositorio repositorioPessoa)
        {
            RepositorioPessoa = repositorioPessoa;
        }

        public Pessoa GetPessoa(Guid id)
        {
            return RepositorioPessoa.GetById(id);
        }

        public IEnumerable<Pessoa> GetAll()
        {
            return RepositorioPessoa.GetAll();
        }

        //IMPLEMENTAR MAIS PARA FRENTE
        //public Pessoa GetAuthor(string author)
        //{
        //    if (IsNullOrWhiteSpace(author))
        //    {
        //        return db.Pessoa.Find(author);
        //    }

        //    return db.Pessoa.Where(x => x.Author == author).FirstOrDefault();

        //}

        public Pessoa CreatePessoa(Guid id, string nome,
            string sobrenome,
            DateTime dataDeNascimento,
            string email, string senha
           )
        {

            var pessoa = new Pessoa();
            pessoa.Id = id;
            pessoa.Nome = nome;
            pessoa.Sobrenome = sobrenome;
            pessoa.DataNascimento = dataDeNascimento;
            pessoa.Email = email;
            pessoa.Senha = senha;
            pessoa.ImagemUrlPessoa = "Perfil_default.png";
            pessoa.CreatedAt = DateTime.UtcNow;
            pessoa.UpdatedAt = new DateTime();

            RepositorioPessoa.SaveUpdate(pessoa);

            return pessoa;
        }

        public Pessoa UpdatePessoa(Pessoa pessoaUpdate)
        {

            var pessoa = RepositorioPessoa.GetById(pessoaUpdate.Id);
            if (pessoaUpdate.Nome != null)
                pessoa.Nome = pessoaUpdate.Nome;
            if (pessoaUpdate.Sobrenome != null)
                pessoa.Sobrenome = pessoaUpdate.Sobrenome;
            if (pessoaUpdate.ImagemUrlPessoa != null && pessoaUpdate.ImagemUrlPessoa != "string")
                pessoa.ImagemUrlPessoa = pessoaUpdate.ImagemUrlPessoa;
            if (pessoaUpdate.Amigos.Count() > 0)
                pessoa.Amigos = pessoaUpdate.Amigos;
            if (pessoaUpdate.Posts.Count() > 0)
                pessoa.Posts = pessoaUpdate.Posts;
            pessoa.UpdatedAt = DateTime.UtcNow;

            RepositorioPessoa.SaveUpdate(pessoa);

            //CRIAR UM RETORNO PARA SAVEUPDATE PARA VERIFICAR SE FOI PERSISTIDO A IMAGEM
            //DEPOIS INCLUIR O REPOSITORIO DA UPLOAD DE IMAGEM AQUI

            return pessoa;
        }


        public void DeletePessoa(Guid id)
        {
            RepositorioPessoa.Remove(id);
        }

        public Pessoa CadastraAmigo(Guid idPessoa, Guid idAmigo)
        {
            var pessoa = GetPessoa(idPessoa);
            pessoa.AddAmigo(idAmigo);

            RepositorioPessoa.SaveUpdate(pessoa);

            return pessoa;
        }
    }
}
