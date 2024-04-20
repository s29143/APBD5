namespace APBD5.Animal;

public interface IAnimalService
{
    public IEnumerable<Animal> GetAll(string orderBy);
    public bool Create(AnimalDTO animalDto);
    public bool Update(int id, AnimalDTO animalDto);
    public bool Delete(int id);
}

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;
    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public IEnumerable<Animal> GetAll(string orderBy)
    {
        return _animalRepository.GetAll(orderBy);
    }

    public bool Create(AnimalDTO animalDto)
    {
        return _animalRepository.Create(animalDto);
    }

    public bool Update(int id, AnimalDTO animalDto)
    {
        return _animalRepository.Update(id, animalDto);
    }

    public bool Delete(int id)
    {
        return _animalRepository.Delete(id);
    }
}