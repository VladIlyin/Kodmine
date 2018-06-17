using Kodmine.Controllers.Base;
using Kodmine.Core.Interfaces;
using Kodmine.Model.Models;
using Microsoft.AspNetCore.Authorization;

namespace Kodmine.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class RubricController : ControllerCRUD<Rubric>
    {
        public RubricController(IRubricRepository repository) : base(repository)
        {
        }
    }
}