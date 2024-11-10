﻿using ProjetoDePost.Data.DTOs;
using ProjetoDePost.Models;

namespace ProjetoDePost.Services.Interfaces
{
    public interface IHistoricoCampanhaService
    {
        Task GuardarHistorico(Campanha campanha);
        Task<List<HistoricoCampanhaDto>> ObterHistoricoPorEmpresaAsync(int empresaId);
    }
}