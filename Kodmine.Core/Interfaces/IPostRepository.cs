using Kodmine.Core.Base;
using Kodmine.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kodmine.Core.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        void SaveContent(int id, string content);
        void SetTopic(int id, int value);
        IEnumerable<Post> PostListMainPage(int take);
    }
}
