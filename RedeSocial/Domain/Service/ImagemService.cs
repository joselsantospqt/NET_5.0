using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class ImagemService : IImagemRepositorio
    {
        public ImagemService(IImagemRepositorio imagemRepositorio)
        {
            ImagemRepositorio = imagemRepositorio;
        }

        public IImagemRepositorio ImagemRepositorio { get; }

        public IEnumerable<string> GetAll()
        {
            throw new NotImplementedException();
        }

        public string GetById(string fileName)
        {
            return ImagemRepositorio.GetById(fileName);
        }

        public void Remove(string fileName)
        {
            throw new NotImplementedException();
        }

        public void SaveUpdate(string fileName, MemoryStream ms)
        {
            ImagemRepositorio.SaveUpdate(fileName, ms);
        }
    }
}
