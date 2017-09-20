/*   Copyright 2017 Matthew Ford <matthew@matthewford.us>
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 */

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MatthewFordUs.WebStarter.WebApi.Models;

namespace MatthewFordUs.WebStarter.WebApi.Controllers
{
  [Produces("application/json")]
  [Route("api/blogs")]
  public class BlogsController : Controller
  {
    private readonly BloggingContext _context;

    public BlogsController(BloggingContext context)
    {
      _context = context;
    }

    // GET: api/Blogs
    [HttpGet]
    public IEnumerable<Blog> GetBlogs()
    {
      return new[]
      {
        new Blog() {Url = "www.example.com"}
      };
      //return _context.Blogs;
    }

    // GET: api/Blogs/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlog([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.BlogId == id);

      if (blog == null)
      {
        return NotFound();
      }

      return Ok(blog);
    }

    // PUT: api/Blogs/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBlog([FromRoute] int id, [FromBody] Blog blog)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != blog.BlogId)
      {
        return BadRequest();
      }

      _context.Entry(blog).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!BlogExists(id))
        {
          return NotFound();
        }
        throw;
      }

      return NoContent();
    }

    // POST: api/Blogs
    [HttpPost]
    public async Task<IActionResult> PostBlog([FromBody] Blog blog)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      _context.Blogs.Add(blog);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetBlog", new {id = blog.BlogId}, blog);
    }

    // DELETE: api/Blogs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlog([FromRoute] int id)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.BlogId == id);
      if (blog == null)
      {
        return NotFound();
      }

      _context.Blogs.Remove(blog);
      await _context.SaveChangesAsync();

      return Ok(blog);
    }

    private bool BlogExists(int id)
    {
      return _context.Blogs.Any(e => e.BlogId == id);
    }
  }
}
