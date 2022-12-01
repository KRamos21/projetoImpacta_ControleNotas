using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetoImpacta_ControleNotas.Data;
using projetoImpacta_ControleNotas.Models;

namespace projetoImpacta_ControleNotas.Controllers
{
    public class AlunosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlunosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Alunos
        public async Task<IActionResult> Index()
        {
              return View(await _context.Alunos.ToListAsync());
        }

        // GET: Alunos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Alunos == null)
            {
                return NotFound();
            }

            var alunoModel = await _context.Alunos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (alunoModel == null)
            {
                return NotFound();
            }

            return View(alunoModel);
        }

        // GET: Alunos/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Nota1,Nota2")] AlunoModel alunoModel)
        {
            float NotaFinal = (alunoModel.Nota1 + alunoModel.Nota2) / 2;
            alunoModel.NotaFinal = NotaFinal;

            if(alunoModel.NotaFinal >= 6)
            {
                alunoModel.Situacao = "Aprovado";
            } 
            else if (alunoModel.NotaFinal > 4 && alunoModel.NotaFinal < 6)
            {
                alunoModel.Situacao = "Recuperação";
            } 
            else
            {
                alunoModel.Situacao = "Reprovado";
            }

            if (alunoModel != null)
            {
                _context.Add(alunoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alunoModel);
        }

        // GET: Alunos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Alunos == null)
            {
                return NotFound();
            }

            var alunoModel = await _context.Alunos.FindAsync(id);
            if (alunoModel == null)
            {
                return NotFound();
            }
            return View(alunoModel);
        }

        // POST: Alunos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Nota1,Nota2")] AlunoModel alunoModel)
        {
            if (id != alunoModel.ID)
            {
                return NotFound();
            }

            float NotaFinal = (alunoModel.Nota1 + alunoModel.Nota2) / 2;
            alunoModel.NotaFinal = NotaFinal;

            if (alunoModel.NotaFinal >= 6)
            {
                alunoModel.Situacao = "Aprovado";
            }
            else if (alunoModel.NotaFinal > 4 && alunoModel.NotaFinal < 6)
            {
                alunoModel.Situacao = "Recuperação";
            }
            else
            {
                alunoModel.Situacao = "Reprovado";
            }


            if (alunoModel != null)
            {
                try
                {
                    _context.Update(alunoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlunoModelExists(alunoModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(alunoModel);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!AlunoModelExists(alunoModel.ID))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}

            return View(alunoModel);
        }

        // GET: Alunos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Alunos == null)
            {
                return NotFound();
            }

            var alunoModel = await _context.Alunos
                .FirstOrDefaultAsync(m => m.ID == id);
            
            if (alunoModel == null)
            {
                return NotFound();
            }

            return View(alunoModel);
        }

        // POST: Alunos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Alunos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Alunos'  is null.");
            }
            var alunoModel = await _context.Alunos.FindAsync(id);
            if (alunoModel != null)
            {
                _context.Alunos.Remove(alunoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlunoModelExists(int id)
        {
          return _context.Alunos.Any(e => e.ID == id);
        }
    }
}
