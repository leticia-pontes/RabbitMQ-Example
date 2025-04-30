using Microsoft.AspNetCore.Mvc;
using ConsumerApi.Data;
using ConsumerApi.Models;

namespace ConsumerApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MensagemController : ControllerBase
{
    private readonly AppDbContext _context;

    public MensagemController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Mensagem mensagem)
    {
        if (mensagem == null)
            return BadRequest();

        mensagem.DataRecebimento = DateTime.Now;
        _context.Mensagens.Add(mensagem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Post), new { id = mensagem.Id }, mensagem);
    }
}