using System;

namespace Domain.Entidade
{
    public class PostComment
    {
        public int Id { get; set; }
        public Guid PostId { get; set; }
        public Guid CommentId { get; set; }
    }
}