using AutoMapper;
using CaterServMongoDb.DataAccess.Entities;
using CaterServMongoDb.Dtos.CategoryDtos;
using CaterServMongoDb.Services.Abstract;
using CaterServMongoDb.Settings;
using MongoDB.Driver;

namespace CaterServMongoDb.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task CreateCategory(CreateCategoryDto categoryDto)
        {
           var values = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(values);
        }

        public async Task DeleteCategory(string id)
        {
           await _categoryCollection.DeleteOneAsync(x=>x.CategoryId == id);
        }

        public async Task<List<ResultCategoryDto>> GetAllCategories()
        {
            var values = await _categoryCollection.AsQueryable().ToListAsync();

            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        public async Task<UpdateCategoryDto> GetCategoryById(string id)
        {
           var value =await _categoryCollection.Find(x=>x.CategoryId==id).FirstOrDefaultAsync();
            return _mapper.Map<UpdateCategoryDto>(value);
        }

        public async Task UpdateCategory(UpdateCategoryDto categoryDto)
        {
            var values = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == values.CategoryId, values);
        }
    }
}
