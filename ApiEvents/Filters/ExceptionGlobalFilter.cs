using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.SqlClient;

namespace ApiEvents.Filters
{
    public class ExceptionGlobalFilter : ExceptionFilterAttribute
    {

        public override void OnException(ExceptionContext context)
        {
            var problem = new ProblemDetails()
            {
                Status = 500,
                Title = "Erro inesperado. Tente novamente",
                Detail = "Ocorreu um erro inesperado",
                Type = context.Exception.GetType().Name
            };
            Console.WriteLine(@$"Tipo da exceção: {context.Exception.GetType().Name}
Descrição:{context.Exception.Message},
Stack Trace: {context.Exception.StackTrace}");

            switch (context.Exception)
            {

                case NullReferenceException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status417ExpectationFailed;
                    problem.Title = "Erro inesperado no sistema";
                    problem.Status = 503;
                    context.Result = new ObjectResult(problem);
                    break;

                case SqlException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                    problem.Title = "Erro inesperado ao se comunicar com o banco de dados";
                    problem.Status = 417;
                    context.Result = new ObjectResult(problem);
                    break;

                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = new ObjectResult(problem);
                    break;
            }

        }
    }
}
