
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

        //public Pessoa GetPesoaEmail(string email)
        //{
        //    return RepositorioPessoa.GetByEmail(email);
        //}

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

            var Pessoa = new Pessoa();
            Pessoa.Id = id;
            Pessoa.Nome = nome;
            Pessoa.Sobrenome = sobrenome;
            Pessoa.DataNascimento = dataDeNascimento;
            Pessoa.Email = email;
            Pessoa.Senha = senha;
            Pessoa.CreatedAt = DateTime.UtcNow;
            Pessoa.UpdatedAt = new DateTime();

            RepositorioPessoa.SaveUpdate(Pessoa);

            return Pessoa;
        }

        public Pessoa UpdatePessoa(Pessoa pessoaUpdate)
        {

            var pessoa = RepositorioPessoa.GetById(pessoaUpdate.Id);
            if (pessoaUpdate.Nome != null)
                pessoa.Nome = pessoaUpdate.Nome;
            if (pessoaUpdate.Sobrenome != null)
                pessoa.Sobrenome = pessoaUpdate.Sobrenome;
            if (pessoaUpdate.DataNascimento != new DateTime())
                pessoa.DataNascimento = pessoaUpdate.DataNascimento;
            if (pessoaUpdate.Email != null)
                pessoa.Email = pessoaUpdate.Email;
            if (pessoaUpdate.Senha != null)
                pessoa.Senha = pessoaUpdate.Senha;
            if (pessoaUpdate.ImagemUrlPessoa != null)
                pessoa.ImagemUrlPessoa = pessoaUpdate.ImagemUrlPessoa;
            if (pessoaUpdate.Amigos != null)
                pessoa.Amigos = pessoaUpdate.Amigos;
            if (pessoaUpdate.Posts != null)
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
