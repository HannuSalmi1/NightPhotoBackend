using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NightPhotoBackend.Models;
using NightPhotoBackend.Services;

namespace NightPhotoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NightPhotoDbContext _context;

        private readonly IFolderCreator _folderCreator;

        public UsersController(NightPhotoDbContext context, IFolderCreator folderCreator)
        {
            _context = context;
            _folderCreator = folderCreator;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsersTables()
        {
          if (_context.UsersTables == null)
          {
              return NotFound();
          }
            return await _context.UsersTables.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUsersTable(int id)
        {
          if (_context.UsersTables == null)
          {
              return NotFound();
          }
            var usersTable = await _context.UsersTables.FindAsync(id);

            if (usersTable == null)
            {
                return NotFound();
            }

            return usersTable;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersTable(int id, UserModel usersTable)
        {
            if (id != usersTable.Id)
            {
                return BadRequest();
            }

            _context.Entry(usersTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserModel>> PostUsersTable(UserModel userModel)
        {
          if (_context.UsersTables == null)
          {
              return Problem("Entity set 'NightPhotoDbContext.UsersTables'  is null.");
          }
            
            _folderCreator.CreateFolder(userModel);
            _context.UsersTables.Add(userModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsersTable", new { id = userModel.Id }, userModel);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersTable(int id)
        {
            if (_context.UsersTables == null)
            {
                return NotFound();
            }
            var usersTable = await _context.UsersTables.FindAsync(id);
            if (usersTable == null)
            {
                return NotFound();
            }

            _context.UsersTables.Remove(usersTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersTableExists(int id)
        {
            return (_context.UsersTables?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return Ok();
        }

    }
}
