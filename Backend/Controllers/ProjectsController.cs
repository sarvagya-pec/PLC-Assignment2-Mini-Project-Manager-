using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // requires valid JWT
    public class ProjectsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/projects
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var userId = User.FindFirstValue("userId");
            if (userId == null) return Unauthorized();

            var projects = await _context.Projects
                .Where(p => p.UserId == int.Parse(userId))
                .Include(p => p.Tasks)
                .ToListAsync();

            return Ok(projects);
        }

        // POST: api/projects
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            var userId = User.FindFirstValue("userId");
            if (userId == null) return Unauthorized();

            project.UserId = int.Parse(userId);
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return Ok(project);
        }
    }
}
