using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositorio
{
    public interface IImagemRepositorio
    {
        string GetById(string fileName);
        void Remove(string fileName);
        IEnumerable<string> GetAll();
        void SaveUpdate(string fileName, MemoryStream ms);
    }
}
