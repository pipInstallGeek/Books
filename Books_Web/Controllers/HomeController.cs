using AutoMapper;
using Books_Web.Models;
using Books_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Books_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public HomeController(IMapper mapper, IBookService bookService)
        {
            _mapper = mapper;
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var booksResponse = await _bookService.GetAllAsync<List<Book>>();
            return View(booksResponse);
        }
    }
}
