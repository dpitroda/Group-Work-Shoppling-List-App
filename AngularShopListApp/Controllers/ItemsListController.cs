﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GroupCWebAPI.ViewModels;
using GroupCWebAPI;
using AngularShopListApp.Data;
using GroupCWebAPI.Data;
using GroupCWebAPI.Models;

namespace GroupCWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemsListController(ApplicationDbContext _context)
        {
            this._context = _context;
        }

        // GET: api/ItemsList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemsList>>> GetItemsLists()
        {

            //Character dbCharacter 

           var temp =  await _context.ItemsList.ToListAsync();
            var test=  await _context.ItemsList
           .Include(e => e.ItemsListItem)
           .ThenInclude(e => e.Item)
           .ToListAsync();

            return temp;
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemsList>> GetItemsLists(int id)
        {
            // var ItemLists = await _context.ItemsLists.FindAsync(id);


            //  var ItemLists = await _context.ItemsLists
            //.Include(c => c.Id)
            //   .Include(c => c.ItemsListItem).ThenInclude(cs => cs.Item)
            //   .FirstOrDefaultAsync(c => c.Id == id);

            try
            {
                //var itemLists = await _context.ItemsLists.FindAsync(id);
                 var itemLists = await _context.ItemsList
                .Include(c => c.ItemsListItem)
                .ThenInclude(cs => cs.Item)
                .FirstOrDefaultAsync(c => c.Id == id);

               // var itemLists = await _context.ItemsLists.Include(itemslist => itemslist.Id).ThenInclude(row => row.Item).First(itemslist => itemslist.Id == id);


                if (itemLists == null)
                {
                    return NotFound();
                }

                return itemLists;

            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
         
        }

        // POST: api/
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ItemsList>> PostItemsList(ItemsList itemlist)
        {
            _context.ItemsList.Add(itemlist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemsLists", new { id = itemlist.Id}, itemlist);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemsList>> DeletesItems(int id)
        {
            var items = await _context.ItemsList.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }

            _context.ItemsList.Remove(items);
            await _context.SaveChangesAsync();

            return items;
        }

        private bool ItemsExists(int id)
        {
            return _context.ItemsList.Any(e => e.Id == id);
        }


    }
}






/*namespace webApiPeer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly Context _context;

        public ItemController(Context context)
        {
            _context = context;
        }


        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItems(int id, Item items)
        {
            if (id != items.Id)
            {
                return BadRequest();
            }

            _context.Entry(items).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemsExists(id))
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

        

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItems(int id)
        {
            var items = await _context.
.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }

            _context.Items.Remove(items);
            await _context.SaveChangesAsync();

            return items;
        }

        private bool ItemsExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}*/
