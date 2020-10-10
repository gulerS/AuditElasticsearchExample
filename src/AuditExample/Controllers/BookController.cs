using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AuditExample.Data;
using AuditExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuditExample.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryContext _context;

        public BookController(LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Books.ToList();
            return View(data);
        }

        public IActionResult Detail(int? ID)
        {
            Book book = null;
            if (ID == null)
            {
                book = new Book();
            }
            else
            {
                book = _context.Books.FirstOrDefault(book => book.Id == ID);
            }

            return View(book);
        }

        public async Task<IActionResult> Update(Book book)
        {
            if (book.Id == 0)
            {
                var newBook = new Book();
                newBook.Name = book.Name;
                newBook.ISBN = book.ISBN;
                newBook.Author = book.Author;
                await _context.Books.AddAsync(newBook);
            }
            else
            {
                var updateBook = _context.Books.FirstOrDefault(b => b.Id == book.Id);
                updateBook.Name = book.Name;
                updateBook.ISBN = book.ISBN;
                updateBook.Author = book.Author;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int? ID)
        {
            if (ID != 0)
            {
                var dbook = _context.Books.FirstOrDefault(b => b.Id == ID);
                if (dbook!=null)
                {
                    _context.Books.Remove(dbook);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }  
            }

            return RedirectToAction("Error", "Home");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}