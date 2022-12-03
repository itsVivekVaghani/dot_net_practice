using AutoMapper;
using Microsoft.EntityFrameworkCore;
using testing_face.Data;
using testing_face.Dtos.Character;

namespace testing_face.Services.CharacterService;

public class CharacterService 
{
    private readonly IMapper _mapper;
    
    private readonly DataContext _context;

    public CharacterService(IMapper mapper,DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        Character character = _mapper.Map<Character>(newCharacter);
        _context.Characters.Add(character);
        await _context.SaveChangesAsync();
        serviceResponse.Data =  await _context.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
        return serviceResponse;
        // var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        // Character character = _mapper.Map<Character>(newCharacter);
        // character.Id = characters.Max(c => c.Id) + 1;
        // characters.Add(character);
        // serviceResponse.Data =  characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        // return serviceResponse;
    }
     
    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    { 
        var response = new ServiceResponse<List<GetCharacterDto>>();
        var dbCharacter = await _context.Characters.ToListAsync();
        response.Data = dbCharacter.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        return response;
        // return new ServiceResponse<List<GetCharacterDto>>
        // {
        //     Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
        // };
    }
    
    public  async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        var dbCharacter = await  _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
        serviceResponse.Data =  _mapper.Map<GetCharacterDto>(dbCharacter);
        return serviceResponse;
        // var serviceResponse = new ServiceResponse<GetCharacterDto>();
        // var character = characters.FirstOrDefault(c => c.Id == id);
        // serviceResponse.Data =  _mapper.Map<GetCharacterDto>(character);
        // return serviceResponse;
    }
    
    public  async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter) 
    {
        var response = new ServiceResponse<GetCharacterDto>();
        try
        {
            var character = await _context.Characters.FirstOrDefaultAsync
                (c => c.Id == updateCharacter.Id);
            character.Name = updateCharacter.Name;
            character.HitPoints = updateCharacter.HitPoints;
            character.Strength = updateCharacter.Strength;
            character.Defense = updateCharacter.Defense;
            character.Intelligence = updateCharacter.Intelligence;
            character.Class = updateCharacter.Class;
            await _context.SaveChangesAsync();
            
            
            response.Data = _mapper.Map<GetCharacterDto>(character);
            // var character = characters.FirstOrDefault(c => c.Id == updateCharacter.Id);
            // _mapper.Map(updateCharacter, character);
            // response.Data = _mapper.Map<GetCharacterDto>(character);
           
            
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
            // Console.WriteLine(e);
            // throw;
        }
        return response;
    }
    
    public  async Task<ServiceResponse<GetCharacterDto>> DeleteCharacter(int id) 
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        try
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            if(character != null) _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data =  _mapper.Map<GetCharacterDto>(null);
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = e.Message;
        }
        serviceResponse.Message = "Character successfully deleted";
        return serviceResponse;
    }
}
