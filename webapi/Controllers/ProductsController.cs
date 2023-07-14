using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        [HttpGet]
        public ActionResult GetProductsPaginated(string searchQuery, int page, int pageSize)
        {
            NorthwindDB db = new NorthwindDB();
            List<Product> products = db.GetProductsPaginated(searchQuery, page, pageSize);
            return Ok(products);
        }
    }
}
