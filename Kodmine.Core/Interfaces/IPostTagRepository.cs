using System;
using System.Collections.Generic;
using System.Text;

namespace Kodmine.Core.Interfaces
{
    public interface IPostTagRepository
    {
        void AddTagToPost(int tagId, int postId);
        void RemoveTagFromPost(int tagId, int postId);
    }
}
