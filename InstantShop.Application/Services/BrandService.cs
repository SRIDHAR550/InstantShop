using AutoMapper;
using InstantShop.Application.DTO_s.Brand;
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
    public class BrandService : IBrandService
    {
        protected readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        public BrandService(IBrandRepository brandRepository, IMapper mapper)
        {
            _brandRepository = brandRepository;
            _mapper= mapper;
        }
        public async Task<BrandDto> CreateAsync(CreateBrandDto createBrandDto)
        {
            var brand = _mapper.Map<Brand>(createBrandDto);
            var entity= await _brandRepository.CreateAsync(brand);
            var GetBrandDto = _mapper.Map<BrandDto>(entity);
            return GetBrandDto;
        }

        public async Task DeleteAsync(int id)
        {
            var brand=await _brandRepository.GetByIdAsync(x=>x.Id==id);
            await _brandRepository.DeleteAsync(brand);

        }

        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            var brand = await _brandRepository.GetAllAsync();
            return _mapper.Map<List<BrandDto>>(brand);
        }

        public async Task<BrandDto> GetByIdAsync(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(x => x.Id == id);
            return _mapper.Map<BrandDto>(brand);
        }

        public async Task UpdateAsync(UpdateBrandDto updateBrandDto)
        {
             var brand =_mapper.Map<Brand>(updateBrandDto);
             await _brandRepository.UpdateAsync(brand);
        }
    }
}
