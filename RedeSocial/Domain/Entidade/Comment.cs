using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entidade
{
    public class Comment
    {
        public Comment(){Post = new PostComment();}
        [Key]
        public Guid Id { get; set; }
        public string Text{ get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpDateTime { get; set; }
        public PostComment Post { get; set; }

        internal void AddPost(Guid postId)
        {
            Post = new PostComment() { PostId = postId, CommentId = this.Id };
        }
    }
}
