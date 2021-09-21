using Azure.Storage.Blobs;
using Domain.Repositorio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BlobStorage
{
    public class ImagemRepositorio : IImagemRepositorio
    {
        private BlobServiceClient BlobServiceClient { get; }
        private BlobContainerClient ContainerClient { get;}
        private string UrlBlobStorageImagem { get; set; }

        public ImagemRepositorio(string connectionString, string urlBlobStorageImagem)
        {
            BlobServiceClient = new BlobServiceClient(connectionString);
            UrlBlobStorageImagem = urlBlobStorageImagem;
            ContainerClient = BlobServiceClient.GetBlobContainerClient("imagens");
        }

        public string GetById(string fileName)
        {
            return $"{UrlBlobStorageImagem}{fileName}";
        }

        public void Remove(string fileName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAll()
        {
            throw new NotImplementedException();
        }

        public void SaveUpdate(string fileName, MemoryStream ms)
        {
            BlobClient blobClient = ContainerClient.GetBlobClient(fileName);
            blobClient.Upload(ms);
        }
    }
}
