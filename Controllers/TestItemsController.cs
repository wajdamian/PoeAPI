using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiAppTest.Models;
using System.Security.Cryptography.X509Certificates;

namespace ApiAppTest.Controllers
{
    [Route("TestujeItema")]
    [ApiController]
    public class TestItemsController : ControllerBase
    {
        private readonly ItemContext _context;

        public TestItemsController(ItemContext context)
        {
            _context = context;
        }

        // GET: api/TestItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestItemDTO>>> GetTestItems()
        {
            return await _context.TestItems
                    .Select(x => ItemToDTO(x))
                    .ToListAsync();
        }

        // GET: api/TestItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestItemDTO>> GetTestItem(long id)
        {
            var testItem = await _context.TestItems.FindAsync(id);

            if (testItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(testItem);
        }

        // PUT: api/TestItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestItem(long id, TestItemDTO testItemDTO)
        {
            if (id != testItemDTO.Id)
            {
                return BadRequest();
            }

            var testItem = await _context.TestItems.FindAsync(id);
            if (testItem == null) return NotFound();

            testItem.IsComplete = testItemDTO.IsComplete;
            testItem.Name = testItemDTO.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                    return NotFound();
            }

            return NoContent();
        }

        // POST: api/TestItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TestItemDTO>> PostTestItem(TestItemDTO testItemDTO)
        {
            var testItem = new TestItem
            {
                IsComplete = testItemDTO.IsComplete,
                Name = testItemDTO.Name
            };

            _context.TestItems.Add(testItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTestItem), new { id = testItem.Id }, ItemToDTO(testItem));
        }

        // DELETE: api/TestItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TestItemDTO>> DeleteTestItem(long id)
        {
            var testItem = await _context.TestItems.FindAsync(id);
            if (testItem == null)
            {
                return NotFound();
            }

            _context.TestItems.Remove(testItem);
            await _context.SaveChangesAsync();

            return ItemToDTO(testItem);
        }

        private bool TestItemExists(long id)
        {
            return _context.TestItems.Any(e => e.Id == id);
        }

        private static TestItemDTO ItemToDTO(TestItem item) =>
            new TestItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            };
    }
}
