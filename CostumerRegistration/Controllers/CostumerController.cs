using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CostumerRegistration.Controllers
{
    public class CostumerController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;



        public CostumerController(DataContext context, IWebHostEnvironment hostingEnvironment)
        {
           

            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult Home()
        {
           return View();
        }

        // GET: Costumers
        public async Task<IActionResult> Index()
        {
              return _context.Costumers != null ? 
                          View(await _context.Costumers.ToListAsync()) :
                          Problem("Entity set 'DataContext.Costumers'  is null.");
        }

        // GET: Costumers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Costumers == null)
            {
                return NotFound();
            }

            var costumer = await _context.Costumers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (costumer == null)
            {
                return NotFound();
            }

            return View(costumer);
        }

        // GET: Costumers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Costumers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cnpj,Segment,Cep,Street,Complement,Neighborhood,City,State,Number,Photo")] Costumer costumer, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    if(Path.GetExtension(file.FileName).Equals(".png", StringComparison.OrdinalIgnoreCase))
                    {

                        // Obtém o caminho para o diretório wwwroot (onde sua aplicação normalmente armazena arquivos estáticos)
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Storage");

                        // Verifica se o diretório para armazenar as imagens existe, se não, cria-o
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Cria o caminho completo para a imagem
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Salva a imagem no diretório
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        // Salva apenas o caminho relativo da imagem no banco de dados
                        costumer.Photo = Path.Combine("Storage", uniqueFileName);
                    }
                    else 
                    {
                        
                        ModelState.AddModelError("File", "Invalid file format, only .png files are allowed.");
                        // Retorne a mesma view com os dados do formulário para que o usuário possa corrigir
                        return View(costumer);
                    }
                    
                }
                    _context.Costumers.Add(costumer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            return View(costumer);
        }

        // GET: Costumers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Costumers == null)
            {
                return NotFound();
            }

            var costumer = await _context.Costumers.FindAsync(id);
            if (costumer == null)
            {
                return NotFound();
            }
            return View(costumer);
        }

        // POST: Costumers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Cnpj,Segment,Cep,Street,Complement,Neighborhood,City,State,Number,Photo")] Costumer costumer, IFormFile file)
        {
            if (id != costumer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    if (Path.GetExtension(file.FileName).Equals(".png", StringComparison.OrdinalIgnoreCase))
                    {

                        // Obtém o caminho para o diretório wwwroot (onde sua aplicação normalmente armazena arquivos estáticos)
                        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Storage");

                        // Verifica se o diretório para armazenar as imagens existe, se não, cria-o
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // Cria o caminho completo para a imagem
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Salva a imagem no diretório
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }

                        // Salva apenas o caminho relativo da imagem no banco de dados
                        costumer.Photo = Path.Combine("Storage", uniqueFileName);
                    }
                    else
                    {

                        ModelState.AddModelError("File", "Invalid file format, only .png files are allowed.");
                        // Retorne a mesma view com os dados do formulário para que o usuário possa corrigir
                        return View(costumer);
                    }

                }
                
                
                    _context.Update(costumer);
                    await _context.SaveChangesAsync();
                
              
            }
            return View(costumer);
        }

        // GET: Costumers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Costumers == null)
            {
                return NotFound();
            }

            var costumer = await _context.Costumers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (costumer == null)
            {
                return NotFound();
            }

            return View(costumer);
        }

        // POST: Costumers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Costumers == null)
            {
                return Problem("Entity set 'DataContext.Costumers'  is null.");
            }
            var costumer = await _context.Costumers.FindAsync(id);
            if (costumer != null)
            {
                _context.Costumers.Remove(costumer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CostumerExists(int id)
        {
          return (_context.Costumers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
