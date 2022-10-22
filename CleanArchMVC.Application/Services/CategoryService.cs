using AutoMapper;
using CleanArchMVC.Application.DTOs;
using CleanArchMVC.Application.Interfaces;
using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Interfaces;

namespace CleanArchMVC.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategories()
        {
            var categoriesEntity = await _categoryRepository.GetCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task<CategoryDTO> GetById(int? id)
        {
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(categoryEntity);
        }

        public async Task Add(CategoryDTO categoryDTO)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.CreateAsync(categoryEntity);
        }

        public async Task Update(CategoryDTO categoryDTO)
        {
            var categoryEntity = _mapper.Map<Category>(categoryDTO);
            await _categoryRepository.UpdateAsync(categoryEntity);
        }

        public async Task Remove(int? id)
        {
            var categoryEntity = _categoryRepository.GetByIdAsync(id).Result;
            await _categoryRepository.RemoveAsync(categoryEntity);
        }
    }
}
