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

        public async Task Remove(string fileName)
        {
            await ImagemRepositorio.Remove(fileName);
        }

        public async Task SaveUpdate(string fileName, MemoryStream ms)
        {
            await ImagemRepositorio.SaveUpdate(fileName, ms);
        }
    }
}
