
using LinkSocial_Domain.DTO.Response;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lauerp_API.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErroController : ControllerBase
{

    [Route("erro")]
    public ErrorViewModelResponse Erro()
    {
        IExceptionHandlerFeature contexto = HttpContext.Features.Get<IExceptionHandlerFeature>();
        Exception excecao = contexto?.Error!;

        HttpStatusCode httpStatusCode = excecao switch
        {
            ArgumentException => HttpStatusCode.NoContent,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            Exception => HttpStatusCode.BadRequest,
            _ => HttpStatusCode.NotFound
        };

        Response.StatusCode = (int)httpStatusCode;

        return new ErrorViewModelResponse(Response.StatusCode.ToString(), excecao.Message, excecao.Data);
    }
}