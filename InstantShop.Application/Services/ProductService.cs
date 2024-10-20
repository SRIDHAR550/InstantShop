using AutoMapper;
using InstantShop.Application.DTO_s.Product;
using InstantShop.Application.Services.Interfaces;
using InstantShop.Domain.Contracts;
using InstantShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantShop.Application.Services
{
  
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            var product02 = await _productRepository.CreateAsync(product);
            var entiry = _mapper.Map<ProductDto>(product02);
            return entiry;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(x => x.Id == id);
            await _productRepository.DeleteAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var product = await _productRepository.GetAllProductAsync();
            return _mapper.Map<List<ProductDto>>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllByFilterAsync(int? categoryId, int? brandId)
        {
            var data = await _productRepository.GetAllProductAsync();

            IEnumerable<Product> query = data;

            if (categoryId > 0)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }
            if (brandId > 0)
            {
                query = query.Where(x => x.BrandId == brandId);
            }
            var result = _mapper.Map<List<ProductDto>>(query);
            return result;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetDetailsAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

       

        public async Task UpdateAsync(UpdateProductDto updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            await _productRepository.UpdateAsync(product);
        }

    }
}
