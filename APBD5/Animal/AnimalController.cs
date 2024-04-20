using Microsoft.AspNetCore.Mvc;

namespace APBD5.Animal;


[Route("/api/animal")]
[ApiController]
public class AnimalController(IAnimalService animalService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAnimals([FromQuery] string orderBy)
    {
        var animals = animalService.GetAll(orderBy);
        return Ok(animals);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult CreateAnimal([FromBody] AnimalDTO dto)
    {
        var success = animalService.Create(dto);
        return success ? Created() : Conflict();
    }

    [HttpPut("/api/animal/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult UpdateAnimal([FromRoute] int id, [FromBody] AnimalDTO dto)
    {
        var success = animalService.Update(id, dto);
        return success ? Ok() : Conflict();
    }

    [HttpDelete("/api/animal/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public IActionResult DeleteAnimal([FromRoute] int id)
    {
        var success = animalService.Delete(id);
        return success ? NoContent() : Conflict();
    }
}