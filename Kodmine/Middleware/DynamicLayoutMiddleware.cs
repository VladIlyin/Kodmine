using Kodmine.Core.Interfaces;
using Kodmine.Model.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodmine.Middleware
{
    public class DynamicLayoutMiddleware
    {
        static IEnumerable<Rubric> _rubricList;

        public static IEnumerable<Rubric> RubricList {
            get
            {
                if (_rubricRepo == null)
                    return null;

                return _rubricRepo.Get();
            }
        }

        private static IRubricRepository _rubricRepo;
        private static RequestDelegate _next;

        public DynamicLayoutMiddleware(RequestDelegate next, IRubricRepository rubricRepo)
        {
            _rubricRepo = rubricRepo;
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            //FillRubricList();
            //string sessionId = context.Session.Id;
            //context.Items.Add("rubricList", _rubricList);
            return _next(context);// .Invoke();
        }

        static void FillRubricList()
        {
            _rubricList = _rubricRepo.Get();
        }

    }
}
