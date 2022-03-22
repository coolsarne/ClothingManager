using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using ClothingManager.BL;
using ClothingManager.BL.Domain;
using ClothingManager.UI.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UI.MVC.GraphQL;

namespace ClothingManager.UI.MVC.Controllers
{
    public class DesignerController : Controller
    {
        private readonly IManager _manager;
        private readonly GraphqlClient _graphqlClient;
        private readonly IHttpClientFactory _clientFactory;
        //private static string ENDPOINT = "desingers";
        //private static string ENDPOINT_CLOTHINGPIECES = "clothingpieces";

        public DesignerController(IManager manager, GraphqlClient graphqlClient, IHttpClientFactory clientFactory)
        {
            _manager = manager;
            _graphqlClient = graphqlClient;
            _clientFactory = clientFactory;
        }

        
        // GET: /Designer
        public async Task<IActionResult> Index()
        {
            var result = await _graphqlClient.AllDesigners.ExecuteAsync();

            var designers = result.Data?.Designers?
                .Select(x => new Designer
                {
                    Name = x.Name,
                    Age = x.Age,
                    Nationality = x.Nationality,
                    Id = x.Id
                });
            return View(designers);
        }

        // GET: /Designer/Details/<Id>
        public IActionResult Details(int designerId)
        {
            Designer designer = _manager.GetDesignerWithClothingPieces(designerId);
            //Designer designer = GetDesignersAsync(designerId);
            //var designer = await _graphqlClient.AllDesigners.Exec
            return View(designer);
        }

        // GET: /Designer/Add
        [HttpGet]
        public IActionResult Add()
        {
            //ViewData["ClothingPieces"] = GetClothingPieces();
            return View();
        }

        // POST: /Designer/Add
        [HttpPost]
        public IActionResult Add(Designer designer)
        {
            if (!ModelState.IsValid) return View(designer);
            Designer newDesigner = _manager.AddDesigner(designer.Name, designer.Age, designer.Nationality);
            return RedirectToAction("Details", "Designer", new {designerId = newDesigner.Id});
        }

        /*
        private Designer GetDesignersAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, ENDPOINT + "/" + id);
            var response = _clientFactory.CreateClient("rest").Send(request);
            var designer = new Designer();
            if (response.IsSuccessStatusCode)
            {
                var respoonseStream = response.Content.ReadAsStringAsync().Result;
                designer = JsonConvert.DeserializeObject<Designer>(respoonseStream);
            }

            return designer;
        }
        
        private List<ClothingPiece> GetClothingPieces()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, ENDPOINT_CLOTHINGPIECES);
            var response = _clientFactory.CreateClient("rest").SendAsync(request).Result;
            var clothingpieces = new List<ClothingPiece>();
            if (response.IsSuccessStatusCode)
            {
                var respoonseStream = response.Content.ReadAsStringAsync().Result;
                clothingpieces = JsonConvert.DeserializeObject<List<ClothingPiece>>(respoonseStream);
            }

            return clothingpieces;
        }
        */
    }
}