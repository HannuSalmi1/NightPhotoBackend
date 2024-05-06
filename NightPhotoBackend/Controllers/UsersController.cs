
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using NightPhotoBackend.Models;
using NightPhotoBackend.Services;
using System.Linq;
using System.Security.Claims;

namespace NightPhotoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly NightPhotoDbContext _context;

        private readonly IFolderCreator _folderCreator;

        private readonly IUserservice _userService;

        private readonly ILogger<UsersController> _logger;

        public UsersController(NightPhotoDbContext context,
            IFolderCreator folderCreator, IUserservice userService, ILogger<UsersController> logger)
        {
            _context = context;
            _folderCreator = folderCreator;
            _userService = userService;
            _logger = logger;
        }

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsersTables()
        {
            if (_context.UsersTable == null)
            {
                return NotFound();
            }
            return await _context.UsersTable.ToListAsync();
        }

        [HttpGet("getImages")]
        public async Task<ActionResult<IEnumerable<string>>> GetImages()
        {

            return _folderCreator.RetrieveImagesPaths();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUsersTable(int id)
        {
            if (_context.UsersTable == null)
            {
                return NotFound();
            }
            var usersTable = await _context.UsersTable.FindAsync(id);

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
        public async Task<ActionResult<UserModel>> PostUsersTable(UserModel userTable)
        {
            //
            if (_context.UsersTable == null)
            {
                return Problem("Entity set 'NightPhotoDbContext.UsersTables'  is null.");
            }

            var duplicate = _context.UsersTable.SingleOrDefault(x => x.Username == userTable.Username);
           
            if (duplicate != null) 
            {
                _logger.LogInformation("testingtesting1212121 -->" + duplicate.ToString());
                return Problem("Username already taken");
            }
                
            

            await Console.Out.WriteLineAsync();

            _folderCreator.CreateFolder(userTable);
            _context.UsersTable.Add(userTable);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("GetUsersTable", new { id = userTable.Id }, userTable);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersTable(int id)
        {
            if (_context.UsersTable == null)
            {
                return NotFound();
            }
            var usersTable = await _context.UsersTable.FindAsync(id);
            if (usersTable == null)
            {
                return NotFound();
            }

            _context.UsersTable.Remove(usersTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersTableExists(int id)
        {
            return (_context.UsersTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [Authorize]
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var userName = User.FindFirstValue("username");
            if (file != null && file.Length > 0)
            {
                
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(),
                                "wwwroot\\images", userName, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return Ok();
        }


        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }


    }
}
