using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FinalProject.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var items = _context.Item.Include(i => i.Collection).AsNoTracking();
            return View(await items.ToListAsync());
        }

        // GET: Items/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.Item.Where(j => (j.Name.Contains(SearchPhrase)) || (j.Tag.Contains(SearchPhrase))).ToListAsync());
        }

        // GET: Items/ShowItems/5
        public async Task<IActionResult> ShowItems(int id)
        {
            var amount = _context.Item.Where(ci => ci.CollectionID.Equals(id)).Count();
            Update(id, amount);
            return View("Index", await _context.Item.Where(j => j.CollectionID.Equals(id)).ToListAsync());
        }

        public void Update(int id, int amount)
        {
            var coll = _context.Collection.FirstOrDefault(i => i.CollectionID == id);
            coll.ItemsAmount = amount;
            _context.SaveChanges();
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.Collection)
                .Include(c => c.Comments)
                .Include(l => l.Likes)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        public Item GetItem(int id)
        {
            return _context.Item.Include(c => c.Comments).FirstOrDefault(i => i.Id == id);
        }

        public Item GetLikes(int id)
        {
            return _context.Item.Include(c => c.Likes).FirstOrDefault(i => i.Id == id);
        }

        // GET: Items/Details/Comments/5
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Comment([Bind("CommentID,User_Name,Text,ItemID")] Comment comment)
        {            
            var item = GetItem(comment.ItemID);
            item.Comments = item.Comments ?? new List<Comment>();

            if(comment.Text == null)            
                return RedirectToAction("Details", new { id = comment.ItemID });
            

            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();               
            }
            return RedirectToAction("Details", new { id = comment.ItemID });
        }

        // GET: Items/Details/Likes/5
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Like([Bind("LikeID,ItemID,User_Name")] Like like)
        {
            var item_likes = GetLikes(like.ItemID);
            item_likes.Likes = item_likes.Likes ?? new List<Like>();

            foreach (var l in item_likes.Likes)
            {
                if (l.User_Name == User.Identity.Name.ToString())
                {
                    _context.Remove(l);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = l.ItemID });
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(like);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", new { id = like.ItemID });
        }

        // GET: Items/Create
        [Authorize]
        public IActionResult Create()
        {
            CollectionDropDownList();
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Tag,CollectionID,User_Name,Created")] Item item)
        {
            if (ModelState.IsValid)
            {
                AddTag(item.Tag);
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("ShowItems", new { id = item.CollectionID });
            }
            CollectionDropDownList(item.CollectionID);
            return View(item);
        }  
        
        public void AddTag(string Tag)
        {
            string[] words = Tag.Split(' ');
            foreach (var word in words)
            {
                var tag = _context.Tag.FirstOrDefault(t => t.Text.Contains(word));
                if (tag == null)
                {
                    _context.Add(new Tag
                    {
                        Text = word,
                        Count = 1
                    });
                }
                else
                    tag.Count += 1;
            }
            _context.SaveChanges();
        }

        // GET: Items/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            CollectionDropDownList();
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Tag,CollectionID,User_Name,Created")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DeleteTag(item.Tag);
                    AddTag(item.Tag);
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ShowItems", new { id = item.CollectionID });
            }
            CollectionDropDownList(item.CollectionID);
            return View(item);
        }

        private void CollectionDropDownList(object selectedCollection = null)
        {
            var collectionsQuery = from c in _context.Collection
                                   orderby c.Name
                                   select c;

            ViewBag.CollectionID = new SelectList(collectionsQuery.Where(c => c.User_Name == User.Identity.Name.ToString()).AsNoTracking(), "CollectionID", "Name", selectedCollection);
        }

        // GET: Items/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            DeleteTag(item.Tag);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowItems", new { id = item.CollectionID });
        }

        public void DeleteTag(string Tag)
        {
            string[] words = Tag.Split(' ');
            foreach (var word in words)
            {
                var tag = _context.Tag.FirstOrDefault(t => t.Text.Contains(word));
                if (tag == null)
                    break;
                else if (tag.Count > 1)
                    tag.Count -= 1;
                else
                    _context.Tag.Remove(tag);
            }
            _context.SaveChanges();
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }
    }
}
