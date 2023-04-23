using Microsoft.AspNetCore.Mvc; // 2. Import required for ControllerBase

namespace dotnet_rpg.Controllers
{
    [ApiController] // 3. Attribute is used to identify a class as a Web API controller. This attribute is essential because it informs the framework that the class should be treated as a controller, allowing it to handle incoming HTTP requests and route them to the appropriate action methods.
    [Route("api/[controller]")] // 4. The [Route("api/[controller]")] attribute in a .NET controller sets the route template for the entire controller. In this example, "api" is a fixed segment in the URL, while "[controller]" is a placeholder that gets replaced by the controller's name (without the "Controller" suffix) at runtime.
    public class CharacterController : ControllerBase // 1. All controllers must be derived from ControllerBase
    {
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character { Id = 1 ,Name = "Sam" }
        };

        [HttpGet("GetAll")] // 6. The [HttpGet] attribute is used to specify that the method responds to HTTP GET requests. The "GetAll" parameter is a route template that is appended to the route template of the controller. The final route for this action is "api/Character/GetAll".
        public ActionResult<List<Character>> Get() // 5. IActionResult provides a consistent way to package and deliver the results of your Web API actions, making it easier for both developers and client applications to understand and handle the outcomes of various requests.
        {
            return Ok(characters);
        }

        [HttpGet("{id}")] // 7. The {id} parameter is a placeholder for a route parameter. Route parameters are defined in the route template and are bound to the action method's parameters. In this example, the route parameter is bound to the id parameter of the GetSingle action method.
        public ActionResult<Character> GetSingle(int id)
        {
            return Ok(characters.Find(character => character.Id == id));
        }

        [HttpPost]
        /// <summary>
        /// Add a new character
        /// </summary>
        public ActionResult<List<Character>> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return Ok(characters);
        }
    }
}