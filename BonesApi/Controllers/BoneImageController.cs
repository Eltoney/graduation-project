using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BonesApi.Models;

namespace BonesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoneImageController : ControllerBase
    {
        private readonly BoneImageContext _context;

        public BoneImageController(BoneImageContext context)
        {
            _context = context;
        }

        // GET: api/BoneImage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoneImage>>> GetBoneImages()
        {
            return await _context.BoneImages.ToListAsync();
        }

        // GET: api/BoneImage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoneImage>> GetBoneImage(int id)
        {
            var boneImage = await _context.BoneImages.FindAsync(id);

            if (boneImage == null)
            {
                return NotFound();
            }

            return boneImage;
        }

        // PUT: api/BoneImage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoneImage(int id, BoneImage boneImage)
        {
            if (id != boneImage.Id)
            {
                return BadRequest();
            }

            _context.Entry(boneImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoneImageExists(id))
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

        // POST: api/BoneImage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BoneImage>> PostBoneImage(BoneImage boneImage)
        {
            _context.BoneImages.Add(boneImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBoneImage), new { id = boneImage.Id }, boneImage);
        }

        // DELETE: api/BoneImage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoneImage(int id)
        {
            var boneImage = await _context.BoneImages.FindAsync(id);
            if (boneImage == null)
            {
                return NotFound();
            }

            _context.BoneImages.Remove(boneImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoneImageExists(int id)
        {
            return _context.BoneImages.Any(e => e.Id == id);
        }
    }
}
