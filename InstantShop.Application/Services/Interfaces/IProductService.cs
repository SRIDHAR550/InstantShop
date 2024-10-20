using InstantShop.Application.DTO_s.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstantShop.Application.Services.Interfaces
{
    public interface IProductService
    {

        Task<ProductDto> CreateAsync(CreateProductDto createProductDto);
        Task UpdateAsync(UpdateProductDto updateProductDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<IEnumerable<ProductDto>> GetAllByFilterAsync(int? categoryId, int? brandId);
        Task<ProductDto> GetByIdAsync(int id);
    }
}
