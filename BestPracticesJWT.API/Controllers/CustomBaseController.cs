using BestPracticesJWT.SharedCommons.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BestPracticesJWT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {

        public IActionResult ActionResultInstance<T>(ResponseDto<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode,
            };
        
        }

    }
}
