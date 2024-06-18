using AutoMapper;
using Books_Web.Models;
using Books_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Books_Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexBook()
        {
            var booksResponse = await _bookService.GetAllAsync<List<Book>>();
            return View(booksResponse);
        }

        public async Task<IActionResult> CreateBook()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBook(Book model)
        {
            if (ModelState.IsValid)
            {
                var book = _mapper.Map<Book>(model);
                var response = await _bookService.CreateAsync<APIResponse>(book);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Book created successfully";
                    return RedirectToAction(nameof(IndexBook));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }

        public async Task<IActionResult> UpdateBook(int bookId)
        {
            var bookResponse = await _bookService.GetAsync<Book>(bookId);
            if (bookResponse != null)
            {
                return View(bookResponse);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBook(Book model)
        {
            if (ModelState.IsValid)
            {
                var response = await _bookService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Book updated successfully";
                    return RedirectToAction(nameof(IndexBook));
                }
            }
            TempData["error"] = "Error encountered.";
            return View(model);
        }

        public async Task<IActionResult> DeleteBook(int bookId)
        {
            var response = await _bookService.GetAsync<Book>(bookId);
            if (response != null )
            {
                
                return View(response);
            }
            return NotFound();
        }

        [HttpPost, ActionName("DeleteBook")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBookConfirmed(Book model)
        {
            var response = await _bookService.DeleteAsync<APIResponse>(model.Id);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Book deleted successfully";
                return RedirectToAction(nameof(IndexBook));
            }
            TempData["error"] = "Error encountered.";
            return View();
        }
    }
}
