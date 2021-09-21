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
        private BlobContainerClient ContainerClient { get; }
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

        public async Task Remove(string fileName)
        {
            BlobClient blobClient = ContainerClient.GetBlobClient(fileName);
            if (fileName != "Perfil_default.png" && fileName != "Post_default.png")
                await blobClient.DeleteIfExistsAsync();
        }

        public IEnumerable<string> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task SaveUpdate(string fileName, MemoryStream ms)
        {
            BlobClient blobClient = ContainerClient.GetBlobClient(fileName);
            var existe = await blobClient.ExistsAsync();
            if (existe.Value == true)
                await blobClient.DeleteIfExistsAsync();

            await blobClient.UploadAsync(ms);
        }
    }
}
