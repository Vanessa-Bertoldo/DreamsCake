using lanchonete.Context;
using lanchonete.Models;
using lanchonete.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lanchonete.Repositories
{
    public class LancheRepository : ILanchesRepository
    {
        private readonly AppDbContext _context;
        public LancheRepository(AppDbContext context) 
        { 
            _context = context;
        }
        //O include permite obter dados relacionados incluindo-os no resultado da consulta
        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);
        //Obtem os lanches onde o IsLanchePreferido = true e suas categorias
        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(p => 
            p.IsLanchePreferido).Include(c => c.Categoria);
        //Obtem o lanche a partir de um ID, retornando o primeiro resultado
        public Lanche GetLancheById(int id) => _context.Lanches.FirstOrDefault(
            l => l.LancheId == id);

    }
}
