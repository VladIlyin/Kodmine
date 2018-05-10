using Kodmine.Core.Interfaces;
using Kodmine.Model.Models;
using Kodmine.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Kodmine.Services
{
    public static class DynamicLayoutService
    {
        
        public static IEnumerable<Rubric> RubricList {
            get
            {
                if (Startup.ServiceMan == null)
                    return null;

                var rubricRepo = Startup.ServiceMan.GetService<IRubricRepository>();

                if (rubricRepo == null)
                    throw new ArgumentNullException("rubricRepo", "Не удалось разрешить зависимость (null)");

                return rubricRepo.Get();
            }
        }

    }
}
