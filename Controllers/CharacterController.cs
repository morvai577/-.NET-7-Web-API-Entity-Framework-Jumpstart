using Microsoft.AspNetCore.Mvc; // 2. Import required for ControllerBase

namespace dotnet_rpg.Controllers
{
    [ApiController] // 3. Attribute is used to identify a class as a Web API controller. This attribute is essential because it informs the framework that the class should be treated as a controller, allowing it to handle incoming HTTP requests and route them to the appropriate action methods.
    [Route("api/[controller]")] // 4. The [Route("api/[controller]")] attribute in a .NET controller sets the route template for the entire controller. In this example, "api" is a fixed segment in the URL, while "[controller]" is a placeholder that gets replaced by the controller's name (without the "Controller" suffix) at runtime.
    public class CharacterController : ControllerBase // 1. All controllers must be derived from ControllerBase
    {
        // 9. The CharacterController class has a constructor that accepts an ICharacterService parameter. The ICharacterService parameter is assigned to a private field, which is used to access the CharacterService methods.
        private readonly ICharacterService _characterService;

        // 10. Dependency injection is a technique for achieving Inversion of Control between classes and their dependencies. The ASP.NET Core runtime handles the complex task of instantiating the dependencies and passing them to the class that requires them. The runtime uses the constructor of the class to determine which dependencies it requires. The runtime then uses the types of the constructor parameters to locate the services that are registered for those types. Finally, the runtime injects an instance of each required service into the constructor when it creates an instance of the class. This technique is called "constructor injection".
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")] // 6. The [HttpGet] attribute is used to specify that the method responds to HTTP GET requests. The "GetAll" parameter is a route template that is appended to the route template of the controller. The final route for this action is "api/Character/GetAll".
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResDto>>>> Get() // 5. IActionResult provides a consistent way to package and deliver the results of your Web API actions, making it easier for both developers and client applications to understand and handle the outcomes of various requests.
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")] // 7. The {id} parameter is a placeholder for a route parameter. Route parameters are defined in the route template and are bound to the action method's parameters. In this example, the route parameter is bound to the id parameter of the GetSingle action method.
        public async Task<ActionResult<ServiceResponse<GetCharacterResDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        /// <summary>
        /// 8. Add a new character
        /// </summary>
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResDto>>>> AddCharacter(AddCharacterReqDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResDto>>>> UpdateCharacter(UpdateCharacterReqDto updatedCharacter)
        {
            var response = await _characterService.UpdateCharacter(updatedCharacter);

            if (response.Data is null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}