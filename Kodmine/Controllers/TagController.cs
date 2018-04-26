using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kodmine.Controllers.Base;
using Kodmine.Core.Interfaces;
using Kodmine.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kodmine.Controllers
{
    public class TagController : ControllerCRUD<Tag>
    {
        public TagController(ITagRepository repository) : base(repository)
        {
        }
    }
}