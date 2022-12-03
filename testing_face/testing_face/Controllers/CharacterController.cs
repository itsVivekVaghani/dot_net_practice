using Microsoft.AspNetCore.Mvc;
using testing_face.Dtos.Character;
using testing_face.Services.CharacterService;

namespace testing_face.Controllers;

[ApiController] 
[Route("api/[controller]")]  
public class CharacterController : ControllerBase
{
    private readonly CharacterService _characterService; 

    public CharacterController(CharacterService characterService) 
    {
        _characterService = characterService; 
    }
    [HttpGet]
    public async  Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()  
    {
        return Ok(await  _characterService.GetAllCharacters());
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)    
    {
        return Ok(await _characterService.GetCharacterById(id));
    }
     
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddRecord(AddCharacterDto newCharacter)
    {
        return  Ok(await _characterService.AddCharacter(newCharacter));
    }
      
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> UpdateRecord(UpdateCharacterDto updateCharacter)
    {
        var response = await _characterService.UpdateCharacter(updateCharacter);
        if (response.Data == null)
        {
            return NotFound(response);
        }

        return Ok(response);

    }
     
    [HttpDelete]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteRecord(int id) 
    {
        var response = await _characterService.DeleteCharacter(id);
        return Ok(response);

    }
}