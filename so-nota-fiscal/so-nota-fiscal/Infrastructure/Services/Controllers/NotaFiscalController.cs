using IdempotentAPI.Filters;
using IdempotentAPI.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoNotaFiscal.Application.Commands;
using SoNotaFiscal.Application.Commands.Requests;
using SoNotaFiscal.Application.Queries;
using SoNotaFiscal.Application.Queries.Responses;
using System;

namespace SoNotaFiscal.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NotaFiscalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotaFiscalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Serviço: Emissão de NF-e
        /// </summary>
        /// <remarks>
        ///  Exemplo de requisição:
        ///
        ///  POST \
        ///  {URL_BASE}/api/v1/notafiscal/nota
        ///  
        ///  { \
        ///     "cliente": "José da Silva", \
        ///     "valor": "100" \
        ///  } 
        ///         
        ///</remarks>
        /// <param name="request">Corpo da requisição do recurso.</param>
        /// <response code="200">Retorna sucesso na consulta</response>
        /// <response code="400">Se houver algum tipo de problema/validação na consulta</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("nota")]
        [Idempotent(Enabled = false, ExpireHours = 24)]
        [HttpPost()]
        public async Task<ActionResult<ConsultaNotaFiscalResponse>> Post(
            EmitirNotaFiscalRequest request, [FromHeader] string IdempotencyKey
            )
        {
            var nota = await _mediator.Send<ConsultaNotaFiscalResponse>(new GetNotaFiscalByIdIdempotencyKeyQuery(IdempotencyKey));
            if(nota != null)
                return Ok(nota);


            //CQRS - Responsabilidade: COMMAND - GRAVAÇÃO DA NOTA FISCAL
            await _mediator.Send(new CreateNotaFiscalCommand(request.Destinatario, request.Valor, IdempotencyKey));


            //CQRS - Responsabilidade: QUERY - CONSULTA DOS DADOS DA NOTA FISCAL 
            nota = await _mediator.Send<ConsultaNotaFiscalResponse>(new GetNotaFiscalByIdIdempotencyKeyQuery(IdempotencyKey));

            return Ok(nota);
        }
    }
}